using IndiaLivings_Web_DAL.Helpers;

namespace IndiaLivings_Web_UI.Models
{
    public class UserViewModel
    {
        #region Properties
        public int userID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userFirstName { get; set; }
        public string userMiddleName { get; set; }
        public string userLastName { get; set; }
        public string userDescription { get; set; }
        public string userEmail { get; set; }
        public string? userMobile { get; set; }
        public int userAddressID { get; set; }
        public string userFullAddress { get; set; }
        public string userImagePath { get; set; }
        public int userRoleID { get; set; } = 0;
        public string userRoleName { get; set; }
        public string userWebsite { get; set; }
        public DateTime? userDOB { get; set; }
        public string emailConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedBy { get; set; }

        public string Error_Message { get; set; }

        public string membershipName { get; set; }

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
                var userDetails = authenticationHelper.validateUser(UserName,password);
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

            }

            return user;
        }

        public bool RegisterUser()
        {
            bool isInsert = false;
            return isInsert;
        }
        #endregion
    }
}
