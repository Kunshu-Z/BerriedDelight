using Microsoft.AspNetCore.Mvc;

namespace BerriedDelight.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
