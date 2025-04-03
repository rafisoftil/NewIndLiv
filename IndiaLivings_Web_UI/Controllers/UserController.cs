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
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
            var profileViewModel = new ProfileViewModel
            {
                Users = users,

            };
            return View(profileViewModel);
        }



        //public IActionResult Settings(UserModel user)
        //{
        //    // Initialize the UserViewModel
        //    UserViewModel userModel = new UserViewModel();

        //    // Call UpdateUserDetails to get the updated UserViewModel
        //    UserViewModel userProfile = userModel.UpdateUserProfile(user);

        //    // Check if the userProfile is null
        //    if (userProfile == null)
        //    {
        //        // Handle the case where userProfile is null (e.g., return a specific error message or view)
        //        ViewBag.ErrorMessage = "User profile could not be loaded.";
        //        return View(); // Or you can return a specific view indicating failure
        //    }

        //    // Return the updated user profile to the view
        //    return View(userProfile);
        //}

        //public ActionResult UpdateProfile(UserModel user)
        //{
        //    UserViewModel userViewModel = new UserViewModel();

        //    string response = string.Empty;

        //    try
        //    {
        //        // Call the UpdateUserProfile method to update the user profile
        //        response = userViewModel.UpdateUserDetails(user);

        //        // Check if the response is successful or not
        //        if (!string.IsNullOrEmpty(response) && response.Contains("error"))
        //        {
        //            // Optionally, you can set a failure message in ViewBag or TempData
        //            ViewBag.Message = "There was an error updating the profile.";
        //        }
        //        else
        //        {
        //            // Optionally, set a success message
        //            ViewBag.Message = "Profile updated successfully.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error and set failure message
        //        ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
        //        ViewBag.Message = "An unexpected error occurred.";
        //    }


        //    return View();
        //}
        public async Task<ActionResult> UpdateProfile(string firstName, string lastName, string middleName, string address, string website,
                                      string phone, string birthday, string profileImg, string description, string email,
                                      [FromForm] IFormFile userProfileImg)
        {
            //UserViewModel userModel = HttpContext.Session.GetObject<UserViewModel>("user");
            string username = HttpContext.Session.GetString("userName");
            var userModel = new UserViewModel();
            List<UserViewModel> users = new List<UserViewModel>();
            users = userModel.GetUsersInfo(username);
            if (userModel == null)
                return BadRequest("User not found.");

            // Update the user details
            userModel.userID = 0;
            userModel.username = username;
            userModel.password = null;
            userModel.userFirstName = firstName;
            userModel.userLastName = lastName;
            userModel.userMiddleName = middleName;
            userModel.userFullAddress = address;
            userModel.userWebsite = website;
            userModel.userMobile = phone;
            userModel.userDOB = Convert.ToDateTime(birthday);
            userModel.userImagePath = profileImg;
            userModel.userDescription = description;
            userModel.userEmail = email;
            //userModel.userCity = null;
            //userModel.userState = null;
            //userModel.userCountry = null;
            //userModel.userPinCode = null;
            //userModel.userRoleID = 0;
            //userModel.userRoleName = null;
            //userModel.strUserImageName = null;
            //userModel.byteUserImageData = null;
            //userModel.strUserImageType = null;
            //userModel.emailConfirmed = null;
            //userModel.isActive = true;
            //userModel.createdDate = "2025-04-03T08:38:56.006Z";
            //userModel.createdBy = null;
            //userModel.updatedDate = "2025-04-03T08:38:56.006Z";
            //userModel.updatedBy = "null";



            string imageStatus = userModel.userImagePath;

            // Handle image upload if there is a new image
            if (userProfileImg != null)
            {
                imageStatus = HandleImageUpload(userProfileImg, userModel.userID.ToString());

                if (imageStatus == "Invalid_file_type")
                {
                    return BadRequest("Invalid file type. Only .jpg, .jpeg, .png, .gif are allowed.");
                }
                else if (imageStatus == "File_size_exceed")
                {
                    return BadRequest("File size exceeds 1 MB.");
                }
            }

            userModel.userImagePath = imageStatus;

            try
            {
                // Await the result from the UpdateUserProfile method
                var result = userModel.UpdateUserProfile(userModel);

                if (result == "User Profile Updated Successfully.")
                {
                    // Serialize and save updated user to session
                    HttpContext.Session.SetObject("user", userModel);

                    // Delete previous image if the profile image is updated
                    if (userProfileImg != null && profileImg != userModel.userImagePath)
                    {
                        DeleteUserImage(profileImg, userModel.userID.ToString());
                    }

                    return Ok("Profile updated successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (this part is important for debugging)

                return BadRequest($"An error occurred: {ex.Message}");
            }

            return BadRequest("Failed to update profile.");
        }

        //// Handle image upload logic
        private string HandleImageUpload(IFormFile profileImage, string userId)
        {
            return UploadUserImage(profileImage, userId);
        }

        // Upload User Profile Image
        public string UploadUserImage(IFormFile userImage, string userId)
        {
            var userProfileSettings = _configuration.GetSection("File_Paths:User_Profile");
            string imgPath = userProfileSettings["Img_Path"];
            int maxFileSizeMB = int.Parse(userProfileSettings["File_Size"]);
            var allowedExtensions = userProfileSettings.GetSection("Extensions").Get<List<string>>();

            if (userImage.Length <= maxFileSizeMB * 1024 * 1024)
            {
                string fileExtension = Path.GetExtension(userImage.FileName)?.ToLower();
                if (allowedExtensions.Contains(fileExtension))
                {
                    string fileName = userImage.FileName;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), imgPath, userId, fileName);
                    string directoryPath = Path.GetDirectoryName(filePath);

                    // Check and create the directory path if not exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        userImage.CopyTo(stream);
                    }

                    return fileName;
                }
                else
                {
                    return "Invalid_file_type";
                }
            }
            else
            {
                return "File_size_exceed";
            }
        }

        // Delete User Image
        private string DeleteUserImage(string filePath, string userId)
        {
            var imgPath = _configuration["File_Paths:User_Profile:Img_Path"];
            try
            {
                var fullPath = Path.Combine(imgPath, userId, filePath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return "File deleted successfully.";
                }
                return "File not found.";
            }
            catch (Exception ex)
            {

                return $"Error: {ex.Message}";
            }
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




