using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;
using System.Net.Mail;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace IndiaLivings_Web_UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }
        /// <summary>
        /// Landing Page
        /// </summary>
        /// <returns>Initially, this page will be the loaded</returns>
        public async Task<IActionResult> Dashboard()
        {
            CategoryViewModel category = new CategoryViewModel();
            ProductViewModel product = new ProductViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            List<ProductViewModel> productsList = product.GetAds(0);
            List<ProductViewModel> RatedProducts = productsList.Where(product => product.averageRating >= 4).OrderByDescending(x => x.averageRating).ToList();
            List<ProductViewModel> recommendedList = productsList.Where(product => product.productMembershipID == 2).ToList();
            List<ProductViewModel> trendingAds = await new ProductViewModel().GetRecommendedAds(6, 4, true);
            int productOwnerID = HttpContext.Session.GetInt32("UserId") ?? 0;
            int wishlistCount = product.GetwishlistCount(productOwnerID);
            SearchFilterDetailsViewModel searchFilterDetails = new SearchFilterDetailsViewModel();
            List<SearchFilterDetailsViewModel> filDetails = searchFilterDetails.GetSearchFilterDetails();
            CompanySetupViewModel companySetup = await new CompanySetupViewModel().GetCompanySetupById(1);
            List<int> wishlistIds = product.GetAllWishlist(productOwnerID).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;
            HttpContext.Session.SetInt32("wishlistCount", wishlistCount);
            if (productOwnerID != 0)
            {
                //List<ProductViewModel> products = product.GetAds(productOwnerID);
                //int productsCount = products.Count();
                //HttpContext.Session.SetInt32("listingAds", productsCount);
                AdsByMembershipViewModel adsRem = new AdsByMembershipViewModel();
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                List<AdsByMembershipViewModel> adData = adsRem.GetUserAdsRemaining(userId);
                if (adData.Count > 0)
                {
                    HttpContext.Session.SetInt32("listingAds", adData[0].userTotalAdsPosted);
                    HttpContext.Session.SetInt32("remainingAds", adData[0].userTotalAdsRemaining);
                    HttpContext.Session.SetInt32("pendingAds", adData[0].userMembershipAds - adData[0].userTotalAdsRemaining);
                    ViewBag.AdsRemaining = adData[0].userTotalAdsRemaining;
                }
            }
            dynamic data = new ExpandoObject();
            data.Categories = categoryList.OrderByDescending(x => x.CategoryCount).ToList();
            data.Products = productsList;
            data.RatedProducts = RatedProducts;
            data.RecommendedAds = recommendedList;
            data.trendingAds = trendingAds;
            data.Cities = filDetails.Where(x => x.CategoryType.ToLower().Equals("cities")).OrderByDescending(x => x.totalCount).ToList();
            data.CompanySetup = companySetup;
            HttpContext.Session.SetString("CompanyName", companySetup.companyName);
            HttpContext.Session.SetString("CompanyEmail", companySetup.email);
            HttpContext.Session.SetString("CompanyPhone", companySetup.phone);
            HttpContext.Session.SetString("CompanyAddress", companySetup.address);
            HttpContext.Session.SetString("CompanyCity", companySetup.city);
            HttpContext.Session.SetString("CompanyState", companySetup.state);
            HttpContext.Session.SetString("CompanyCountry", companySetup.country);
            HttpContext.Session.SetString("CompanyWebsite", companySetup.website);
            HttpContext.Session.SetString("AboutUs", companySetup.aboutUs);
            HttpContext.Session.SetString("ContactUs", companySetup.contactUs);
            return View(data);
        }

        /// <summary>
        /// Sign Up / Sign In Page
        /// </summary>
        /// <returns>once User clicks on "Join Me" Login page will be loaded</returns>
        public IActionResult Login()
        {
            ViewBag.UsernameFromCookie = Request.Cookies["UsernameFromCookie"] ?? "";
            ViewBag.PasswordFromCookie = Request.Cookies["PasswordFromCookie"] ?? "";
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
        public JsonResult Login(string userName, string password, bool RememberMe = false)
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
                if (RememberMe)
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        IsEssential = true,
                        HttpOnly = true,
                        Secure = true
                    };
                    Response.Cookies.Append("UsernameFromCookie", userName, cookieOptions);
                    Response.Cookies.Append("PasswordFromCookie", password, cookieOptions);
                }
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

        public async Task<IActionResult> SendSubscribeEmail(string email)
        {

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string senderEmail = configuration["EmailSender:Email"]; // Replace with your email
            string senderPassword = configuration["EmailSender:Password"]; // Replace with your email password
            string smtpHost = configuration["EmailSender:smtp"]; // Gmail SMTP host
            int smtpPort = Convert.ToInt32(configuration["EmailSender:port"]); // TLS port for Gmail
            string token = Guid.NewGuid().ToString();
            string message = "Subscribe link is not sent";

            EmailSubscriptionViewModel sub = new EmailSubscriptionViewModel
            {
                Email = email,
                FullName = "",
            };
            string response = await new EmailSubscriptionViewModel().Subscribe(sub);
            try
            {
                string resetLink = Url.Action("VerifySubscription", "Dashboard", new { token = token }, Request.Scheme);
                string unsubscribeLink = Url.Action("Unsubscribe", "Dashboard", new { token = token }, Request.Scheme);
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Your link for Password Reset",
                    IsBodyHtml = true,
                    Body = $@"
                        <!DOCTYPE html>
                        <html>
                        <body style='font-family: Arial, sans-serif; background-color:#f4f6f8; padding:20px;'>

                            <div style='max-width:600px; margin:auto; background:#ffffff; padding:30px; border-radius:6px;'>

                                <h2 style='color:#333;'>Confirm Your Subscription</h2>

                                <p style='font-size:14px; color:#555;'>
                                    Thank you for subscribing! Please confirm your email address by clicking the button below.
                                </p>

                                <div style='text-align:center; margin:30px 0;'>
                                    <a href='{resetLink}'
                                       style='background-color:#007bff;
                                              color:#ffffff;
                                              padding:14px 28px;
                                              font-size:16px;
                                              text-decoration:none;
                                              border-radius:5px;
                                              display:inline-block;'>
                                        Confirm Subscription
                                    </a>
                                </div>

                                <p style='font-size:12px; color:#888;'>
                                    If you did not subscribe, you can safely ignore this email.
                                </p>

                                <hr style='margin:30px 0; border:none; border-top:1px solid #eee;' />

                                <p style='font-size:12px; color:#999; text-align:center;'>
                                    No longer want to receive these emails?
                                    <br />
                                    <a href='{unsubscribeLink}'
                                       style='color:#007bff; text-decoration:none;'>
                                        Unsubscribe
                                    </a>
                                </p>

                            </div>

                        </body>
                        </html>"
                };
                mailMessage.To.Add(email);

                using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    message = $"Subscribe link sent successfully to {email}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return Json(new { success = true, message = message });
        }
        public IActionResult SubscribeStatus(string state)
        {
            return View(model: state);
        }

        [Route("Dashboard/VerifySubscription/{token}")]
        public async Task<IActionResult> VerifySubscription(string token)
        {
            var response = await new EmailSubscriptionViewModel().VerifySubscription(token);
            var message = "An issue occured. Please Subscribe again";
            if (response.Contains("Email verified successfully"))
            {
                message = "subscribed";
            }
            return RedirectToAction("SubscribeStatus", new { state = message });
        }
        [Route("Dashboard/Unsubscribe/{token}")]
        public async Task<IActionResult> Unsubscribe(string token)
        {
            var response = await new EmailSubscriptionViewModel().Unsubscribe(token);
            var message = "An issue occured. Please try again";
            if (response.Contains("successfully unsubscribed"))
            {
                message = "unsubscribed";
            }
            return RedirectToAction("SubscribeStatus", new { state = message });
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
        public IActionResult termsAndConditions()
        {
            return View();
        }

        /// <summary>
        /// State names
        /// </summary>
        /// <returns> List of all states under given country id</returns>
        public string GetStates(int countryId)
        {
            UserViewModel user = new UserViewModel();
            string states = user.GetStateName(countryId);
            return states;
        }

        /// <summary>
        /// City names
        /// </summary>
        /// <returns> List of all cities under given state id</returns>
        public string GetCities(int stateId)
        {
            UserViewModel user = new UserViewModel();
            string cities = user.GetCityName(stateId);
            return cities;
        }

        /// <summary>
        /// Review Blogs Page
        /// </summary>
        /// <returns> List of all Blogs to be reviewed </returns>
        public IActionResult ReviewBlogs(int pageNumber = 1, int pageSize = 10, int categoryId = 0, bool publishedOnly = false)
        {
            ViewBag.CurrentPage = pageNumber;
            List<BlogViewModel> blogs = BlogViewModel.GetAllBlogs(pageNumber, pageSize, categoryId, publishedOnly);
            return View(blogs);
        }

        /// <summary>
        /// Manage Blogs Page
        /// </summary>
        /// <returns> List of all Blogs (Status of active ads can be changed) </returns>
        public IActionResult ManageBlogs(int pageNumber = 1, int pageSize = 6, int categoryId = 0, bool publishedOnly = false)
        {
            ViewBag.CurrentPage = pageNumber;
            List<BlogViewModel> blogs = BlogViewModel.GetAllBlogs(pageNumber, pageSize, categoryId, publishedOnly);
            return View(blogs);
        }
        public IActionResult navSearch(int categoryid = 0, int page = 1, string strProductName = "", string strCity = "", string strState = "", decimal decMinPrice = 0, decimal decMaxPrice = 0, string strSearchType = "", string strSearchText = "")
        {
            ProductViewModel productViewModel = new ProductViewModel();
            List<ProductViewModel> products;

            // If no search/filter parameters are used, fall back to category-based logic  
            bool isSearch = !string.IsNullOrEmpty(strProductName) ||
                            !string.IsNullOrEmpty(strCity) ||
                            !string.IsNullOrEmpty(strState) ||
                            decMinPrice > 0 || decMaxPrice > 0 ||
                            !string.IsNullOrEmpty(strSearchType) ||
                            !string.IsNullOrEmpty(strSearchText);

            if (isSearch)
            {
                products = productViewModel.GetProductsList(strProductName, strCity, strState, decMinPrice, decMaxPrice, strSearchType, strSearchText);
            }
            else
            {
                products = productViewModel.GetProducts(categoryid);
            }
            List<ProductViewModel> allProducts = productViewModel.GetAds(0);
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            SearchFilterDetailsViewModel searchFilterDetails = new SearchFilterDetailsViewModel();
            List<SearchFilterDetailsViewModel> filDetails = searchFilterDetails.GetSearchFilterDetails();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<int> wishlistIds = productViewModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;
            ViewBag.CurrentPage = page;
            ViewBag.Count = products.Count();
            ViewBag.ItemsPerPage = 12;
            List<ProductViewModel> recommendedList = allProducts.Where(product => product.productMembershipID == 2).ToList();
            AdListFiltersViewModel adListFilters = new AdListFiltersViewModel()
            {
                Products = products,
                Filters = filDetails,
                Categories = categoryList,
                RecommendedAds = recommendedList
            };
            return View("AdsList", adListFilters);
        }
        /// <summary>
        /// Ads List
        /// </summary>
        /// <returns> List of all Ads will be returned</returns>
        /// // Need to be reviewed with Anoop
        public async Task<IActionResult> AdsList(int categoryid = 0, int subcategoryid = 0, int page = 1, int ItemsPerPage = 12, bool rating = false, bool featured = false, bool trend = false)
        {
            ProductViewModel productModel = new ProductViewModel();
            List<ProductViewModel> products = productModel.GetAds(0);
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            if (categoryid != 0)
            {
                products = products.Where(product => product.productCategoryID == categoryid).ToList();
                if (subcategoryid != 0)
                {
                    products = products.Where(product => product.productsubCategoryID == subcategoryid).ToList();
                }
            }
            SearchFilterDetailsViewModel searchFilterDetails = new SearchFilterDetailsViewModel();
            List<SearchFilterDetailsViewModel> filDetails = searchFilterDetails.GetSearchFilterDetails();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<int> wishlistIds = productModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            List<ProductViewModel> recommendedList = products.Where(product => product.productMembershipID == 2).ToList();

            if (featured)
            {
                products = recommendedList;
            }

            if (trend)
            {
                // Use the API to get trending/recommended ads instead of local filtering
                // Request a reasonable number (0 means let API decide / return full set if supported)
                var trendingFromApi = await productModel.GetRecommendedAds(12, 4, true);
                products = trendingFromApi ?? new List<ProductViewModel>();
            }

            if (rating)
            {
                products = products.OrderByDescending(p => p.averageRating).ToList();
            }

            ViewBag.WishlistIds = wishlistIds;
            ViewBag.CurrentPage = page;
            ViewBag.Count = products.Count();
            ViewBag.ItemsPerPage = ItemsPerPage;
            AdListFiltersViewModel adListFilters = new AdListFiltersViewModel()
            {
                Products = products,
                Filters = filDetails,
                Categories = categoryList,
                RecommendedAds = recommendedList
            };
            return View(adListFilters);
        }
        public IActionResult BlogDetails(int blogId)
        {
            BlogViewModel blogs = BlogViewModel.GetBlogById(blogId);
            if (blogs != null)
            {
                return View(blogs);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult PublishBlog(int blogId)
        {
            var updatedBy = HttpContext.Session.GetString("userName") ?? "";
            BlogViewModel blogVM = new BlogViewModel();
            var response = blogVM.PublishBlog(blogId, updatedBy);
            //return Json(new { status = response });
            return RedirectToAction("BlogDetails", new { blogId = blogId });
        }
        public IActionResult ProductsList([FromBody] List<ProductViewModel> products, int page = 1, int itemsPerPage = 12)
        {
            ViewBag.Count = products.Count();
            ViewBag.CurrentPage = page;
            ViewBag.ItemsPerPage = itemsPerPage;
            ProductViewModel productModel = new ProductViewModel();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<int> wishlistIds = productModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;

            return PartialView("_ProductsPartial", products);
        }
        public IActionResult ProductsList2([FromBody] List<ProductViewModel> products, int page = 1, int itemsPerPage = 12)
        {
            ViewBag.Count = products.Count();
            ViewBag.CurrentPage = page;
            ViewBag.ItemsPerPage = itemsPerPage;

            return PartialView("_ProductsPartial2", products);
        }
        public IActionResult ProductsList3([FromBody] List<ProductViewModel> products, int page = 1, int itemsPerPage = 12)
        {
            ViewBag.Count = products.Count();
            ViewBag.CurrentPage = page;
            ViewBag.ItemsPerPage = itemsPerPage;

            return PartialView("_ProductsPartial3", products);
        }
        public async Task<IActionResult> productList(int categoryid = 0, int subcategoryid = 0, string adtype = "", int page = 1, string strProductName = "", string strCity = "", string strState = "", decimal decMinPrice = 0, decimal decMaxPrice = 0, string strSearchType = "", string strSearchText = "", string sort = "", int itemsPerPage = 12, bool ratings = false, bool recommended = false, bool trend = false, bool featured = false)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            List<ProductViewModel> products;

            // If no search/filter parameters are used, fall back to category-based logic  
            //bool isSearch = !string.IsNullOrEmpty(strProductName) ||
            //                !string.IsNullOrEmpty(strCity) ||
            //                !string.IsNullOrEmpty(strState) ||
            //                decMinPrice > 0 || decMaxPrice > 0 ||
            //                !string.IsNullOrEmpty(strSearchType) ||
            //                !string.IsNullOrEmpty(strSearchText);

            //if (isSearch)
            //{
            //    products = productViewModel.GetProductsList(strProductName, strCity, strState, decMinPrice, decMaxPrice, strSearchType, strSearchText);
            //}
            //else
            //{
            //    products = productViewModel.GetProducts(categoryid);
            //}
            
            if (trend && decMinPrice > 0 || decMaxPrice > 0)
            {
                products = productViewModel.GetProductsList(strProductName, "", strState, decMinPrice, decMaxPrice, strSearchType, strSearchText);
            }
            else
            {
                products = productViewModel.GetAds(0);
            }

            if (featured)
            {
                products = products.Where(product => product.productMembershipID == 2).ToList();
            }

            if (recommended)
            {
                products = products.Where(product => product.productMembershipID == 2).ToList();
            }

            if (trend)
            {
                // Use API for trending results
                var trendingFromApi = await productViewModel.GetRecommendedAds(12, 4, true);
                products = trendingFromApi ?? new List<ProductViewModel>();
            }
            if (decMinPrice > 0 || decMaxPrice > 0)
            {
                products = products.Where(product => product.MaxPrice <= decMaxPrice && product.MinPrice >= decMinPrice).ToList();
            }
            if (categoryid != 0)
            {
                products = products.Where(product => product.productCategoryID == categoryid).ToList();

                if (subcategoryid != 0)
                {
                    products = products.Where(product => product.productsubCategoryID == subcategoryid).ToList();
                }
            }

            if (strCity != "")
            {
                List<string> citiesList = strCity.Split(',').ToList();
                products = products.Where(product => citiesList.Contains(product.userContactCity.ToLower())).ToList();
            }

            if (adtype != "")
            {
                List<string> adtypeList = adtype.Split(',').ToList();
                products = products.Where(product => adtypeList.Contains(product.productAdCategory.ToLower())).ToList();
            }
            if (ratings)
            {
                products = products.OrderByDescending(p => p.averageRating).ToList();
            }

            if (sort != "")
            {
                if (sort == "desc" || sort == "5")
                {
                    products = products.OrderByDescending(p => p.productPrice).ToList();
                }
                else if (sort == "1")
                {
                    products = products.Where(p => p.productMembershipID == 2).ToList();
                }
                else
                {
                    products = products.OrderBy(p => p.productPrice).ToList();
                }
            }

            //CategoryViewModel category = new CategoryViewModel();
            //List<CategoryViewModel> categoryList = category.GetCategoryCount();
            //SearchFilterDetailsViewModel searchFilterDetails = new SearchFilterDetailsViewModel();
            //List<SearchFilterDetailsViewModel> filDetails = searchFilterDetails.GetSearchFilterDetails();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<int> wishlistIds = productViewModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;
            ViewBag.CurrentPage = page;
            ViewBag.Count = products.Count();
            ViewBag.ItemsPerPage = itemsPerPage;
            //AdListFiltersViewModel adListFilters = new AdListFiltersViewModel()
            //{
            //    Products = products,
            //    Filters = filDetails,
            //    Categories = categoryList
            //};
            return PartialView("_ProductsPartial", products);
        }
        /// <summary>
        /// Categories List
        /// </summary>
        /// <returns>List of Categories and Subcategories</returns>
        public IActionResult CategoryList()
        {
            CategoryViewModel categoryModel = new CategoryViewModel();
            List<CategoryViewModel> categoriesList = categoryModel.GetCategoryCount();
            return View(categoriesList);
        }
        /// <summary>
        /// Categories List
        /// </summary>
        /// <returns>List of Categories and Subcategories</returns>
        public IActionResult Cities()
        {
            SearchFilterDetailsViewModel searchFilterDetails = new SearchFilterDetailsViewModel();
            List<SearchFilterDetailsViewModel> filDetails = searchFilterDetails.GetSearchFilterDetails();
            var citiesList = filDetails.Where(x => x.CategoryType.ToLower().Equals("cities")).OrderByDescending(x => x.totalCount).ToList();
            return View(citiesList);
        }
        [Route("Dashboard/Subscribe/{token}")]
        public async Task<string> Subscribe(string Email)
        {
            string response = string.Empty;
            EmailSubscriptionViewModel sub = new EmailSubscriptionViewModel
            {
                Email = Email,
                FullName = ""
            };
            try
            {
                response = await sub.Subscribe(sub);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}