using BerriedDelight.Models;
using BerriedDelight.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;

namespace BerriedDelight.Controllers
{
    public class BerryController : Controller
    {
        private readonly IBerryRepository _berryRepository;
        private readonly ICategoryRepository _categoryRepository;

        //Constructor
        public BerryController(IBerryRepository berryRepository, ICategoryRepository categoryRepository)
        {
            _berryRepository = berryRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category, string categorizeBy)
        {
            IEnumerable<Berry> berries;
            string? currentCategory;

            if (string.IsNullOrEmpty(category) || category == "All Berries")
            {
                berries = _berryRepository.AllBerries;
                currentCategory = "All Berries";
            }
            else
            {
                berries = _berryRepository.AllBerries.Where(p => p.Category.CategoryName == category);
                currentCategory = category;
            }

            //Adding a switch to order berry contents according to the filter selected
            berries = categorizeBy switch
            {
                "name_asc" => berries.OrderBy(p => p.Name),
                "name_desc" => berries.OrderByDescending(p => p.Name),
                "price_asc" => berries.OrderBy(p => p.Price),
                "price_desc" => berries.OrderByDescending(p => p.Price),
                _ => berries
            };
            return View(new BerryListViewModel(berries, currentCategory, categorizeBy));
        }

        public IActionResult Details(int id)
        {
            var berry = _berryRepository.GetBerryById(id);
            if (berry == null)
            {
                return NotFound();
            }
            return View(berry);
        }

        //Method for search
        public IActionResult Search()
        {
            return View();
        }
    }
}
