namespace BerriedDelight.Models
{
    public interface IBerryRepository
    {
        //Fields
        IEnumerable<Berry> AllBerries { get; }
        IEnumerable<Berry> PopularBerries { get; }
        Berry? GetBerryById(int berryId);
        IEnumerable<Berry> SearchBerries(string searchQuery);
        void AddBerry(Berry berry);
        void UpdateBerry(Berry berry);
        void DeleteBerry(int berryId);
    }
}
