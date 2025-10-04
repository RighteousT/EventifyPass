using Microsoft.AspNetCore.Mvc;

namespace EventifyPass.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
