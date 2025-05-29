using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
   public class MembershipModel
    {
        public int intMembershipID { get; set; }
        public int intMembershipUserID { get; set; }
        public string strMembershipName { get; set; }
        public string strMembershipDescription { get; set; }
        public string intMembershipAdListing { get; set; }
        public Decimal decMembershipPrice { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
