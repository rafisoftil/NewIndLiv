using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }
    }
}
