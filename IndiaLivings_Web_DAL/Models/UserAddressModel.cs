using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class UserAddressModel
    {
   
        public int intUserID { get; set; }
        public string strUserContactFullAddress { get; set; }
        public string strUserContactCity { get; set; }
        public string strUserContactState { get; set; }
        public string strUserContactCountry { get; set; }
        public string strUserContactPinCode { get; set; }
        public int intUserAddressType { get; set; }

    }
}
