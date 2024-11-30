using BerriedDelight.Models;
using BerriedDelight.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BerriedDelight.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBerryRepository _berryRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IBerryRepository berryRepository, IShoppingCart shoppingCart)
        {
            _berryRepository = berryRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int berryId)
        {
            var selectedBerry = _berryRepository.AllBerries.FirstOrDefault(p => p.BerryId == berryId);
            if (selectedBerry != null)
            {
                _shoppingCart.AddToCart(selectedBerry, 1);
            }
            return RedirectToAction("Index");
        }

        //Method to increase berry quantity
        [HttpPost]
        public IActionResult IncreaseQuantity(int berryId)
        {
            var selectedBerry = _berryRepository.GetBerryById(berryId);
            if (selectedBerry != null) 
            {
                _shoppingCart.AddToCart(selectedBerry, 1);
            }
            return RedirectToAction("Index");
        }

        //Method to decrease berry quantity
        [HttpPost]
        public IActionResult DecreaseQuantity(int berryId)
        {
            var selectedBerry = _berryRepository.GetBerryById(berryId);

            if (selectedBerry != null)
            {
                _shoppingCart.DecreaseCartQuantity(selectedBerry);
            }

            return RedirectToAction("Index");
        }

        //Method to remove berry from cart (regardless of quantity)
        public IActionResult RemoveFromShoppingCart(int berryId)
        {
            var selectedBerry = _berryRepository.AllBerries.FirstOrDefault(p => p.BerryId == berryId);
            if (selectedBerry != null)
            {
                _shoppingCart.RemoveAllFromCart(selectedBerry);
            }
            return RedirectToAction("Index");
        }
    }
}
