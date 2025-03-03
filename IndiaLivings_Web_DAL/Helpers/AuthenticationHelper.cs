using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Newtonsoft.Json;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class AuthenticationHelper : IAuthenticationRepository
    {

        //AuthenticationRepository _authenticationRepository;
        public void registerUser()
        {

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
                string response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Users/CheckUserLogin?strUserName="+userName+"&strPWD="+password);
                user = JsonConvert.DeserializeObject<UserModel>(response);
            }
            catch (Exception ex)
            {

            }
            return user;
        }
    }
}
