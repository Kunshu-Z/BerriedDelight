using Microsoft.EntityFrameworkCore;

namespace BerriedDelight.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        //Fields
        private readonly BerriedDelightDbContext _berriedDelightDbContext;

        //Constructor
        public CategoryRepository(BerriedDelightDbContext berriedDelightDbContext)
        {
            _berriedDelightDbContext = berriedDelightDbContext;
        }

        public IEnumerable<Category> AllCategories => _berriedDelightDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
