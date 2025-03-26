using IndiaLivings_Web_DAL.Models;
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
        /// <summary>
        /// Users WishList Page
        /// </summary>
        /// <returns> List of all wishlists will be reurned</returns>
        public IActionResult Bookmark()
        {
            ProductViewModel productModel = new ProductViewModel();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<ProductViewModel> wishlist = productModel.GetAllWishlist(productOwner);    
            return View(wishlist);
        }
        /// <summary>
        /// My Ads Page
        /// </summary>
        /// <returns> Ads created by User </returns>
        public IActionResult MyAds()
        {
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            ProductViewModel productModel = new ProductViewModel();
            List<ProductViewModel> products = productModel.GetAds(productOwner);
            return View(products);
        }

        [HttpPost]
        public ActionResult PostAd(IFormFile productImage, IFormCollection FormData)
        {
            bool isInsert = false;
            ProductViewModel PVM = new ProductViewModel();
            PVM.productName = FormData["productName"].ToString();
            PVM.productDescription = FormData["AdDescription"].ToString();
            PVM.productAdTags = FormData["adTag"].ToString();
            PVM.productPrice = Convert.ToDecimal(FormData["productPrice"]);
            PVM.productQuantity = Convert.ToInt32(FormData["productQuantity"]);
            PVM.productCondition = FormData["product_Condition"].ToString().ToUpper() == "NEW" ? 1 : 0;
            PVM.productCategoryID = Convert.ToInt32(FormData["category"].ToString());
            //PVM.productCategoryName = FormData[""];
            PVM.productsubCategoryID = Convert.ToInt32(FormData["subCategory"].ToString());
            //PVM.productSubCategoryName = FormData[""];
            PVM.productPriceCondition = FormData["price_Condition"];
            PVM.productAdCategory = FormData["Ad_Category"];
            PVM.productImageName = productImage.FileName;
            PVM.productAdminReviewStatus = "";
            PVM.productImagePath = "";//  [];//productImage.OpenReadStream();
            PVM.productImageType = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
            PVM.productSold = false;
            PVM.productOwner = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            PVM.productOwnerName = HttpContext.Session.GetString("userName");
            //PVM.productMembershipID = FormData[""];
            //PVM.productMembershipName = FormData[""];
            PVM.productAdminReview = true;
            PVM.createdDate = DateTime.Now;
            PVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            PVM.updatedDate = DateTime.Now; 
            PVM.updatedBy = HttpContext.Session.GetString("userName").ToString();
            isInsert = PVM.CreateNewAdd(PVM);

            
            return RedirectToAction("PostAd");
        }

    }
}
