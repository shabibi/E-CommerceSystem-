using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderService
    {
        void DeleteOrder(int oid);
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int oid);
        void PlaceOrder(List<OrderItemDTO> items, int uid);
        void UpdateOrder(Order order);
    }
}