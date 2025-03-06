using Microsoft.AspNetCore.Mvc;

namespace ApplicationToka.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
