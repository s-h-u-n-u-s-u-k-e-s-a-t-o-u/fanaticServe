using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IPeople _people;

    public PeopleController(ILogger<PeopleController> logger, IPeople people)
    {
        _logger = logger;
        _people = people;
    }

    [HttpGet]
    public IActionResult Detail(Guid? id)
    {

        if (id == null)
        {
            return NotFound();
        }

        return View(_people.GetPerson(id.Value));
    }
}
