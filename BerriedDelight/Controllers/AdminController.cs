//Parth Talwar
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BerriedDelight.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

//Controller to pass functions throughout the admin page
public class AdminController : Controller
{
    private readonly IBerryRepository _berryRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(IBerryRepository berryRepository, UserManager<IdentityUser> userManager)
    {
        _berryRepository = berryRepository;
        _userManager = userManager;
    }

    //Get all berries from berries table
    [HttpGet]
    public IActionResult AdminBerries()
    {
        var berries = _berryRepository.AllBerries;
        return View(berries);
    }

    //HttpPost to create berry and store it on table
    [HttpPost]
    public IActionResult CreateBerry(Berry berry)
    {

        _berryRepository.AddBerry(berry);

        var allBerries = _berryRepository.AllBerries;
        return View("AdminBerries", allBerries);
    }

    //HttpPost to edit berry and update table
    [HttpPost]
    public IActionResult EditBerry(Berry berry)
    {
        _berryRepository.UpdateBerry(berry);

        //Redirect to Adminberries action after successful edit
        return RedirectToAction("AdminBerries");
    }

    [HttpGet]
    public IActionResult GetBerry(int id)
    {
        var berry = _berryRepository.GetBerryById(id);
        if (berry == null)
        {
            return NotFound();
        }
        return Json(berry);
    }

    //HttpPost to delete berry and update table
    [HttpPost]
    public IActionResult DeleteBerry(int id)
    {
        var berry = _berryRepository.GetBerryById(id);
        if (berry == null)
        {
            return NotFound();
        }
        _berryRepository.DeleteBerry(id);

        //Redirect to AdminBerries action after successful deletion
        return RedirectToAction("AdminBerries");
    }
}

