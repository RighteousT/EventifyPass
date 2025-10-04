using Microsoft.AspNetCore.Mvc;

namespace EventifyPass.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
