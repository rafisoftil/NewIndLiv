using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Globalization;

namespace IndiaLivings_Web_UI.Controllers
{
    public class UserController : Controller
    {
        //private readonly IConfiguration _configuration;
        //public UserController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
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
        public ActionResult Profile()
        {
            string username = HttpContext.Session.GetString("userName");
            var userViewModel = new UserViewModel();
            List<UserViewModel> users = new List<UserViewModel>();
            users = userViewModel.GetUsersInfo(username);             
            return View(users);
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
        public ActionResult upadteProfile(IFormFile profileImage, IFormCollection FormData)
        {
            bool isInsert = false;
            UserViewModel UVM = new UserViewModel();
            UVM.userFirstName = FormData["userFirstName"].ToString();
            UVM.userLastName = FormData["userLastName"].ToString();
            UVM.userMiddleName = FormData["userMiddleName"].ToString();
            UVM.userFullAddress = FormData["userFullAddress"].ToString();
            UVM.userWebsite = FormData["userWebsite"].ToString();
            UVM.userMobile = FormData["userMobile"].ToString();
            UVM.userDOB = DateTime.TryParse(FormData["userDOB"].ToString(), out DateTime parsedDOB) ? parsedDOB : (DateTime?)null;
            UVM.userImagePath = "";
            UVM.userDescription = FormData["userDescription"].ToString();
            UVM.userEmail = FormData["userEmail"].ToString();
            UVM.userCity = FormData["userCity"].ToString();
            UVM.userState = FormData["userState"].ToString();
            UVM.userCountry = FormData["userCountry"].ToString();
            UVM.userPinCode = Convert.ToInt32(FormData["userPinCode"].ToString());
            //UVM.userRoleID = 0;
            //UVM.userRoleName = null;
            UVM.strUserImageName = profileImage.FileName;
            UVM.byteUserImageData = "";
            UVM.strUserImageType = profileImage.FileName != "" ? profileImage.FileName.Split(".")[1] : "";
            UVM.emailConfirmed = FormData["emailConfirmed"].ToString();
            UVM.isActive = true;
            UVM.createdDate = DateTime.Now;
            UVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            UVM.updatedDate = DateTime.Now;
            UVM.updatedBy = HttpContext.Session.GetString("userName").ToString();
            isInsert = UVM.UpdateUserProfile(UVM);
            return RedirectToAction("Settings");
        }




            //public async Task<ActionResult> UpdateProfile(IFormCollection FormData,  IFormFile userProfileImg)
            //{
            //    //UserViewModel userModel = HttpContext.Session.GetObject<UserViewModel>("user");
            //    string username = HttpContext.Session.GetString("userName");
            //    var userModel = new UserViewModel();
            //    List<UserViewModel> users = new List<UserViewModel>();
            //    users = userModel.GetUsersInfo(username);
            //    if (userModel == null)
            //        return BadRequest("User not found.");
            //    UserViewModel UVM = new UserViewModel();
            //    // Update the user details

            //    UVM.username = FormData["username"].ToString();
            //   // UVM.password = null;
            //    UVM.userFirstName = FormData["userFirstName"].ToString(); 
            //    UVM.userLastName = FormData["userLastName"].ToString();
            //    UVM.userMiddleName = FormData["userMiddleName"].ToString();
            //    UVM.userFullAddress = FormData["userFullAddress"].ToString();
            //    UVM.userWebsite = FormData["userWebsite"].ToString();
            //    UVM.userMobile = FormData["userMobile"].ToString();
            //    UVM.userDOB = DateTime.TryParse(FormData["userDOB"].ToString(), out DateTime parsedDOB)? parsedDOB: (DateTime?)null;
            //    UVM.userImagePath = FormData["userImagePath"].ToString();
            //    UVM.userDescription = FormData["userDescription"].ToString();
            //    UVM.userEmail = FormData["userEmail"].ToString();
            //    UVM.userCity = FormData["userCity"].ToString();
            //    UVM.userState = FormData["userState"].ToString();
            //    UVM.userCountry = FormData["userCountry"].ToString();
            //    UVM.userPinCode = Convert.ToInt32(FormData["userPinCode"].ToString());
            //    UVM.userRoleID = 0;
            //    UVM.userRoleName = null;
            //    UVM.strUserImageName = FormData["strUserImageName"].ToString();
            //    UVM.byteUserImageData = FormData["byteUserImageData"].ToString();
            //    UVM.strUserImageType = FormData["strUserImageType"].ToString();
            //    UVM.emailConfirmed = FormData["emailConfirmed"].ToString();
            //    UVM.isActive = true;
            //    UVM.createdDate = DateTime.Now;
            //    UVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            //    UVM.updatedDate = DateTime.Now;
            //    UVM.updatedBy = HttpContext.Session.GetString("userName").ToString();

            //    string imageStatus = userModel.userImagePath;
            //    // Handle image upload if there is a new image
            //    if (userProfileImg != null)
            //    {
            //        imageStatus = HandleImageUpload(userProfileImg, userModel.userID.ToString());

            //        if (imageStatus == "Invalid_file_type")
            //        {
            //            return BadRequest("Invalid file type. Only .jpg, .jpeg, .png, .gif are allowed.");
            //        }
            //        else if (imageStatus == "File_size_exceed")
            //        {
            //            return BadRequest("File size exceeds 1 MB.");
            //        }
            //    }
            //    userModel.userImagePath = imageStatus;

            //    try
            //    {
            //        var result = userModel.UpdateUserProfile(userModel);
            //        if (result == "User Profile Updated Successfully.")
            //        {
            //            HttpContext.Session.SetObject("user", userModel);
            //            //if (userProfileImg != null && userProfileImg != userModel.userImagePath)
            //            //{
            //            //    DeleteUserImage(userProfileImg, userModel.userID.ToString());
            //            //}

            //            return Ok("Profile updated successfully.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest($"An error occurred: {ex.Message}");
            //    }
            //    return BadRequest("Failed to update profile.");
            //}

            ////// Handle image upload logic
            //private string HandleImageUpload(IFormFile profileImage, string userId)
            //{
            //    return UploadUserImage(profileImage, userId);
            //}

            //// Upload User Profile Image
            //public string UploadUserImage(IFormFile userImage, string userId)
            //{
            //    var userProfileSettings = _configuration.GetSection("File_Paths:User_Profile");
            //    string imgPath = userProfileSettings["Img_Path"];
            //    int maxFileSizeMB = int.Parse(userProfileSettings["File_Size"]);
            //    var allowedExtensions = userProfileSettings.GetSection("Extensions").Get<List<string>>();

            //    if (userImage.Length <= maxFileSizeMB * 1024 * 1024)
            //    {
            //        string fileExtension = Path.GetExtension(userImage.FileName)?.ToLower();
            //        if (allowedExtensions.Contains(fileExtension))
            //        {
            //            string fileName = userImage.FileName;
            //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), imgPath, userId, fileName);
            //            string directoryPath = Path.GetDirectoryName(filePath);

            //            // Check and create the directory path if not exists
            //            if (!Directory.Exists(directoryPath))
            //            {
            //                Directory.CreateDirectory(directoryPath);
            //            }

            //            // Save the file
            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                userImage.CopyTo(stream);
            //            }

            //            return fileName;
            //        }
            //        else
            //        {
            //            return "Invalid_file_type";
            //        }
            //    }
            //    else
            //    {
            //        return "File_size_exceed";
            //    }
            //}

            //// Delete User Image
            //private string DeleteUserImage(string filePath, string userId)
            //{
            //    var imgPath = _configuration["File_Paths:User_Profile:Img_Path"];
            //    try
            //    {
            //        var fullPath = Path.Combine(imgPath, userId, filePath);
            //        if (System.IO.File.Exists(fullPath))
            //        {
            //            System.IO.File.Delete(fullPath);
            //            return "File deleted successfully.";
            //        }
            //        return "File not found.";
            //    }
            //    catch (Exception ex)
            //    {

            //        return $"Error: {ex.Message}";
            //    }
            //}


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




