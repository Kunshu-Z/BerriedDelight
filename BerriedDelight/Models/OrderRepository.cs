using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BerriedDelight.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BerriedDelightDbContext _berriedDelightDbContext;
        private readonly IShoppingCart _shoppingCart;
        //Using UserManager to link UserId with orders
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepository(
            BerriedDelightDbContext berriedDelightDbContext,
            IShoppingCart shoppingCart,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _berriedDelightDbContext = berriedDelightDbContext;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //Function to create an order
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    BerryId = shoppingCartItem.Berry.BerryId,
                    Price = shoppingCartItem.Berry.Price,
                    UserId = userId
                };
                order.OrderDetails.Add(orderDetail);
            }

            _berriedDelightDbContext.Orders.Add(order);
            _berriedDelightDbContext.SaveChanges();
        }

        //Function to recieve all orders to display user's orders made
        public IEnumerable<Order> ReceiveAllOrders()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _berriedDelightDbContext.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDetails.Any(od => od.UserId == userId))
                .ToList();
        }

        //Function to get each order by its Id
        public Order GetOrderById(int orderId)
        {
            return _berriedDelightDbContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Berry).FirstOrDefault(o => o.OrderId == orderId);
        }

        //Function to delete order from te database
        public void DeleteOrder(int orderId)
        {
            var order = _berriedDelightDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                _berriedDelightDbContext.Orders.Remove(order);
                _berriedDelightDbContext.SaveChanges();
            }
        }
    }
}
