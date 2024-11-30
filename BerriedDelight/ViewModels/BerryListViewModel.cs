using BerriedDelight.Models;

namespace BerriedDelight.ViewModels
{
    public class BerryListViewModel
    {
        //Fields
        public IEnumerable<Berry> Berries { get; }
        public string? CurrentCategory { get; }
        public string? CategorizeBy { get; set; }

        public BerryListViewModel(IEnumerable<Berry> berries, string? currentCategory, string? categorizeBy)
        {
            Berries = berries;
            CurrentCategory = currentCategory;
            CategorizeBy = categorizeBy;
        }
    }
}
