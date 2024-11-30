//Parth Talwar
using Microsoft.EntityFrameworkCore;

namespace BerriedDelight.Models
{
    public class BerryRepository : IBerryRepository
    {
        private readonly BerriedDelightDbContext _berriedDelightDbContext;

        // Constructor that takes a BerriedDelightDbContext instance
        public BerryRepository(BerriedDelightDbContext berriedDelightDbContext)
        {
            _berriedDelightDbContext = berriedDelightDbContext;
        }

        //Property to get all berries including their categories
        public IEnumerable<Berry> AllBerries
        {
            get
            {
                return _berriedDelightDbContext.Berries.Include(c => c.Category);
            }
        }

        //Property to get popular berries including their categories
        public IEnumerable<Berry> PopularBerries
        {
            get
            {
                return _berriedDelightDbContext.Berries.Include(c => c.Category).Where(p => p.IsPopularBerry);
            }
        }

        //Method to get a berry by its ID
        public Berry? GetBerryById(int berryId)
        {
            return _berriedDelightDbContext.Berries.FirstOrDefault(p => p.BerryId == berryId);
        }

        //Search method 
        public IEnumerable<Berry> SearchBerries(string searchQuery)
        {
            return _berriedDelightDbContext.Berries.Where(p => p.Name.Contains(searchQuery));
        }

        //Add Berry method (Admin only)
        public void AddBerry(Berry berry)
        {
            _berriedDelightDbContext.Berries.Add(berry);
            _berriedDelightDbContext.SaveChanges();
        }

        //Update Berry method (Admin only)
        public void UpdateBerry(Berry berry)
        {
            _berriedDelightDbContext.Berries.Update(berry);
            _berriedDelightDbContext.SaveChanges();
        }

        //Delete Berry method (Admin only)
        public void DeleteBerry(int berryId)
        {
            var berry = _berriedDelightDbContext.Berries.Find(berryId);
            if (berry != null)
            {
                _berriedDelightDbContext.Berries.Remove(berry);
                _berriedDelightDbContext.SaveChanges();
            }
        }
    }
}

