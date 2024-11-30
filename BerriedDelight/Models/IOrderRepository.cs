namespace BerriedDelight.Models
{
    public interface IOrderRepository
    {
        //Method to take an order instance
        void CreateOrder(Order order);
        IEnumerable<Order> ReceiveAllOrders();
        Order GetOrderById (int orderId);
        void DeleteOrder(int orderId);
    }
}
