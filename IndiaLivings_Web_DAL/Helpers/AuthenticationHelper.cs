using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            }
            return role;

        }

        public bool updateUser(UserModel user)
        {
            bool isInsert = false;
            
            var  response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Users/UpdateUser", user);

            if (!response.Contains("Error"))
                isInsert = true;
            return isInsert;

        }

        public List<UserModel> GetUserByUsername(string userName)
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Users/GetUserByUserName?strUserName={userName}");
                users = JsonConvert.DeserializeObject<List<UserModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }
            return users;
        }

       
    }
}