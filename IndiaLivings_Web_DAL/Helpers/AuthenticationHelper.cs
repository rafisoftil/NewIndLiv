using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Newtonsoft.Json;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class AuthenticationHelper : IAuthenticationRepository
    {
        public bool registerUser(UserModel user)
        {
            bool isRegistered = false;
            var response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Users/AddUser", user);
            if (response == "1")
                isRegistered = true;
            return isRegistered;

        }

        public bool checkDuplicate(string userName)
        {
            dynamic user = null;
            bool isUserExists = false;
            var response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Users/GetUserByUserName?strUserName=" + userName);
            user = JsonConvert.DeserializeObject(response);
            if (user.Count > 0)
                isUserExists = true;
            return isUserExists;
        }

        public void updateUser()
        {
        }

        public void deleteUser() { }

        public List<UserModel> ActiveUserList()
        {
            List<UserModel> users = new List<UserModel>();
            var userList = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Users/GetActiveListofUsers");
            users = JsonConvert.DeserializeObject<List<UserModel>>(userList);
            return users;

        }
        public UserModel validateUser(string userName, string password)
        {
            UserModel user = null;
            try
            {
                user = new UserModel();
                string response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Users/CheckUserLogin?strUserName=" + userName + "&strPWD=" + password);
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
                var response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Users/GetRoles");
                role = JsonConvert.DeserializeObject<List<RoleModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return role;

        }

        public bool updateUser(UserModel user)
        {
            bool isInsert = false;
            try
            {
                var response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Users/UpdateUser", user);
                if (!response.Contains("Error"))
                    isInsert = true;

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return isInsert;
        }
        public string UpdateAddress(int intUserID, string strUserContactFullAddress,string strUserContactCity,string strUserContactState,string strUserContactCountry,string strUserContactPinCode , int intUserAddressType)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Users/AddUserAddress?intUserID={intUserID}&strUserContactFullAddress={strUserContactFullAddress}&strUserContactCity={strUserContactCity}&strUserContactState={strUserContactState}&strUserContactCountry={strUserContactCountry}&strUserContactPinCode={strUserContactPinCode}&intUserAddressType={intUserAddressType}");
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
                var response = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Users/GetUserByUserName?strUserName={userName}");
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
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Users/AddUserPasswordReset?intUserID={userid}&strUserName={username}&strUserPasswordToken={token}&dtmUserTokenExpiration={expirationTime}&createdBy={createdby}");

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
                var response = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Users/GetUserPasswordReset?strUserPasswordToken={token}");
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
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Users/UpdatePasswordfromReset?tokenId={token}&newPassword={newPassword}").Trim('\"');

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
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Users/UpdateUserPassword?userId={userId}&newPassword={newPassword}").Trim('\"');

            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}