using BerriedDelight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerriedDelight.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some berries first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            //Using ViewBag to display confirmation message
            ViewBag.CheckoutCompleteMessage = "Order has been processed. Explore the delight of your berries!";
            return View();
        }

        public IActionResult ViewOrders()
        {
            var orders = _orderRepository.ReceiveAllOrders();
            return View(orders);
        }

        //Action result to remove order from account 
        public IActionResult DeleteOrders(int id)
        {
            _orderRepository.DeleteOrder(id);
            //Redirects back to view orders page with the item removed
            return RedirectToAction("ViewOrders");
        }
    }
}
