using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Repositories
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// To Create a new user
        /// </summary>
        public void registerUser();
        /// <summary>
        /// To Verify whether User exists or not
        /// </summary>
        /// <returns> returns true if user exists if not, returns false</returns>
        public bool validateUser();
    }
}
