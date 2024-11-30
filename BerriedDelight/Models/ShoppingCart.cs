using BerriedDelight.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BerriedDelight.Controllers
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BerriedDelightDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(BerriedDelightDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            var context = services.GetService<BerriedDelightDbContext>();
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Berry berry, int amount)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Berry.BerryId == berry.BerryId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Berry = berry,
                    Amount = amount
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += amount;
            }
            _context.SaveChanges();
        }

        public int DecreaseCartQuantity(Berry berry)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Berry.BerryId == berry.BerryId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    _context.SaveChanges();
                    return shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                    _context.SaveChanges();
                    return 0;
                }
            }
            return 0;
        }

        public void RemoveAllFromCart(Berry berry)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Berry.BerryId == berry.BerryId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
                _context.SaveChanges();
            }
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Berry)
                           .ToList());
        }

        public decimal GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Berry.Price * c.Amount).Sum();
        }

        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
    }
}
