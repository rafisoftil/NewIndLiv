using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public JsonResult AddServiceCategory(string name, string slug, string description, string image)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = svm.CreateServiceCategory(name, slug, description, image, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult UpdateServiceCategory(int categoryId, string name, string slug, string description, bool isActive)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = svm.UpdateServiceCategory(categoryId, name, slug, description, isActive, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult DeleteServiceCategory(int categoryId)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            var result = svm.DeleteServiceCategory(categoryId, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
    }
}
