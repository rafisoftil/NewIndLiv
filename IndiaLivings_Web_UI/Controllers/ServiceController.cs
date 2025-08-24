using IndiaLivings_Web_UI.Models;
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
        public IActionResult ServiceInfo()
        {
            return View();
        }
        public IActionResult ServicesList()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = svm.getAllActiveCategories();
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
    }
}
