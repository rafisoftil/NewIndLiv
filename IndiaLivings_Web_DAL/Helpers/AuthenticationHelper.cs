using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class AuthenticationHelper : IAuthenticationRepository
    {
        public bool registerUser(UserModel user)
        {
            bool isRegistered = false;
            var response = ServiceAPI.Post_Api("Users/AddUser", user);
            if (response == "1")
                isRegistered = true;
            return isRegistered;

        }

        public bool checkDuplicate(string userName)
        {
            dynamic user = null;
            bool isUserExists = false;
            var response = ServiceAPI.Get_async_Api("Users/GetUserByUserName?strUserName=" + userName);
            user = JsonConvert.DeserializeObject(response);
            if (user.Count > 0)
                isUserExists = true;
            return isUserExists;
        }

        public void deleteUser() { }

        public List<UserModel> ActiveUserList()
        {
            List<UserModel> users = new List<UserModel>();
            var userList = ServiceAPI.Get_async_Api("Users/GetActiveListofUsers");
            users = JsonConvert.DeserializeObject<List<UserModel>>(userList);
            return users;

        }
        public UserModel validateUser(string userName, string password)
        {
            UserModel user = null;
            try
            {
                user = new UserModel();
                string response = ServiceAPI.Get_async_Api("Users/CheckUserLogin?strUserName=" + userName + "&strPWD=" + password);
                user = JsonConvert.DeserializeObject<UserModel>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return user;
        }
        public List<RoleModel> RolesList()
        {
            List<RoleModel> role = null;
            try
            {
                role = new List<RoleModel>();
                var response = ServiceAPI.Get_async_Api("Users/GetRoles");
                role = JsonConvert.DeserializeObject<List<RoleModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return role;

        }

        public string updateUser(UserModel user)
        {
            string response = "";
            try
            {
                response = ServiceAPI.Post_Api("Users/UpdateUser", user).Trim('\"');
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateAddress(int intUserID, string strUserContactFullAddress, string strUserContactCity, string strUserContactState, string strUserContactCountry, string strUserContactPinCode, int intUserAddressType)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Users/AddUserAddress?intUserID={intUserID}&strUserContactFullAddress={strUserContactFullAddress}&strUserContactCity={strUserContactCity}&strUserContactState={strUserContactState}&strUserContactCountry={strUserContactCountry}&strUserContactPinCode={strUserContactPinCode}&intUserAddressType={intUserAddressType}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }


        public List<UserModel> GetUserByUsername(string userName)
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUserByUserName?strUserName={userName}");
                response = JsonConvert.DeserializeObject<string>(response);
                users = JsonConvert.DeserializeObject<List<UserModel>>(response);
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return users;
        }
        public string AddPasswordReset(int userid, string username, string token, string expirationTime, string createdby)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Users/AddUserPasswordReset?intUserID={userid}&strUserName={username}&strUserPasswordToken={token}&dtmUserTokenExpiration={expirationTime}&createdBy={createdby}");

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public List<PasswordReset> GetPasswordReset(string token)
        {
            List<PasswordReset> resetInfo = new List<PasswordReset>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUserPasswordReset?strUserPasswordToken={token}");
                resetInfo = JsonConvert.DeserializeObject<List<PasswordReset>>(response);

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return resetInfo;
        }
        public string PasswordReset(string newPassword, string token)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Users/UpdatePasswordfromReset?tokenId={token}&newPassword={newPassword}").Trim('\"');

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdatePassword(int userId, string newPassword)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Users/UpdateUserPassword?userId={userId}&newPassword={newPassword}").Trim('\"');

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public string InsertUserImage(int intUserID, string strUserImageName, string strUserImageType, string createdBy, IFormFile UserImag)
        {
            try
            {
                if (UserImag != null && UserImag.Length > 0 && UserImag.Length <= 2 * 1024 * 1024)
                {
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(intUserID.ToString()), "intUserID");
                    formData.Add(new StringContent(strUserImageName), "strUserImageName");
                    formData.Add(new StringContent(strUserImageType), "strUserImageType");
                    formData.Add(new StringContent(createdBy), "createdBy");
                    formData.Add(new StreamContent(UserImag.OpenReadStream()), "UserImg", UserImag.FileName);

                    var response = ServiceAPI.PostMultipartApi($"Users/AddUserImage?intUserID={intUserID}&strUserImageName={strUserImageName}&strUserImageType={strUserImageType}&createdBy={createdBy}", formData);
                    response.Wait();
                    var res = response.Result?.Trim('\"');
                    if (res.Contains("Image uploaded successfully."))
                    {
                        return "Image uploaded successfully.";
                    }
                    else
                    {
                        return "Failed to upload image";
                    }
                }
                else
                {
                    if (UserImag == null || UserImag.Length == 0)
                    {
                        return "No image file found.";
                    }
                    else
                    {
                        return "Invalid image or image size exceeds 2 MB.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return "An error occurred while uploading the product image.";
            }
        }
        public string GetCountryName()
        {
            var response = "";
            try
            {
                response = ServiceAPI.Get_async_Api("Users/GetCountryName");
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string GetStateNames(int countryId)
        {
            var response = "";
            try
            {
                response = ServiceAPI.Get_async_Api($"Users/GetStateName?intCountryID={countryId}");
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string GetCitiesNames(int stateId)
        {
            var response = "";
            try
            {
                response = ServiceAPI.Get_async_Api($"Users/GetCityName?intStateID={stateId}");
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<AdsByMembershipModel> GetUserAdsRemaining(int userId)
        {
            List<AdsByMembershipModel> adData = new List<AdsByMembershipModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUserAdsRemaining?UserID={userId}");
                response = JsonConvert.DeserializeObject<string>(response);
                adData = JsonConvert.DeserializeObject<List<AdsByMembershipModel>>(response);
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return adData;
        }
        public List<MembershipModel> GetMembershipDetails(int userId)
        {
            List<MembershipModel> adData = new List<MembershipModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Membership/GetMembershipDetailsByUser?intMembershipUserID={userId}");
                adData = JsonConvert.DeserializeObject<List<MembershipModel>>(response);
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return adData;
        }
        public List<MembershipModel> GetAllListofMembership(int memId)
        {
            List<MembershipModel> memDetails = new List<MembershipModel>();
            try
            {
                var details = ServiceAPI.Get_async_Api($"Membership/GetAllListofMembership?intMembershipID={memId}");
                memDetails = JsonConvert.DeserializeObject<List<MembershipModel>>(details);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return memDetails;
        }
        public string AddUserMembership(int MembershipId, int userId, string createdBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Users/AddUserMembership?UserMembershipID={MembershipId}&UserID={userId}&createdBy={createdBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateMembership(int intMembershipID, string strMembershipName, int intMembershipAdsLimit, double decMembershipPrice, string strMembershipDescription, string strUpdatedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Membership/UpdateMembership?intMembershipID={intMembershipID}&strMembershipName={strMembershipName}&intMembershipAdsLimit={intMembershipAdsLimit}&decMembershipPrice={decMembershipPrice}&strMembershipDescription={strMembershipDescription}&strUpdatedBy={strUpdatedBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteMembership(int intMembershipID, string updatedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Membership/DeleteMembership?intMembershipID={intMembershipID}&strUpdatedBy={updatedBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string SendMessage(int senderUserId, int receiverUserId, string messageText)
        {
            string response = string.Empty;
            try
            {
                // Assuming you have a method to send messages, implement it here.
                response = ServiceAPI.Post_Api($"Users/SendMessageToUser?senderUserId={senderUserId}&receiverUserId={receiverUserId}&messageText={messageText}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<UserModel> GetUserChatHistory(int userId)
        {
            List<UserModel> chatHistory = new List<UserModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUserChatHistory?userId={userId}");
                chatHistory = JsonConvert.DeserializeObject<List<UserModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return chatHistory;
        }
        public List<MessageModel> GetMessagesByUserId(int SenderUserId, int ReceiverUserId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUserMessages?SenderUserId={SenderUserId}&ReceiverUserId={ReceiverUserId}");
                messages = JsonConvert.DeserializeObject<List<MessageModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return messages;
        }
        public string DeleteUserMessage(int messageId, int userId)
        {
            string response = "An error occured";
            try
            {
                response = ServiceAPI.Post_Api($"Users/DeleteUserMessage?messageId={messageId}&userId={userId}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string BlogPost(BlogModel blog)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api("Blog/addBlog", blog).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<BlogCategoriesModel> GetBlogCategories()
        {
            List<BlogCategoriesModel> categories = new List<BlogCategoriesModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api("Blog/getAllBlogCategories");
                // Parse the response and extract the "data" property
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    categories = JsonConvert.DeserializeObject<List<BlogCategoriesModel>>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return categories;
        }
        public List<BlogModel> GetAllBlogs(int pageNumber, int pageSize, int categoryId, bool publishedOnly)
        {
            List<BlogModel> blogs = new List<BlogModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Blog/getAllBlogs?pageNumber={pageNumber}&pageSize={pageSize}&categoryId={categoryId}&publishedOnly={publishedOnly}");
                // Parse the response and extract the "data" property
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    blogs = JsonConvert.DeserializeObject<List<BlogModel>>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blogs;
        }
        public BlogModel GetBlogById(int blogId)
        {
            BlogModel blog = new BlogModel();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Blog/getBlogById?blogId={blogId}");
                // Parse the response and extract the "data" property
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    blog = JsonConvert.DeserializeObject<BlogModel>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blog;
        }
        public string UpdateBlog(BlogModel blog)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api("Blog/updateBlog", blog).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteBlog(int blogId, string updatedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Blog/deleteBlog?blogId={blogId}&updatedBy={updatedBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string PublishBlog(int blogId, string updatedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Blog/publishBlog?blogId={blogId}&updatedBy={updatedBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<BlogModel> GetBlogsByUser(string username, int pageNumber, int pageSize, int categoryId, bool publishedOnly)
        {
            List<BlogModel> blogs = new List<BlogModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Blog/getBlogByUser?username={username}&pageNumber={pageNumber}&pageSize={pageSize}&categoryID={categoryId}&publishedOnly={publishedOnly}");
                // Parse the response and extract the "data" property
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    blogs = JsonConvert.DeserializeObject<List<BlogModel>>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blogs;
        }
        public string CreateJob(JobNewsModel job)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api("JobNews/addJobNews", job).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<JobNewsCategoryModel> GetJobCategoryModels()
        {
            List<JobNewsCategoryModel> categories = new List<JobNewsCategoryModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api("JobNews/getAllJobNewsCategories");
                // Parse the response and extract the "data" property
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    categories = JsonConvert.DeserializeObject<List<JobNewsCategoryModel>>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return categories;
        }
        public List<JobNewsModel> GetAllJobNews(int pageNumber, int pageSize, int categoryId, bool publishedOnly, bool activeOnly)
        {
            List<JobNewsModel> jobNews = [];
            try
            {
                var response = ServiceAPI.Get_async_Api($"JobNews/getAllJobNews?pageNumber={pageNumber}&pageSize={pageSize}&categoryId={categoryId}&publishedOnly={publishedOnly}&activeOnly={activeOnly}");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    jobNews = JsonConvert.DeserializeObject<List<JobNewsModel>>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return jobNews;
        }
        public JobNewsModel GetJobNewsByID(int jobId)
        {
            JobNewsModel jobNews = null;
            try
            {
                var response = ServiceAPI.Get_async_Api($"JobNews/getJobNewsById?jobNewsId={jobId}");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    jobNews = JsonConvert.DeserializeObject<JobNewsModel>(data);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return jobNews;
        }
        public string UpdateJobNews(JobNewsModel job)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api("JobNews/updateJobNews", job).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteJobNews(int jobId, string updatedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"JobNews/deleteJobNews?jobNewsId={jobId}&updatedBy={updatedBy}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<NotificationModel> GetNotificationsByUser(int userId)
        {
            List<NotificationModel> notifications = new List<NotificationModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetNotificationsByUser?userId={userId}");
                notifications = JsonConvert.DeserializeObject<List<NotificationModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return notifications;
        }
        public string MarkMessagesAsRead(int messageId, int receiverUserId)
        {
            string response = "An error occured";
            try
            {
                response = ServiceAPI.Post_Api($"Users/MarkMessageAsRead?messageId={messageId}&receiverUserId={receiverUserId}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public int GetReadMessageCount(int userId)
        {
            int count = 0;
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetReadMessageCount?userId={userId}");
                count = int.Parse(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return count;
        }
        public int GetUnreadMessageCount(int userId)
        {
            int count = 0;
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUnreadMessageCount?userId={userId}");
                count = int.Parse(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return count;
        }
        public int GetUnreadNotificationCount(int userId)
        {
            int count = 0;
            try
            {
                var response = ServiceAPI.Get_async_Api($"Users/GetUnreadNotificationCount?userId={userId}");
                count = int.Parse(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return count;
        }
        public static async Task<string> Subscribe(EmailSubscriptionModel subscription)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"EmailSubscription/Subscribe", subscription);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> VerifySubscription(string token)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.GetAsyncApi($"EmailSubscription/VerifyEmail?token={token}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> Unsubscribe(string token)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.GetAsyncApi($"EmailSubscription/Unsubscribe?token={token}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<CompanySetupModel> GetCompanySetupById(int companyId)
        {
            CompanySetupModel companySetup = null;
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"CompanySetup/GetCompanySetupById?companyId={companyId}");
                companySetup = JsonConvert.DeserializeObject<CompanySetupModel>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return companySetup;
        }
        public static async Task<MembershipUpgradePreviewModel> GetMembershipUpgradePreview(int userid, int membershipid)
        {
            MembershipUpgradePreviewModel response = new MembershipUpgradePreviewModel();
            try
            {
                var result = await ServiceAPI.GetAsyncApi($"Membership/GetMembershipUpgradePreview?intUserID={userid}&intNewMembershipID={membershipid}");
                response = JsonConvert.DeserializeObject<MembershipUpgradePreviewModel>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> UpgradeUserMembership(int userid, int currentMembership, int membershipid, string updatedby)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"Membership/UpgradeUserMembership?intUserID={userid}&intCurrentMembershipID={currentMembership}&intNewMembershipID={membershipid}&strUpdatedBy={updatedby}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}