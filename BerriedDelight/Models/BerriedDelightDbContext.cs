using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BerriedDelight.Models
{
    //Referencing IdentityDbContext for login & register functions
    public class BerriedDelightDbContext : IdentityDbContext
    {
        public BerriedDelightDbContext(DbContextOptions<BerriedDelightDbContext> options) : base(options)
        {
        }
        //Fields
        public DbSet<Category> Categories { get; set; }
        public DbSet<Berry> Berries { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        //To save order and order details to the database
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order> OrderDetails { get; set; }
    }
}
