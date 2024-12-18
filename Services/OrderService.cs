using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace E_CommerceSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductService _productService;

        public OrderService(IOrderRepo orderRepo, IProductService productService)
        {
            _orderRepo = orderRepo;
            _productService = productService;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _orderRepo.GetAllOrders();
            if (orders == null)
                throw new KeyNotFoundException($"order List is empty.");

            return orders;
        }

        public Order GetOrderById(int oid)
        {
            var order = _orderRepo.GetOrderById(oid);
            if (order == null)
                throw new KeyNotFoundException($"order with ID {oid} not found.");

            return order;
        }

        public void DeleteOrder(int oid)
        {
            var order = _orderRepo.GetOrderById(oid);
            if (order == null)
                throw new KeyNotFoundException($"order with ID {oid} not found.");

            _orderRepo.DeleteOrder(oid);
            throw new Exception($"order with ID {oid} is deleted");
        }
        public void AddOrder(Order order)
        {
            _orderRepo.AddOrder(order);
        }
        public void UpdateOrder(Order order)
        {
            _orderRepo.UpdateOrder(order);
        }
        public void PlaceOrder( List<OrderItemDTO> items, int uid)
        {
           
            Product existingProduct = null;
            decimal TotalPrice, totalOrderPrice = 0 ;

            for (int i = 0; i < items.Count; i++)
            {
                TotalPrice = 0;
                existingProduct = _productService.GetProductByName(items[i].ProductName);
                if (existingProduct == null)
                    throw new Exception($"{items[i].ProductName} not Found");

                if (existingProduct.Stock < items[i].Quantity)
                    throw new Exception($"{items[i].ProductName} is out of stock");

            }
            foreach (var item in items)
            {
                existingProduct = _productService.GetProductByName(item.ProductName);
                TotalPrice = item.Quantity * existingProduct.Price;
                existingProduct.Stock -= item.Quantity;
                
                totalOrderPrice += TotalPrice;
                _productService.UpdateProduct(existingProduct);
            }
            var order = new Order { TotalAmount = totalOrderPrice, OrderDate = DateTime.Now, UID = uid };
            AddOrder(order);
        }
    }
}
