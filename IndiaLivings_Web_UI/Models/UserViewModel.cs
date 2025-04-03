using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;

namespace IndiaLivings_Web_UI.Models
{
    public class UserViewModel
    {
        #region Properties
        public int userID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userFirstName { get; set; }=string.Empty;
        public string userMiddleName { get; set; } = string.Empty;  
        public string userLastName { get; set; } = string.Empty;
        public string userDescription { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string? userMobile { get; set; }
        public int userAddressID { get; set; }      
        public string userFullAddress { get; set; } = string.Empty;
        public string userImagePath { get; set; } = string.Empty;
        public int userRoleID { get; set; } = 0;
        public string userRoleName { get; set; } = string.Empty;
        public string userWebsite { get; set; } = string.Empty;
        public DateTime? userDOB { get; set; }
        public string emailConfirmed { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedBy { get; set; }

        public string Error_Message { get; set; }

        public string membershipName { get; set; }

        public byte userImageInfo { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// verifying whether credentials are valid
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns>if credentials are valid, User Object will be returned.</returns>
        public UserViewModel ValidateUser(string UserName, string password)
        {
            UserViewModel user = new UserViewModel();
            AuthenticationHelper authenticationHelper = new AuthenticationHelper();
            try
            {
                var userDetails = authenticationHelper.validateUser(UserName, password);
                if (userDetails != null)
                {
                    user.userID = userDetails.userID;
                    user.username = userDetails.username;
                    user.userFirstName = userDetails.userFirstName;
                    user.userMiddleName = userDetails.userMiddleName;
                    user.userLastName = userDetails.userLastName;
                    user.userDescription = userDetails.userDescription;
                    user.userEmail = userDetails.userEmail;
                    user.userMobile = userDetails.userMobile;
                    user.userFullAddress = userDetails.userFullAddress;
                    user.userImagePath = userDetails.userImagePath;
                    user.userRoleID = userDetails.userRoleID;
                    user.userRoleName = userDetails.userRoleName;
                    user.userWebsite = userDetails.userWebsite;
                    user.userDOB = userDetails.userDOB;
                    user.IsActive = userDetails.IsActive;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return user;
        }

        /// <summary>
        /// Creating unique User
        /// </summary>
        /// <returns> whether User Created or not will be returned</returns>
        public bool RegisterUser(UserViewModel userVM)
        {
            bool isInsert = false;
            UserModel userModel = new UserModel();
            AuthenticationHelper AH=new AuthenticationHelper();
            try
            {
                
                userModel.username = userVM.username;
                userModel.password = userVM.password;
                userModel.IsActive = true;
                userModel.userRoleID = 2;
                userModel.createdDate =DateTime.Now;
                userModel.updatedDate = DateTime.Now;

                userModel.userDOB = userVM.userDOB == null ? DateTime.MinValue : (DateTime)userVM.userDOB;
                userModel.IsActive = true;
                userModel.createdBy = "User";
                userModel.userRoleName = "User";                
                if (userVM.username.Contains("@"))
                    userModel.userEmail = userVM.username;
                else
                    userModel.userMobile = userVM.username;
                isInsert = AH.registerUser(userModel);

            }
            catch (Exception ex) {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            
            return isInsert;
        }

        public bool checkDuplicate(string userName)
        {
            bool isExist = false;
            AuthenticationHelper AH=new AuthenticationHelper();
            try
            {
                isExist = AH.checkDuplicate(userName);
            }catch(Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return isExist;
        }

        public List<UserViewModel> UsersList()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var userList = AH.ActiveUserList();
                if (userList != null)
                {
                    foreach (var userDetails in userList)
                    {
                        UserViewModel user = new UserViewModel();
                        user.userID = userDetails.userID;
                        user.username = userDetails.username;
                        user.userFirstName = userDetails.userFirstName;
                        user.userMiddleName = userDetails.userMiddleName;
                        user.userLastName = userDetails.userLastName;
                        user.userDescription = userDetails.userDescription;
                        user.userEmail = userDetails.userEmail;
                        user.userMobile = userDetails.userMobile;
                        user.userFullAddress = userDetails.userFullAddress;
                        user.userImagePath = userDetails.userImagePath;
                        user.userRoleID = userDetails.userRoleID;
                        user.userRoleName = userDetails.userRoleName;
                        user.userWebsite = userDetails.userWebsite;
                        user.userDOB = userDetails.userDOB;
                        user.IsActive = userDetails.IsActive;
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }


            return users;
        }
      
        public List<UserViewModel> GetUsersInfo(string username)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var userList = AH.GetUserByUsername(username);
                if (userList != null)
                {
                    foreach (var userDetails in userList)
                    {
                        UserViewModel user = new UserViewModel();
                        user.userEmail = userDetails.userEmail;
                        user.userMobile = userDetails.userMobile;
                        user.userFirstName = userDetails.userFirstName;
                        user.userID = userDetails.userID;
                        user.userMobile = userDetails.userMobile;
                        user.username = userDetails.username;
                        user.userWebsite = userDetails.userWebsite;
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }


            return users;
        }
      
        #endregion
    }
    
}
