namespace BerriedDelight.Models
{
    public interface IShoppingCart
    {
        //Fields
        void AddToCart(Berry berry, int amount);
        int DecreaseCartQuantity(Berry berry);
        void RemoveAllFromCart (Berry berry);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
