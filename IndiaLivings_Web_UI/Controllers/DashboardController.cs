using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Mail;
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
            ProductViewModel product = new ProductViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            List<ProductViewModel> productsList = product.GetAds(0);
            int productOwnerID = HttpContext.Session.GetInt32("UserId") ?? 0;
            int wishlistCount = product.GetwishlistCount(productOwnerID);

            HttpContext.Session.SetInt32("wishlistCount", wishlistCount);
            if (productOwnerID != 0)
            {
                List<ProductViewModel> products = product.GetAds(productOwnerID);
                int productsCount = products.Count();
                HttpContext.Session.SetInt32("listingAds", productsCount);
            }
            dynamic data = new ExpandoObject();
            data.Categories = categoryList;
            data.Products = productsList;
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
        public JsonResult RegisterUser(string userName, string password)
        {
            object JsonData = null;
            UserViewModel user = new UserViewModel();
            user.username = userName;
            user.password = password;
            bool isRegistered = false;
            try
            {
                isRegistered = user.RegisterUser(user);
                if (isRegistered)
                {
                    JsonData = new
                    {
                        StatusCode = 200
                    };
                }

            }
            catch (Exception ex)
            {
            }
            return Json(JsonData);
        }
        /// <summary>
        /// 

        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult verifyUserName(string userName)
        {
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
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("Role", "");
            HttpContext.Session.SetString("UserFullName", "");
            if (user != null)
            {
                HttpContext.Session.SetObject("UserDetails", user);
                HttpContext.Session.SetString("userName", user.username);
                HttpContext.Session.SetInt32("RoleId", user.userRoleID);
                HttpContext.Session.SetString("Role", user.userRoleName);
                HttpContext.Session.SetInt32("UserId", user.userID);
                HttpContext.Session.SetObject("UserImage", user.byteUserImageData);
                HttpContext.Session.SetString("UserFullName", user.userFirstName + " " + user.userMiddleName + " " + user.userLastName);
                HttpContext.Session.SetString("Mobile", user.userMobile);
                HttpContext.Session.SetString("Email", user.userEmail);
                HttpContext.Session.SetString("Address", user.userFullAddress);
                HttpContext.Session.SetString("City", user.userCity);
                HttpContext.Session.SetString("State", user.userState);
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
        /// User Varification
        /// </summary>
        /// <returns>To verify whether User is existing or not</returns>
        //public bool CheckUserExists(string email)
        //{
        //    UserViewModel user = new UserViewModel();

        //    if (userInfo.Count() > 0)
        //    {
        //        HttpContext.Session.SetInt32("UserID", userInfo[0].userID);
        //        HttpContext.Session.SetString("UserName", userInfo[0].username);
        //        HttpContext.Session.SetString("UserEmail", userInfo[0].userEmail.ToString());
        //        HttpContext.Session.SetString("UserPhone", userInfo[0].userMobile.ToString());
        //        HttpContext.Session.SetString("UserFirstName", userInfo[0].userFirstName.ToString());
        //    }
        //    return (userInfo.Count != 0) ? true : false;
        //}

        /// <summary>
        /// Send Email to User
        /// </summary>
        /// <returns>Password Reset link to the user mail</returns>
        public IActionResult SendEmail(string email)
        {

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string senderEmail = configuration["EmailSender:Email"]; // Replace with your email
            string senderPassword = configuration["EmailSender:Password"]; // Replace with your email password
            string smtpHost = configuration["EmailSender:smtp"]; // Gmail SMTP host
            int smtpPort = Convert.ToInt32(configuration["EmailSender:port"]); // TLS port for Gmail
            string token = Guid.NewGuid().ToString();

            //string resetLink = $"{Request.Scheme}://{Request.Host}/Dashboard/ForgotPassword/{userid}";
            PasswordResetViewModel passwordReset = new PasswordResetViewModel();
            UserViewModel user = new UserViewModel();
            string message = string.Empty;
            try
            {

                List<UserViewModel> userInfo = user.GetUsersInfo(email);
                int userid = userInfo[0].userID;
                string username = userInfo[0].username;
                string createdby = userInfo[0].username;
                DateTime expirationtime = DateTime.Now.AddMinutes(30);
                string formattedExpirationTime = expirationtime.ToString("yyyy-MM-dd HH:mm:ss");
                string response = passwordReset.AddPasswordReset(userid, username, token, formattedExpirationTime, username);

                if (response.Contains("Record inserted"))
                {
                    string resetLink = Url.Action("ForgotPassword", "Dashboard", new { token = token }, Request.Scheme);
                    //string resetLink = Url.Action("ForgotPassword", "Dashboard", new { userid = userid, username = username, token = token }, Request.Scheme);
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = "Your link for Password Reset",
                        Body = $"Please reset your password by clicking this link: {resetLink}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(email);

                    using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mailMessage);
                        message = $"Password link sent successfully to {email}";
                    }

                }
                else
                {
                    message = $"Error while sending password. Please try again";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return Json(new { success = true, message = message });
        }

        /// <summary>
        /// Reset Password Path
        /// </summary>
        /// <returns>Forgot Password Modal</returns>
        [Route("Dashboard/ForgotPassword/{token}")]
        public IActionResult ForgotPassword(string token)
        {
            PasswordResetViewModel passwordReset = new PasswordResetViewModel();
            List<PasswordResetViewModel> resetInfo = passwordReset.GetPasswordReset(token);
            if (resetInfo[0].UserTokenExpiration < DateTime.Now)
            {
                return Json(new { message = "This link is expired. Please request for new link." });
            }
            ViewBag.token = token;
            return View();
        }

        /// <summary>
        /// Resets password through link
        /// </summary>
        /// <returns>Password reset status</returns>
        public IActionResult ResetPassword(string newPassword, string token)
        {
            PasswordResetViewModel passwordReset = new PasswordResetViewModel();
            string reset = passwordReset.PasswordReset(newPassword, token);
            return Json(new { message = reset });
        }

        /// <summary>
        /// Validates current password while password update
        /// </summary>
        /// <returns>Password correct or not</returns>
        public IActionResult ValidateUpdatePassword(string userName, string password)
        {
            dynamic JsonData = null;
            UserViewModel user = new UserViewModel();
            user = user.ValidateUser(userName, password);
            if (user.userID != 0)
            {
                JsonData = new
                {
                    StatusCode = 200,
                    userId = user.userID
                };
            }
            else
            {
                JsonData = new
                {
                    StatusCode = 400,
                    userId = 0
                };
            }
            return Json(JsonData);
        }
        /// <summary>
        /// Update Password from Login page
        /// </summary>
        /// <returns>Update Password Status</returns>
        public IActionResult UpdatePassword(int userId, string newPassword)
        {
            PasswordResetViewModel passwordReset = new PasswordResetViewModel();
            string reset = passwordReset.UpdatePassword(userId, newPassword);
            return Json(new { message = reset });
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
            MembershipViewModel membModel = new MembershipViewModel();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<MembershipViewModel> memDetails = membModel.GetMembershipDetails(userId);
            return View(userList.ToList());
        }

        /// <summary>
        /// Review Ads Page
        /// </summary>
        /// <returns> List of all Ads to be reviewed </returns>
        public IActionResult ReviewAds()
        {
            try
            {
                ProductViewModel productModel = new ProductViewModel();
                List<ProductViewModel> products = productModel.AdsList(1);
                return View(products);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Manage Ads Page
        /// </summary>
        /// <returns> List of all Ads (Status of active ads can be changed) </returns>
        public IActionResult ManageAds()
        {
            try
            {
                ProductViewModel productModel = new ProductViewModel();
                List<ProductViewModel> products = productModel.AdsList(0);
                return View(products);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Review Ad
        /// </summary>
        /// <returns> Changed Status </returns>
        public IActionResult UpdateAdAdminReview(int productid, bool status)
        {
            try
            {
                ProductViewModel productModel = new ProductViewModel();
                string updatedBy = HttpContext.Session.GetString("userName");
                var reviewStatus = productModel.UpdateAdStatus(productid, status, updatedBy);
                return Json(new { message = reviewStatus });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Image Details Page
        /// </summary>
        /// <returns> Get image in bytes </returns>
        public IActionResult GetImageDetails(int productid)
        {
            try
            {
                ProductImageDetailsViewModel productImageDetails = new ProductImageDetailsViewModel();
                List<ProductImageDetailsViewModel> imageDetails = productImageDetails.GetImage(productid);

                if (imageDetails.Any())
                {
                    var image = imageDetails.FirstOrDefault().byteProductImageData;
                    string base64String = Convert.ToBase64String(image);
                    return Json(new { image = base64String });
                }
                else
                {
                    return Json(new { image = "" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { image = "" });
            }
        }

        /// <summary>
        /// User Image
        /// </summary>
        /// <returns>UserImage in Bytes</returns>
        public IActionResult GetUserImage()
        {
            var imageBytes = HttpContext.Session.GetObject<byte[]>("UserImage");
            if (imageBytes != null)
            {
                return File(imageBytes, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Product Image
        /// </summary>
        /// <returns>ProductImage in Bytes</returns>
        public IActionResult GetProductImage(int productid)
        {
            try
            {
                ProductImageDetailsViewModel productImageDetails = new ProductImageDetailsViewModel();
                List<ProductImageDetailsViewModel> imageDetails = productImageDetails.GetImage(productid);

                if (imageDetails.Any())
                {
                    var image = imageDetails.FirstOrDefault().byteProductImageData;
                    return File(image, "image/jpeg");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
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
        /// <summary>
        /// Membership
        /// </summary>
        /// <returns>All types of Memberships available</returns>
        public IActionResult Membership()
        {
            MembershipViewModel membershipViewModel = new MembershipViewModel();
            List<MembershipViewModel> Memberships = membershipViewModel.GetAllListofMembership(0);
            return View(Memberships);
        }
        /// <summary>
        /// Categories
        /// </summary>
        /// <returns>List of Categories and Subcategories</returns>
        public IActionResult Categories()
        {
            CategoryViewModel categoryModel = new CategoryViewModel();
            List<CategoryViewModel> categoriesList = categoryModel.GetCategoryCount();
            return View(categoriesList);
        }
        /// <summary>
        /// Updates Manage Users Page
        /// </summary>
        /// <returns> User update status </returns>
        public IActionResult UpdateUser([FromBody] UserViewModel user)
        {
            try
            {
                UserViewModel userViewModel = new UserViewModel();
                var response = userViewModel.UpdateUser(user);
                return Json(new { message = response });
            }
            catch (Exception)
            {

                return Json(new { message = "User update unsuccessful" });
            }
        }
        /// <summary>
        /// Membership
        /// </summary>
        /// <returns>Membership details of User</returns>
        public IActionResult MembershipDetails(int userid)
        {
            MembershipViewModel membershipViewModel = new MembershipViewModel();
            List<MembershipViewModel> Membership = membershipViewModel.GetMembershipDetails(userid);
            if (Membership.Count() > 0)
            {
                MembershipViewModel Memberships = membershipViewModel.GetMembershipDetails(userid)[0];
                return Json(new { Memberships });
            }
            return Json(new { membershipViewModel = "" });
        }
        public IActionResult CategoryMainPage(int userid)
        {
            //int userid = int.Parse(HttpContext.Session.GetString("UserID"));
            CategoryViewModel category = new CategoryViewModel();
            SubCategoryViewModel subCategory = new SubCategoryViewModel();
            // To Get All the list of Categories
            List<CategoryViewModel> Categories = category.GetCategoryCount();
            //To Get All the list of Sub Categories
            List<SubCategoryViewModel> SubCategories = subCategory.GetSubCategories(0);
            dynamic Products = new ExpandoObject();
            Products.Categories = Categories;
            Products.SubCategories = SubCategories;
            return View(Products);
        }
        /// <summary>
        /// Update Membership Details
        /// </summary>
        /// <param name="intMembershipID"></param>
        /// <param name="strMembershipName"></param>
        /// <param name="intMembershipAdsLimit"></param>
        /// <param name="decMembershipPrice"></param>
        /// <param name="strMembershipDescription"></param>
        /// <param name="strUpdatedBy"></param>
        /// <returns> Status of the Update Membership </returns>
        public IActionResult UpdateMembership(int intMembershipID, string strMembershipName, int intMembershipAdsLimit, double decMembershipPrice, string strMembershipDescription, string strUpdatedBy)
        {
            try
            {
                MembershipViewModel userViewModel = new MembershipViewModel();
                var response = userViewModel.UpdateMembership(intMembershipID, strMembershipName, intMembershipAdsLimit, decMembershipPrice, strMembershipDescription, strUpdatedBy);
                return Json(new { message = response });
            }
            catch (Exception)
            {

                return Json(new { message = "Membership update unsuccessful" });
            }
        }
        /// <summary>
        /// Delete Membership
        /// </summary>
        /// <param name="intMembershipID"></param>
        /// <returns>Status from Delete Memebership API</returns>
        public IActionResult DeleteMembership(int intMembershipID)
        {
            try
            {
                string updatedBy = HttpContext.Session.GetString("userName");
                MembershipViewModel membershipViewModel = new MembershipViewModel();
                var response = membershipViewModel.DeleteMembership(intMembershipID, updatedBy);
                return Json(new { message = response });
            }
            catch (Exception)
            {
                return Json(new { message = "Membership deletion unsuccessful" });
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Login");
        }
    }
}
