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
        public JsonResult RegisterUser(string userName,string password)
        {
            object JsonData = null;
            UserViewModel user = new UserViewModel();
            user.username = userName;
            user.password = password;
            bool isRegistered = false;
            try
            {
                isRegistered = user.RegisterUser(user);
            }
            catch (Exception ex) { 
            }
            return Json(JsonData);
        }
        /// <summary>
        /// 
        
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult verifyUserName(string userName) { 
            UserViewModel user = new UserViewModel();
            object JsonData = null;
            bool isExist = false;
            isExist = user.checkDuplicate(userName);
            JsonData = new
            {
                StatusCode = 200,
                isExist = isExist
            };
            return Json(JsonData);
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns>To verify whether User is existing or not</returns>
        [HttpPost]
        public JsonResult Login(string userName, string password)
        {

            dynamic JsonData = null;
            UserViewModel user = new UserViewModel();
            HttpContext.Session.SetObject("UserDetails", user);
            user = user.ValidateUser(userName, password);
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("userName","");
            HttpContext.Session.SetString("Role", "");
            if (user != null)
            {
                HttpContext.Session.SetString("Mobile", user.userMobile);
                HttpContext.Session.SetString("Email", user.userEmail);
                HttpContext.Session.SetString("Address", user.userFullAddress);
                HttpContext.Session.SetString("userName", user.username);
                HttpContext.Session.SetString("Role", user.userRoleName);
                HttpContext.Session.SetInt32("UserId", user.userID);
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

        /// <summary>
        /// Users List Page
        /// </summary>
        /// <returns> List of all active users</returns>
        public IActionResult ManageUsers()
        {
            UserViewModel user = new UserViewModel();
            List<UserViewModel> userList = new List<UserViewModel>();
            userList = user.UsersList();
            return View(userList.ToList());
        }
        /// <summary>
        /// Roles 
        /// </summary>
        /// <returns> List of Roles will be returned</returns>
        public IActionResult GetRoles()
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            List<RoleViewModel> Roles = new List<RoleViewModel>();
            Roles = roleViewModel.GetAllRoles();
            return View(Roles.ToList());
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetObject("UserDetails", "");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("Role");
            //HttpContext.Session.Remove("userName");
            return RedirectToAction("Login");
        }
    }
}  
