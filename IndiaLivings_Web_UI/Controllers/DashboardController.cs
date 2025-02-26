using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class DashboardController : Controller
    {
        /// <summary>
        /// Landing Page
        /// </summary>
        /// <returns>Initially, this will be the landing page</returns>
        public IActionResult Dashboard()
        {
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categories = category.GetCategoryCount();
            ViewData["Categories"] = categories;
            return View();
        }
        /// <summary>
        /// Sign Up / Sign In Page
        /// </summary>
        /// <returns>once User clicks on "Join Me" Login page will be loaded</returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Registration
        /// </summary>
        /// <returns>To Create New User, Create User will be called</returns>
        [HttpPost]
        public IActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns>To verify whether User is existing or not</returns>
        [HttpPost]
        public IActionResult Login(string userName,string password)
        {

            return View();
        }
    }
}
