using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationToka.Controllers
{
    public class ReportsController : Controller
    {

        public ReportsController()
        {
        }

        public IActionResult Index()
        {


            return View();
        }

    }
}
