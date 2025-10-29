using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace IndiaLivings_Web_UI.Controllers
{
    public class UserController : BaseController
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
            MembershipViewModel membershipModel = new MembershipViewModel();
            MembershipViewModel membership = membershipModel.GetMembershipDetails(productOwner)[0];
            return View(membership);
        }
        public IActionResult PostAd()
        {
            var loggedInUSer = HttpContext.Session.GetInt32("UserId");
            if (loggedInUSer.HasValue)
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
                data.product = null;
                data.ProductImages = null;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public IActionResult ProductsSearch(string strProductName, string strCity, string strState, decimal decMinPrice, decimal decMaxPrice, string strSearchType, string strSearchText)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            List<ProductViewModel> products = productViewModel.GetProductsList(strProductName, strCity, strState, decMinPrice, decMaxPrice, strSearchType, strSearchText);
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
        public IActionResult Bookmark(int page = 1)
        {
            ProductViewModel productModel = new ProductViewModel();
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<ProductViewModel> wishlist = productModel.GetAllWishlist(productOwner);
            int wishlistCount = productModel.GetwishlistCount(productOwner);
            HttpContext.Session.SetInt32("wishlistCount", wishlistCount);
            ViewBag.CurrentPage = page;
            ViewBag.Count = wishlist.Count();
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
        public IActionResult MyAds(int page = 1)
        {
            int productOwner = HttpContext.Session.GetInt32("UserId") ?? 0;
            ProductViewModel productModel = new ProductViewModel();
            List<ProductViewModel> products = productModel.GetAds(productOwner);
            List<int> wishlistIds = productModel.GetAllWishlist(productOwner).Select(w => w.productId).ToList();
            ViewBag.WishlistIds = wishlistIds;
            ViewBag.CurrentPage = page;
            ViewBag.Count = products.Count();
            return View(products);
        }

        /// <summary>
        /// User Contact and Address details
        /// </summary>
        /// <returns>User Contact details</returns>
        public ActionResult Profile()
        {
            string username = HttpContext.Session.GetString("userName");
            var userViewModel = new UserViewModel();
            List<UserViewModel> users = new List<UserViewModel>();
            users = userViewModel.GetUsersInfo(username);
            MembershipViewModel membershipModel = new MembershipViewModel();
            List<MembershipViewModel> membership = new List<MembershipViewModel>();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            membership = membershipModel.GetMembershipDetails(70);
            ProfileViewModel profileModel = new ProfileViewModel()
            {
                Users = users,
                Membership = membership
            };
            return View(profileModel);
        }

        /// <summary>
        /// Update billing and shipping address
        /// </summary>
        /// <returns>Update status</returns>
        public IActionResult UpdateAddress(string houseNo, string pincode, string country, string state, string city, int type)
        {
            string result = "Update Failed";
            int intUserID = HttpContext.Session.GetInt32("UserId") ?? 0;
            UserAddressViewModel address = new UserAddressViewModel();
            result = address.UpdateAddress(intUserID, houseNo, city, state, country, pincode, type);
            return Json(new { status = result });
        }

        /// <summary>
        /// Sends User Address details to Edit Modal 
        /// </summary>
        /// <returns>Current user address</returns>
        public IActionResult UserAddressInfo(string houseNo, string pincode, string state, string city, string country, string type)
        {
            ViewBag.HouseNo = houseNo;
            ViewBag.Pincode = pincode;
            ViewBag.State = state;
            ViewBag.City = city;
            ViewBag.Country = country;
            ViewBag.Type = type;
            return PartialView("_UserAddressInfo");
        }

        /// <summary>
        /// Country Names
        /// </summary>
        /// <returns> List of all Country names</returns>
        public string GetCountryName()
        {
            UserViewModel user = new UserViewModel();
            string countries = user.GetCountryName();
            return countries;
        }
        [HttpPost]
        public ActionResult PostAd(List<IFormFile> productImage, IFormCollection FormData)
        {
            bool isInsert = false;
            byte[] ImageBytes = [];
            if (productImage[0
                ] != null)
            {
                ImageBytes = GetByteInfo(productImage[0]);
            }
            ProductViewModel PVM = new ProductViewModel();
            PVM.productId = Convert.ToInt32(FormData["productId"]);
            PVM.productName = FormData["productName"].ToString();
            PVM.productDescription = FormData["AdDescription"].ToString();
            PVM.productAdTags = FormData["adTag"].ToString();
            PVM.productPrice = Convert.ToDecimal(FormData["productPrice"]);
            PVM.productQuantity = Convert.ToInt32(FormData["productQuantity"]);
            PVM.productCondition = FormData["product_Condition"].ToString().ToUpper() == "NEW" ? 1 : 0;
            PVM.productCategoryID = Convert.ToInt32(FormData["category"].ToString());
            PVM.productImageId = Convert.ToInt32(FormData["existingImageId"]);
            PVM.byteProductImageData = ImageBytes;
            //PVM.productCategoryName = FormData[""];
            PVM.productsubCategoryID = Convert.ToInt32(FormData["subCategory"].ToString());
            //PVM.productSubCategoryName = FormData[""];
            PVM.productPriceCondition = FormData["price_Condition"];
            PVM.productAdCategory = FormData["Ad_Category"];
            PVM.productImageName = productImage[0] != null ? productImage[0].FileName : "";

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
            PVM.userContactCity = HttpContext.Session.GetString("City").ToString();
            PVM.userContactState = HttpContext.Session.GetString("State").ToString();
            isInsert = PVM.CreateNewAdd(PVM, productImage);


            return RedirectToAction("PostAd");
        }
        public int UserAdCount()
        {
            int adsRemCount = 0;
            try
            {
                AdsByMembershipViewModel adsRem = new AdsByMembershipViewModel();
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                List<AdsByMembershipViewModel> adData = adsRem.GetUserAdsRemaining(userId);
                adsRemCount = adData[0].userTotalAdsRemaining;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return adsRemCount;
        }
        public ActionResult UpdateUser(IFormFile profileImage, IFormCollection FormData)
        {
            bool isInsert = false;
            string result = "";
            UserViewModel UVM = new UserViewModel();
            UVM.userID = HttpContext.Session.GetInt32("UserId") ?? 0;
            UVM.username = HttpContext.Session.GetString("userName");
            UVM.password = "";
            UVM.userFirstName = string.IsNullOrEmpty(FormData["txt_firstname"]) ? "" : FormData["txt_firstname"];
            UVM.userMiddleName = string.IsNullOrEmpty(FormData["txt_middlename"]) ? "" : FormData["txt_middlename"];
            UVM.userLastName = string.IsNullOrEmpty(FormData["txt_lastname"]) ? "" : FormData["txt_lastname"];
            UVM.userDescription = string.IsNullOrEmpty(FormData["txt_description"]) ? "" : FormData["txt_description"];
            UVM.userEmail = string.IsNullOrEmpty(FormData["txt_Email"]) ? "" : FormData["txt_Email"];
            UVM.userMobile = string.IsNullOrEmpty(FormData["txt_phone"]) ? "" : FormData["txt_phone"];
            UVM.userFullAddress = string.IsNullOrEmpty(FormData["txt_address"]) ? "" : FormData["txt_address"];
            UVM.userCity = string.IsNullOrEmpty(FormData["txt_city"]) ? "" : FormData["txt_city"];
            UVM.userState = string.IsNullOrEmpty(FormData["txt_state"]) ? "" : FormData["txt_state"];
            UVM.userCountry = string.IsNullOrEmpty(FormData["txt_Country"]) ? "" : FormData["txt_Country"];
            UVM.userPinCode = string.IsNullOrEmpty(FormData["txt_postCode"]) ? "" : FormData["txt_postCode"];
            UVM.userRoleID = HttpContext.Session.GetInt32("RoleId") ?? 0;
            UVM.userRoleName = HttpContext.Session.GetString("Role");
            UVM.userWebsite = string.IsNullOrEmpty(FormData["txt_website"]) ? "" : FormData["txt_website"];
            UVM.userDOB = DateTime.TryParse(FormData["txt_dob"].ToString(), out DateTime parsedDOB) ? parsedDOB : DateTime.MinValue;
            UVM.strUserImageName = "";
            byte[] ImageBytes = [];
            UVM.strUserImageType = "";
            UVM.userImagePath = "";
            if (profileImage != null)
            {
                ImageBytes = GetByteInfo(profileImage);
                UVM.strUserImageName = profileImage.FileName;
                UVM.strUserImageType = Path.GetExtension(profileImage.FileName)?.TrimStart('.').ToLower();
                UVM.byteUserImageData = ImageBytes;
            }
            UVM.byteUserImageData = ImageBytes;
            //UVM.userImagePath = "";
            UVM.emailConfirmed = "";
            UVM.IsActive = true;
            UVM.createdDate = DateTime.Now;
            UVM.createdBy = HttpContext.Session.GetString("userName").ToString();
            UVM.updatedDate = DateTime.Now;
            UVM.updatedBy = HttpContext.Session.GetString("userName").ToString();
            result = UVM.UpdateUserProfile(UVM);
            if (profileImage != null)
            {
                var response = UVM.UploadUserImage(UVM.userID, profileImage.FileName, UVM.strUserImageType, UVM.createdBy, profileImage);
                if (response)
                {
                    var userViewModel = new UserViewModel();
                    List<UserViewModel> user = new List<UserViewModel>();
                    user = userViewModel.GetUsersInfo(UVM.createdBy);
                    HttpContext.Session.SetObject("UserImage", user[0].byteUserImageData);
                }
            }
            return Json(new { status = result });
        }
        public JsonResult UpdateUserImage(IFormFile imageFile)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            string imageType = Path.GetExtension(imageFile.FileName)?.TrimStart('.').ToLower();
            string createdBy = HttpContext.Session.GetString("userName").ToString();
            UserViewModel userModel = new UserViewModel();
            var response = userModel.UploadUserImage(userId, imageFile.FileName, imageType, createdBy, imageFile);
            if (response)
            {
                var userViewModel = new UserViewModel();
                List<UserViewModel> user = new List<UserViewModel>();
                user = userViewModel.GetUsersInfo(createdBy);
                HttpContext.Session.SetObject("UserImage", user[0].byteUserImageData);
                return Json(new { Status = "Image Added" });
            }
            else
            {
                return Json(new { Status = "Upload Failed" });
            }
        }
        //public UserAddressViewModel GetUserAddress(IFormCollection FormData, bool isBilling)
        //{
        //    string prefix = isBilling ? "txt_b_" : "txt_s_";
        //    return new UserAddressViewModel
        //    {
        //        intUserID = HttpContext.Session.GetInt32("UserId") ?? 0,
        //        strUserContactFullAddress = FormData["txt_address"],
        //        strUserContactCity = FormData[$"{prefix}city"],
        //        strUserContactState = FormData[$"{prefix}state"],
        //        strUserContactCountry = FormData[$"{prefix}country"],
        //        strUserContactPinCode = FormData[$"{prefix}postcode"],
        //        intUserAddressType = isBilling ? 1 : 2
        //    };
        //}

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
            HttpContext.Session.SetObject("UserDetails", users[0]);
            HttpContext.Session.SetString("UserFullName", users[0].userFirstName + " " + users[0].userMiddleName + " " + users[0].userLastName);
            HttpContext.Session.SetString("Mobile", users[0].userMobile.ToString());
            HttpContext.Session.SetString("Email", users[0].userEmail);
            HttpContext.Session.SetString("Address", users[0].userFullAddress);
            return View(users);
        }

        public IActionResult success()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessPayment(IFormCollection formData)
        {
            int Amount = 0;
            var loggedInUser = HttpContext.Session.GetInt32("UserId");
            if (loggedInUser == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Amount = Convert.ToInt32(formData["planSelection"]);
                if (Amount != 0)
                {
                    var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                    PaymentRequestViewModel paymentRequestViewModel = new PaymentRequestViewModel();
                    string ApiKey = configuration["PaymentOptions:ApiKey"].ToString();
                    string SecretKey = configuration["PaymentOptions:SecretKey"].ToString();
                    paymentRequestViewModel = paymentRequestViewModel.ProcessRequest(Amount, ApiKey, SecretKey, loggedInUser, "Membership");
                    return View("Payment", paymentRequestViewModel);
                }
                else
                {
                    return View("PostAd");
                }

            }

        }
        private readonly IHttpContextAccessor httpContextAccessor;

        [HttpPost]
        public IActionResult CompleteRequest(IFormCollection formData)
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //IHttpContextAccessor httpContextAccessor=null;
            string paymentCaptured = string.Empty;
            string ApiKey = configuration["PaymentOptions:ApiKey"].ToString();
            string SecretKey = configuration["PaymentOptions:SecretKey"].ToString();
            PaymentRequestViewModel paymentRequestViewModel = new PaymentRequestViewModel();
            paymentCaptured = paymentRequestViewModel.CompleteRequest(formData["rzp_paymentid"], formData["rzp_orderid"], ApiKey, SecretKey);
            //if (paymentCaptured == "captured")
            //    return View("success");
            //else
            //    return View("failed");
            return RedirectToAction("PostAd");
        }
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ServiceProcessPayment(IFormCollection formData)
        {
            int Amount = 0;
            var loggedInUser = HttpContext.Session.GetInt32("UserId");
            if (loggedInUser == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Amount = Convert.ToInt32(formData["price"]);
                if (Amount != 0)
                {
                    var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                    PaymentRequestViewModel paymentRequestViewModel = new PaymentRequestViewModel();
                    string ApiKey = configuration["PaymentOptions:ApiKey"].ToString();
                    string SecretKey = configuration["PaymentOptions:SecretKey"].ToString();
                    paymentRequestViewModel = paymentRequestViewModel.ProcessRequest(Amount, ApiKey, SecretKey, loggedInUser, "Service");
                    return View("Payment", paymentRequestViewModel);
                }
                else
                {
                    return View("ServicesSubCategory");
                }

            }

        }
        /// <summary>
        /// Add Rating for Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        /// <returns>Status message</returns>
        public JsonResult AddRating(int userId, int productId, int rating, string comments)
        {
            string createdBy = HttpContext.Session.GetString("UserFullName") ?? string.Empty;
            ProductViewModel productViewModel = new ProductViewModel();
            string message = productViewModel.AddRating(productId, userId, rating, comments, createdBy);
            string notification = new ProductHelper().AddNotification(productId, userId, "Rating", comments);
            return Json(new { status = message });
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult UpdateProduct(int productId)
        {
            CategoryViewModel category = new CategoryViewModel();
            List<CategoryViewModel> categoryList = category.GetCategoryCount();
            AdConditionViewModel adCondition = new AdConditionViewModel();
            List<AdConditionViewModel> adConitions = new List<AdConditionViewModel>();
            adConitions = adCondition.GetAllAdConditionsTypeName("");
            var priceConditions = adConitions.Where(x => x.strAdConditionType.Equals("Price Condition")).ToList();
            var Ad_Categories = adConitions.Where(x => x.strAdConditionType.Equals("Ad Category")).ToList();
            var productConditions = adConitions.Where(x => x.strAdConditionType.Equals("Product Condition")).ToList();
            ProductViewModel productViewModel = new ProductViewModel();
            var productWithImages = productViewModel.GetProductById(productId);
            dynamic data = new ExpandoObject();
            data.Categories = categoryList;
            data.priceConditions = priceConditions;
            data.Ad_Categories = Ad_Categories;
            data.productConditions = productConditions;
            data.product = productWithImages?.Product ?? new ProductViewModel();
            data.ProductImages = productWithImages?.ProductImages ?? new List<ProductImageDetailsViewModel>();
            return View("PostAd", data);
        }
        /// <summary>
        /// Message Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Message()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            UserViewModel userViewModel = new UserViewModel();
            List<UserViewModel> chatHistory = userViewModel.GetUserChatHistory(userId);

            ViewBag.DefaultChatUserId = chatHistory.FirstOrDefault()?.userID;

            // Load default chat messages
            MessageViewModel msgModel = new MessageViewModel();
            List<MessageViewModel> defaultMessages = msgModel.GetMessageByUser(ViewBag.DefaultChatUserId ?? 0, 0);

            ViewData["DefaultMessages"] = defaultMessages;

            return View(chatHistory);
        }
        public string SendMessage(int senderUserId, int receiverUserId, string messageText)
        {
            string response = "An error occured while calling";
            UserViewModel model = new UserViewModel();
            response = model.SendMessage(senderUserId, receiverUserId, messageText);
            return response;
        }
        public IActionResult GetMessagesByUser(int ReceiverUserId, string username = "")
        {
            int SenderUserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            MessageViewModel messageViewModel = new MessageViewModel();
            ViewBag.UserID = ReceiverUserId;
            List<MessageViewModel> messages = messageViewModel.GetMessageByUser(SenderUserId, ReceiverUserId);
            dynamic data = new ExpandoObject();
            data.Messages = messages;
            if (username != "")
            {
                UserViewModel UVM = new UserViewModel();
                UserViewModel userDetails = UVM.GetUsersInfo(username)[0];
                data.UserData = userDetails;
            }
            string read = messageViewModel.MarkMessagesAsRead(ReceiverUserId, SenderUserId);
            return PartialView("_MessagesByUser", data);
        }
        public IActionResult ChatList()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            UserViewModel userViewModel = new UserViewModel();
            List<UserViewModel> chatHistory = userViewModel.GetUserChatHistory(userId);
            return PartialView("_ChatList", chatHistory);
        }
        public IActionResult SearchUsers(string term)
        {
            UserViewModel user = new UserViewModel();
            List<UserViewModel> userList = new List<UserViewModel>();
            userList = user.UsersList();
            //return View(userList.ToList());
            var results = userList
                .Where(u => !string.IsNullOrEmpty(term) && (
                    (u.username?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.userFirstName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.userMiddleName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.userLastName?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.userMobile?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (u.userEmail?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false)
                ))
                //.Select(u => new {
                //    u.userID,
                //    name = string.IsNullOrEmpty(u.userFirstName) ? u.username : u.userFirstName,
                //    u.byteUserImageData,
                //    u.messageText
                //})
                .ToList();

            return Json(results);
        }
        //public PartialViewResult GetUserCard(int userId)
        //{
        //    var user = new UserViewModel().GetUserById(userId); // Your method
        //    return PartialView("_UserCard", user);
        //}
        public IActionResult GetUserChatListItem(string username)
        {
            UserViewModel user = new UserViewModel().GetUsersInfo(username)[0]; // Use your method to fetch user
            if (user != null)
            {
                return PartialView("_UserCard", user); // Your partial view that renders one <li>
            }
            return NotFound();
        }
        public string DeleteUserMessage(int messageId, int userId)
        {
            string deleteStatus = new MessageViewModel().DeleteUserMessage(messageId, userId);
            return deleteStatus;
        }

        public IActionResult BlogPost()
        {
            // Get categories for dropdown
            List<BlogCategoriesViewModel> blogCategories = BlogCategoriesViewModel.GetAllBlogCategories();

            // Create dynamic object to match view's expectations
            dynamic viewModel = new ExpandoObject();
            viewModel.Blog = null;  // null because this is create mode
            viewModel.Categories = blogCategories;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult BlogPost(IFormFile featuredImageFile, string title, string summary, string content, string tags, int categoryID)
        {
            var userName = HttpContext.Session.GetString("userName") ?? "";
            var now = DateTime.Now;
            byte[] ImageBytes = [];
            if (featuredImageFile != null)
            {
                ImageBytes = GetByteInfo(featuredImageFile);
            }
            var blog = new BlogViewModel
            {
                title = title,
                summary = summary,
                content = content,
                tags = tags,
                categoryID = categoryID,
                author = userName,
                featuredImage = ImageBytes,
                isPublished = false,
                isActive = true,
                publishedDate = now,
                createdDate = now,
                createdBy = userName,
                updatedDate = now,
                updatedBy = userName
            };

            var result = new BlogViewModel().PostBlog(blog);

            if (result == "Success")
                return RedirectToAction("BlogPost"); // Or redirect to a list/details page

            ViewBag.Error = result;
            return BlogPost();
        }
        public IActionResult UpdateBlog(int blogId)
        {
            BlogViewModel blog = BlogViewModel.GetBlogById(blogId);
            List<BlogCategoriesViewModel> blogCategories = BlogCategoriesViewModel.GetAllBlogCategories();

            dynamic data = new ExpandoObject();
            data.Blog = blog;
            data.Categories = blogCategories;

            return View("BlogPost", data);
        }
        [HttpPost]
        public IActionResult UpdateBlogPost(IFormFile featuredImageFile, string title, string summary, string content, string tags, int categoryID, int blogId)
        {
            var userName = HttpContext.Session.GetString("userName") ?? "";
            var now = DateTime.Now;
            byte[] ImageBytes = [];
            if (featuredImageFile != null)
            {
                ImageBytes = GetByteInfo(featuredImageFile);
            }
            var blog = new BlogViewModel
            {
                blogId = blogId,
                title = title,
                summary = summary,
                content = content,
                tags = tags,
                categoryID = categoryID,
                author = userName,
                featuredImage = ImageBytes,
                isPublished = true,
                isActive = true,
                publishedDate = now,
                createdDate = now,
                createdBy = userName,
                updatedDate = now,
                updatedBy = userName
            };
            var result = new BlogViewModel().UpdateBlog(blog);
            if (result == "Success")
                return RedirectToAction("BlogPost"); // Or redirect to a list/details page
            ViewBag.Error = result;
            return BlogPost();
        }
        public IActionResult DeleteBlog(int blogId)
        {
            var updatedBy = HttpContext.Session.GetString("userName") ?? "";
            BlogViewModel blogVM = new BlogViewModel();
            var response = blogVM.DeleteBlog(blogId, updatedBy);
            //return Json(new { status = response });
            return View("ManageBlogs");
        }
        public IActionResult BlogsByUser(int pageNumber = 1, int pageSize = 12, int categoryId = 0, bool publishedOnly = false)
        {
            string username = HttpContext.Session.GetString("userName") ?? "";
            List<BlogViewModel> blogs = BlogViewModel.GetBlogsByUser(username, pageNumber, pageSize, categoryId, publishedOnly);
            return View(blogs);
        }

        public IActionResult JobInfo()
        {
            JobNewsViewModel jobNews = new JobNewsViewModel();
            List<JobNewsViewModel> jobList = new List<JobNewsViewModel>();
            return View(jobList.ToList());
        }

        public IActionResult Notification()
        {
            NotificationViewModel notificationModel = new NotificationViewModel();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<NotificationViewModel> notifications = notificationModel.GetUserNotifications(userId);
            return View(notifications);
        }
        [HttpGet]
        public JsonResult GetUnreadMessageCount(int userId)
        {
            int unreadCount = 0;
            try
            {
                MessageViewModel messageModel = new MessageViewModel();
                unreadCount = messageModel.GetUnreadMessageCount(userId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return Json(unreadCount);
        }

    }
}