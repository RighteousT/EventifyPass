using Microsoft.AspNetCore.Mvc;

namespace EventifyPass.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
