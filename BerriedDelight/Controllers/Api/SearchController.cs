using BerriedDelight.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BerriedDelight.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBerryRepository _berryRepository;

        public SearchController (IBerryRepository berryRepository)
        {
            _berryRepository = berryRepository;
        }

        //To trigger the execution of the method
        [HttpGet]
        public IActionResult GetAll()
        {
            var allBerries = _berryRepository.AllBerries;
            return Ok(allBerries);
        }

        //Adding another HttpGET which uses the integer id
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            if (!_berryRepository.AllBerries.Any(p => p.BerryId == id))
                return NotFound();
            return Ok(_berryRepository.AllBerries.Where(p => p.BerryId == id));
        }

        [HttpPost]
        public IActionResult SearchBerries([FromBody] string searchQuery)
        {
            IEnumerable<Berry> berries = new List<Berry>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                berries = _berryRepository.SearchBerries(searchQuery);
            }
            return new JsonResult(berries);
        }
    }
}
