using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class UserModel
    {
        #region Properties
        public int userID { get; set; } = 0;
        public string user { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string password { get; set; }
        public string userFirstName { get; set; } = string.Empty;
        public string userMiddleName { get; set; } = string.Empty;
        public string userLastName { get; set; } = string.Empty;
        public string userDescription { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string userMobile { get; set; } = string.Empty;
        public int userAddressID { get; set; } = 0;
        public string userFullAddress { get; set; } = string.Empty;
        public string userImagePath { get; set; } = string.Empty;
        public int userRoleID { get; set; } = 0;
        public string userRoleName { get; set; } = string.Empty;
        public string userWebsite { get; set; } = string.Empty;
        public DateTime userDOB { get; set; } = DateTime.MinValue;
        public string emailConfirmed { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? createdDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string createdBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? updatedBy { get; set; }
        public string Error_Message { get; set; } = string.Empty;
        public string membershipName { get; set; } = string.Empty;

        public string strUserImageName { get; set; } = string.Empty;

        public string strUserImageType { get; set; } = string.Empty;

        public byte[] byteUserImageData { get; set; } = [];
        public string userCity { get; set; } = string.Empty;
        public string userState { get; set; } = string.Empty;
        public string userCountry { get; set; } = string.Empty;
        public string userPinCode { get; set; } = string.Empty;
        public string userCompany { get; set; } = string.Empty;
        public int UnreadCount { get; set; } = 0;

        public List<UserAddressModel> userAddressInfo { get; set; } = [];
        public string MessageText { get; set; } = string.Empty;
        #endregion

    }
}
