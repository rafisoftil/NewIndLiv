using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace IndiaLivings_Web_UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult PostAd()
        {
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            AdConditionViewModel adCondition = new AdConditionViewModel();
            List<AdConitionTypeViewModel> adConitions = new List<AdConitionTypeViewModel>();
            adConitions = adCondition.GetAllAdConditionsTypeName("");
            dynamic data = new ExpandoObject();
            data.Categories = categoryList;
            data.AdConitions = adConitions;
            return View(data);
        }
        public IActionResult AdsList()
        {
            return View();
        }
        public JsonResult GetSubCategories(int Category)
        {
            object JsonData = null;
            SubCategoryViewModel subCategory = new SubCategoryViewModel();
            List<SubCategoryViewModel> subCategories= new List<SubCategoryViewModel>();
            try
            {
                subCategories = subCategory.GetSubCategories(Category);
                JsonData = new
                {
                    StatusCode = 200,
                    SubCategories = subCategories
                };
            }
            catch (Exception ex)
            {
                
            }
            return Json(JsonData);
        }

    }
}
