using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class PeopleController : Controller
{
    private readonly IFanaticServeContext _context;

    public PeopleController(IFanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Detail(Guid? id)
    {

        if (id == null)
        {
            return NotFound();
        }

        return View(new PeopleService(_context).GetPerson(id.Value));
    }
}
