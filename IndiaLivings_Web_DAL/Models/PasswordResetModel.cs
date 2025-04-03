using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class PasswordReset
    {
        public int UserPasswordID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UserToken { get; set; }
        public DateTime UserTokenExpiration { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
