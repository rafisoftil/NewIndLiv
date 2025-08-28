using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = svm.getAllCategories();
            return View(categories);
        }
        public IActionResult AdminServices()
        {
            return View();
        }
        public IActionResult ServiceDetails(int categoryId)
        {
            ServiceViewModel svm = new ServiceViewModel();
            ServiceViewModel categories = svm.ViewServiceCategory(categoryId);
            return View(categories);
        }
        public IActionResult ServiceInfo()
        {
            return View();
        }
        public IActionResult ServicesList()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = svm.getAllCategories();
            return View(categories);
        }
        public IActionResult ServicesSubCategory(int categoryId)
        {
            ServicesSubCategoriesViewModel sscvm = new ServicesSubCategoriesViewModel();
            List<ServicesSubCategoriesViewModel> subCategories = sscvm.GetAllActiveSubCategoriesByCategoryId(categoryId);
            return View(subCategories);
        }
        public IActionResult MyServices()
        {
            return View();
        }
        public IActionResult AdminBookings()
        {
            return View();
        }
        public IActionResult AdminProviders()
        {
            return View();
        }
        public JsonResult AddServiceCategory(string name, string slug, string description)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = svm.CreateServiceCategory(name, slug, description, username);
            bool success = result == "Success";
            return Json(new { success = success, message = result });
        }
        public string UpdateServiceCategory(int categoryId, string name, string slug, string description, bool isActive)
        {
            string result = "An error occured";
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            result = svm.UpdateServiceCategory(categoryId, name, slug, description, isActive, username);
            return result;
        }
        public string DeleteServiceCategory(int categoryId)
        {
            string result = "An error occured";
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            result = svm.DeleteServiceCategory(categoryId, username);
            return result;
        }
    }
}
