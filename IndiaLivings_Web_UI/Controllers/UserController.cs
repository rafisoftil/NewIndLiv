
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace IndiaLivings_Web_UI.Controllers
{
    public class UserController : Controller
    {

        /// <summary>
        /// Users Dashboard Page
        /// </summary>
        /// <returns>Ads count created by user</returns>
        public IActionResult Dashboard()
        {
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            ProductViewModel productViewModel = new ProductViewModel();
            List<ProductViewModel> products = productViewModel.GetAds(productOwner);
            int productsCount = products.Count();
            HttpContext.Session.SetInt32("listingAds", productsCount);
            ViewBag.ActiveAds = productsCount;
            ViewBag.BookingAds = products.Where(p => p.productAdCategory.Equals("Booking")).ToList().Count();
            ViewBag.SalesAds = products.Where(p => p.productAdCategory.Equals("Sale")).ToList().Count();
            ViewBag.RentalAds = products.Where(p => p.productAdCategory.Equals("Rent")).ToList().Count();
            return View();
        }
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
        /// <summary>
        /// Ads List
        /// </summary>
        /// <returns> List of all Ads will be reurned</returns>
        /// // Need to be reviewed with Anoop
        public IActionResult AdsList(int categoryid = 0, int page = 1)
        {
            ProductViewModel productModel = new ProductViewModel();
            List<ProductViewModel> products = productModel.GetAds(0);
            if (categoryid != 0)
            {
                products = products.Where(product => product.productCategoryID == categoryid).ToList();
            }
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<int> wishlistIds = productModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;
            ViewBag.CurrentPage = page;
            return View(products);
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
            int wishlistCount = productModel.GetwishlistCount(productOwner);
            HttpContext.Session.SetInt32("wishlistCount", wishlistCount);
            return View(wishlist);
        }


        /// <summary>
        /// Update Wishlist Page
        /// </summary>
        /// <returns> Message on wishlist action</returns>
        public JsonResult UpdateBookmarks(int productID, string createdBy, int status)
        {
            object JsonData = null;
            ProductViewModel productModel = new ProductViewModel();
            int userID = HttpContext.Session.GetInt32("UserId") ?? 0;
            try
            {
                string response = productModel.UpdateWishlist(productID, userID, createdBy, status);
                JsonData = new { StatusCode = 200 };
            }
            catch (Exception)
            {

                throw;
            }
            return Json(JsonData);
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
        public ActionResult Profile()
        {
            string username = HttpContext.Session.GetString("userName");
            var userViewModel = new UserViewModel();
            List<UserViewModel> users = new List<UserViewModel>();
            users = userViewModel.GetUsersInfo(username);
            //ProfileViewModel profileModel = new ProfileViewModel()
            //{
            //    Users = users
            //};
            return View(users);
        }

        [HttpPost]
        public ActionResult PostAd(IFormFile productImage, IFormCollection FormData)
        {
            bool isInsert = false;
            byte[] ImageBytes = [];
            if (productImage != null)
            {
                ImageBytes = GetByteInfo(productImage);
            }
            ProductViewModel PVM = new ProductViewModel();
            PVM.productName = FormData["productName"].ToString();
            PVM.productDescription = FormData["AdDescription"].ToString();
            PVM.productAdTags = FormData["adTag"].ToString();
            PVM.productPrice = Convert.ToDecimal(FormData["productPrice"]);
            PVM.productQuantity = Convert.ToInt32(FormData["productQuantity"]);
            PVM.productCondition = FormData["product_Condition"].ToString().ToUpper() == "NEW" ? 1 : 0;
            PVM.productCategoryID = Convert.ToInt32(FormData["category"].ToString());
            PVM.byteProductImageData = ImageBytes;
            //PVM.productCategoryName = FormData[""];
            PVM.productsubCategoryID = Convert.ToInt32(FormData["subCategory"].ToString());
            //PVM.productSubCategoryName = FormData[""];
            PVM.productPriceCondition = FormData["price_Condition"];
            PVM.productAdCategory = FormData["Ad_Category"];
            PVM.productImageName = productImage != null ? productImage.FileName : "";
            
            PVM.productAdminReviewStatus = "";
            PVM.productImagePath = "";//  [];//productImage.OpenReadStream();
                                      // PVM. = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
            PVM.productSold = false;
            PVM.productOwner = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            PVM.productOwnerName = HttpContext.Session.GetString("userName");
            //PVM.productMembershipID = FormData[""];
            //PVM.productMembershipName = FormData[""];
            PVM.productAdminReview = true;
            PVM.createdDate = DateTime.Now;
            PVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            PVM.updatedDate = DateTime.Now;
            PVM.updatedBy = HttpContext.Session.GetString("userName").ToString();
            isInsert = PVM.CreateNewAdd(PVM, productImage);


            return RedirectToAction("PostAd");
        }
        public ActionResult UpdateUser(IFormFile profileImage, IFormCollection FormData)
        {
            bool isInsert = false;
            byte[] ImageBytes = [];
            if (profileImage != null)
            {
                ImageBytes = GetByteInfo(profileImage);
            }
            UserViewModel UVM = new UserViewModel();
            //UVM.user = HttpContext.Session.GetString("userName");
            UVM.userFirstName = FormData["txt_firstname"].ToString();
            UVM.userLastName = FormData["txt_middlename"].ToString();
            UVM.userMiddleName = FormData["txt_lastname"].ToString();
            UVM.password= FormData["txt_password"].ToString();
            UVM.userFullAddress = FormData["txt_address"].ToString();
            UVM.userWebsite = FormData["txt_website"].ToString();
            UVM.userMobile = FormData["txt_phone"].ToString();
            UVM.userDOB = DateTime.TryParse(FormData["txt_dob"].ToString(), out DateTime parsedDOB) ? parsedDOB : (DateTime?)null;
            UVM.userImagePath = "";
            UVM.userDescription = FormData["txt_description"].ToString();
            UVM.userEmail = FormData["txt_Email"].ToString();
            UVM.userCity = FormData["txt_city"].ToString();
            UVM.userState = FormData["txt_state"].ToString();
            UVM.userCountry = FormData["txt_Country"].ToString();
            UVM.userPinCode = FormData["txt_postcode"].ToString();
            UVM.userCompany = FormData["txt_company"].ToString();
            //UVM.userRoleID = 0;
            //UVM.userRoleName = null;
            UVM.strUserImageName = profileImage.FileName;
            UVM.byteUserImageData = "";
            UVM.strUserImageType = profileImage.FileName != "" ? profileImage.FileName.Split(".")[1] : "";
            //UVM.emailConfirmed = FormData["emailConfirmed"].ToString();
            UVM.isActive = true;
            UVM.createdDate = DateTime.Now;
            UVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            UVM.updatedDate = DateTime.Now;
            UVM.updatedBy = HttpContext.Session.GetString("userName").ToString();
            isInsert = UVM.UpdateUserProfile(UVM);
            return RedirectToAction("Settings");
        }


        public byte[] GetByteInfo(IFormFile productImage)
        {
            byte[] bytes = null;
            using (var br = new MemoryStream())
            {
                productImage.OpenReadStream().CopyTo(br);
                bytes = br.ToArray();
            }
            return bytes;
        }

        public IActionResult Settings()
        {
            string username = HttpContext.Session.GetString("userName");
            var userViewModel = new UserViewModel();
            List<UserViewModel> users = new List<UserViewModel>();
            users = userViewModel.GetUsersInfo(username);
            return View(users);
        }


    }
}




