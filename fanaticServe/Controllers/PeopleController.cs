using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class PeopleController : Controller
{
    private readonly IPeople _people;

    public PeopleController(IPeople people)
    {
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
