using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class UserModel
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

    }
}
