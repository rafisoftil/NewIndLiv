using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult AdminServices()
        {
            return View();
        }
        public IActionResult ServiceDetails()
        {
            return View();
        }
        public IActionResult ServiceProviderDashboard()
        {
            return View();
        }
        public IActionResult ServicesList()
        {
            return View();
        }
    }
}
