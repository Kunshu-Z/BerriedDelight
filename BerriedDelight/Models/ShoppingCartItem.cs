namespace BerriedDelight.Models
{
    public class ShoppingCartItem
    {
        //Fields for Shopping Cart
        public int ShoppingCartItemId { get; set; } 
        public Berry Berry { get; set; } = default!;
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; } //To provide users with a unique shopping cart ID when entering the website
    }
}
