using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Session;
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
            int productCount = product.GetwishlistCount(productOwnerID);
            ViewData["wishlistcount"] = productCount;
            
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
        public JsonResult RegisterUser(string userName,string password)
        {
            object JsonData = null;
            UserViewModel user = new UserViewModel();
            user.username = userName;
            user.password = password;
            bool isRegistered = false;
            try
            {
                isRegistered = user.RegisterUser(user);
                JsonData = new
                {
                    StatusCode = 200
                };
            }
            catch (Exception ex) { 
            }
            return Json(JsonData);
        }
        /// <summary>
        /// 
        
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult verifyUserName(string userName) { 
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
            HttpContext.Session.SetString("userName","");
            HttpContext.Session.SetString("Role", "");
            if (user != null)
            {
                HttpContext.Session.SetString("Mobile", user.userMobile);
                HttpContext.Session.SetString("Email", user.userEmail);
                HttpContext.Session.SetString("Address", user.userFullAddress);
                HttpContext.Session.SetString("userName", user.username);
                HttpContext.Session.SetString("Role", user.userRoleName);
                HttpContext.Session.SetInt32("UserId", user.userID);
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
        public bool CheckUserExists(string email)
        {
            UserViewModel user = new UserViewModel();
            List<UserViewModel> userInfo = user.GetUsersInfo(email);
            if (userInfo.Count() > 0)
            {
                HttpContext.Session.SetInt32("UserID", userInfo[0].userID);
                HttpContext.Session.SetString("UserName", userInfo[0].username);
                HttpContext.Session.SetString("UserEmail", userInfo[0].userEmail.ToString());
                HttpContext.Session.SetString("UserPhone", userInfo[0].userMobile.ToString());
                HttpContext.Session.SetString("UserFirstName", userInfo[0].userFirstName.ToString());
            }
            return (userInfo.Count != 0) ? true : false;
        }

        /// <summary>
        /// Send Email to User
        /// </summary>
        /// <returns>Password Reset link to the user mail</returns>
        public IActionResult SendEmail(string email)
        {
            try
            {
                var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                string senderEmail = configuration["EmailSender:Email"]; // Replace with your email
                string senderPassword = configuration["EmailSender:Password"]; // Replace with your email password
                string smtpHost = configuration["EmailSender:smtp"]; // Gmail SMTP host
                int smtpPort = Convert.ToInt32(configuration["EmailSender:port"]); // TLS port for Gmail
                string token = Guid.NewGuid().ToString();

                //string resetLink = $"{Request.Scheme}://{Request.Host}/Dashboard/ForgotPassword/{userid}";
                PasswordResetViewModel passwordReset = new PasswordResetViewModel();
                int userid = HttpContext.Session.GetInt32("UserID") ?? 0;
                string username = HttpContext.Session.GetString("UserName");
                string createdby = HttpContext.Session.GetString("UserFirstName") ?? HttpContext.Session.GetString("UserName");
                DateTime expirationtime = DateTime.Now.AddMinutes(30);
                string formattedExpirationTime = expirationtime.ToString("yyyy-MM-dd HH:mm:ss");
                string response = passwordReset.AddPasswordReset(userid, username, token, formattedExpirationTime, username);

                if (response.Contains("Record inserted"))
                {
                    string resetLink = Url.Action("ForgotPassword", "Dashboard", new { userid = userid, username = username, token = token }, Request.Scheme);
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
                    }
                    return Json(new { success = true, message = $"Password link sent successfully to {email}" });
                }
                else
                {
                    return Json(new { success = true, message = $"Error while sending password. Please try again" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Reset Password Path
        /// </summary>
        /// <returns>Forgot Password Modal</returns>
        [Route("Dashboard/ForgotPassword/{userid}/{username}/{token}")]
        public IActionResult ForgotPassword(int userid, string username, string token)
        {
            PasswordResetViewModel passwordReset = new PasswordResetViewModel();
            List<PasswordResetViewModel> resetInfo = passwordReset.GetPasswordReset(userid, username, token);
            if (resetInfo[0].UserTokenExpiration < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))
            {
                return Json(new { message = "This link is expired. Please request for new link." });
            }
            ViewBag.UserId = userid;
            ViewBag.Username = username;
            return View();
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
        public IActionResult Logout()
        {
            HttpContext.Session.SetObject("UserDetails", "");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("Role");
            //HttpContext.Session.Remove("userName");
            return RedirectToAction("Login");
        }
    }
}  
