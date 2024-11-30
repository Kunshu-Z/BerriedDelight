using BerriedDelight.Models;

namespace BerriedDelight.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Berry> PopularBerries { get; }

        public HomeViewModel(IEnumerable<Berry> popularBerries)
        {
            PopularBerries = popularBerries;
        }
    }
}
