using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class StarMatrixController : Controller
{
    private readonly IStarMatrix _starMatrix;

    public StarMatrixController(IStarMatrix starMatrix)
    {
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
