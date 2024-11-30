using BerriedDelight.Models;
using BerriedDelight.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BerriedDelight.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBerryRepository _berryRepository;

        public HomeController(IBerryRepository berryRepository)
        {
            _berryRepository = berryRepository;
        }
        public IActionResult Index()
        {
            var popularBerries = _berryRepository.PopularBerries;
            var homeViewModel = new HomeViewModel(popularBerries);
            return View(homeViewModel);
        }
    }
}
