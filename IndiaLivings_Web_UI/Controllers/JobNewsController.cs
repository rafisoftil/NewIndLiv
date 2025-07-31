using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class JobNewsController : Controller
    {
        public IActionResult JobDetails()
        {
            return View();
        }
        public IActionResult CreateJob()
        {
            return View();
        }
    }
}
