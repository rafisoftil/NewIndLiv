using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndiaLivings_Web_DAL.Repositories;

namespace IndiaLivings_Web_DAL.Helpers
{
    internal class AuthenticationHelper : IAuthenticationRepository
    {

        //AuthenticationRepository _authenticationRepository;
        public void registerUser()
        {

        }

        public void updateUser()
        {
        }

        public void deleteUser() { }

        public bool validateUser()
        {
            bool isUserExist = false;
            try
            {
                var user = ServiceAPI.Get_async_Api("CheckUserLogin");

            }
            catch (Exception ex)
            {

            }
            return isUserExist;
        }
    }
}
