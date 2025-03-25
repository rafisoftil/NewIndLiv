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
            List<AdConditionViewModel> adConitions = new List<AdConditionViewModel>();
            adConitions = adCondition.GetAllAdConditionsTypeName("");
            var priceConditions = adConitions.Where(x => x.strAdConditionType.Equals("Price Condition")).ToList();
            var Ad_Categories = adConitions.Where(x => x.strAdConditionType.Equals("Ad Category")).ToList();
            var productConditions = adConitions.Where(x => x.strAdConditionType.Equals("Product Condition")).ToList();
            dynamic data = new ExpandoObject();
            data.Categories = categoryList;
            data.priceConditions = priceConditions;
            data.Ad_Categories = Ad_Categories;
            data.productConditions = productConditions;
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
            List<SubCategoryViewModel> subCategories = new List<SubCategoryViewModel>();
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

        [HttpPost]
        public ActionResult PostAd(IFormFile productImage, IFormCollection FormData)
        {
            AdViewModel ad = new AdViewModel();
            ad.productName = FormData["productName"].ToString();
            ad.productDescription = FormData["AdDescription"].ToString();
            ad.productAdTags = FormData["adTag"].ToString();
            ad.productPrice = Convert.ToDecimal(FormData["productPrice"]);
            ad.productQuantity = FormData[""];
            ad.productCondition = FormData[""];
            ad.productCategoryID = FormData[""];
            ad.productCategoryName = FormData[""];
            ad.productsubCategoryID = FormData[""];
            ad.productSubCategoryName = FormData[""];
            ad.productPriceCondition = FormData[""];
            ad.productAdCategory = FormData[""];
            ad.strProductImageName = productImage.FileName;
            ad.byteProductImageData =  [];//productImage.OpenReadStream();
            ad.strProductImageType = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
            ad.productSold = false;
            ad.productOwner = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            ad.productOwnerName = HttpContext.Session.GetString("userID");
            ad.productMembershipID = FormData[""];
            ad.productMembershipName = FormData[""];
            ad.productAdminReview = FormData[""];
            ad.createdDate = DateTime.Now;
            ad.createdBy = HttpContext.Session.GetString("userName").ToString();
            ad.updatedDate = DateTime.Now; ;
            ad.updatedBy = HttpContext.Session.GetString("userName").ToString();
            var productname =
            return View();
        }

    }
}
