using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Session;
namespace IndiaLivings_Web_UI.Controllers
{
    public class DashboardController : Controller
    {
        /// <summary>
        /// Landing Page
        /// </summary>
        /// <returns>Initially, this page will be the loaded</returns>
        public IActionResult Dashboard()
        {
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            dynamic data = new ExpandoObject();
            data.Categories = categoryList;
            return View(data);
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
        public JsonResult Login(string userName, string password)
        {
            int StatusCode = 0;
            dynamic JsonData = null;
            UserViewModel user = new UserViewModel();
            user = user.ValidateUser(userName, password);
            HttpContext.Session.SetString("userName","");
            HttpContext.Session.SetString("Role", "");
            if (user != null)
            {
                HttpContext.Session.SetString("userName", user.username);
                HttpContext.Session.SetString("Role",user.userRoleName);
                JsonData = new
                {
                    StatusCode = 200,
                    userId = user.userID,
                    userRole = user.userRoleName,
                    userImage = user.userImagePath
                };

            }
            else if (user == null)
                JsonData = new
                {
                    StatusCode = 400
                };
            else
            {
                JsonData = new
                {
                    StatusCode = 500
                };
            }

            return Json(JsonData);
        }
    }
}
