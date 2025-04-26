using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IndiaLivings_Web_UI.Models
{
    public class UserViewModel
    {
        #region Properties
        public int userID { get; set; }
        public string user { get; set; } = string.Empty;
        public string username { get; set; }
        public string password { get; set; }
        public string userFirstName { get; set; } = string.Empty;
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
        public string userCity { get; set; } = string.Empty;
        public string userState { get; set; } = string.Empty;
        public string userCountry { get; set; } = string.Empty;
        public string userPinCode { get; set; } 
        public string strUserImageName { get; set; } = string.Empty;
        public byte[] byteUserImageData { get; set; } = [];
        public string   strUserImageType { get; set; } = string.Empty;

        public bool isActive = true;

        public string userCompany {get;set;}=string.Empty;

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
                    user.byteUserImageData = userDetails.byteUserImageData;
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
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {

                userModel.username = userVM.username;
                userModel.password = userVM.password;
                userModel.IsActive = true;
                userModel.userRoleID = 2;
                userModel.createdDate = DateTime.Now;
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
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return isInsert;
        }

        public bool checkDuplicate(string userName)
        {
            bool isExist = false;
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                isExist = AH.checkDuplicate(userName);
            }
            catch (Exception ex)
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
                        user.username = userDetails.username;
                        user.userEmail = userDetails.userEmail;
                        user.userMobile = userDetails.userMobile;
                        user.userWebsite = userDetails.userWebsite;
                        user.userFirstName = userDetails.userFirstName;
                        user.userMiddleName = userDetails.userMiddleName;
                        user.userLastName = userDetails.userLastName;
                        user.password = userDetails.password;
                        user.userDescription = userDetails.userDescription;
                        user.userFullAddress = userDetails.userFullAddress;
                        user.byteUserImageData = userDetails.byteUserImageData;
                        user.userDOB =(DateTime)userDetails.userDOB;
                        user.userCity = userDetails.userCity;
                        user.userState = userDetails.userState;
                        user.userCountry = userDetails.userCountry;
                        user.userPinCode = userDetails.userPinCode;
                        user.IsActive = userDetails.IsActive;
                        user.strUserImageName = userDetails.strUserImageName;
                        user.strUserImageType = userDetails.strUserImageType;
                        user.byteUserImageData = userDetails.byteUserImageData;
                        user.userCompany = userDetails.userCompany;

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
        public bool UpdateUserProfile(UserViewModel user)
        {
            bool isCreated = false;
            AuthenticationHelper PH = new AuthenticationHelper();
            try
            {
                UserModel UVM = new UserModel();
                UVM.userFirstName = user.userFirstName;
                UVM.userLastName = user.userLastName;
                UVM.userMiddleName = user.userMiddleName;
                UVM.password = user.password;
                UVM.userFullAddress = user.userFullAddress;
                UVM.userWebsite = user.userWebsite;
                UVM.userMobile = user.userMobile;
                UVM.userDOB = (DateTime)user.userDOB;
                UVM.userImagePath = "";
                UVM.userDescription = user.userDescription;
                UVM.userEmail = user.userEmail;
                //UVM.userCity = user.userCity;
                //UVM.userState = user.userState;
                //UVM.userCountry = user.userCountry;
                //UVM.userPinCode = user.userPinCode;
                //UVM.userRoleID = 0;
                //UVM.userRoleName = null;
                UVM.strUserImageName = user.strUserImageName;
                UVM.byteUserImageData = [];
                UVM.strUserImageType = user.strUserImageType;
                UVM.emailConfirmed = user.emailConfirmed;
                //UVM.isActive = true;
                UVM.createdDate = user.createdDate;
                UVM.createdBy = user.createdBy;
                UVM.updatedDate = (DateTime)user.updatedDate;
                UVM.updatedBy = user.updatedBy;
                isCreated = PH.updateUser(UVM);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return isCreated;
        }
        public bool UploadUserImage(int userId, string fileName, string imageType, string createdBy, IFormFile imageFile)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            bool result = false;
            try
            {
                var response = AH.InsertUserImage(userId, fileName, imageType, createdBy, imageFile);
                if (response == "Image uploaded successfully.")
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        #endregion
    }
}






    


   

    

