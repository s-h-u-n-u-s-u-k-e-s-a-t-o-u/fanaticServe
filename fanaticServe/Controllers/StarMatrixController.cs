using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class StarMatrixController : Controller
{
    private readonly ILogger<StarMatrixController> _logger;
    private readonly IStarMatrix _starMatrix;

    public StarMatrixController(ILogger<StarMatrixController> logger, IStarMatrix starMatrix)
    {
        _logger = logger;
        _starMatrix = starMatrix;
    }

    public IActionResult Index()
    {
        return View(_starMatrix.GetStarMatrix());
    }

    // Modalを表示するパーシャルビューを返すアクション
    public IActionResult GetModal()
    {
        return PartialView("_StarMatrixModal", _starMatrix.GetStarMatrix());
    }
}
