using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderService
    {
        void DeleteOrder(int oid);
        public IEnumerable<OrdersOutputOTD> GetAllOrders(int uid);
        Order GetOrderById(int oid);
        void PlaceOrder(List<OrderItemDTO> items, int uid);
        void UpdateOrder(Order order);
    }
}