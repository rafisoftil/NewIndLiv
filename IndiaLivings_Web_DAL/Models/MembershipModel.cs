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

    public class MembershipUpgradePreviewModel
    {
        public int intUserID { get; set; }
        public int intCurrentMembershipID { get; set; }
        public string strCurrentMembershipName { get; set; } = string.Empty;
        public int intCurrentMembershipAds { get; set; }
        public int intCurrentAdsPosted { get; set; }
        public int intCurrentAdsRemaining { get; set; }
        public int intNewMembershipID { get; set; }
        public string strNewMembershipName { get; set; } = string.Empty;
        public int intNewMembershipAds { get; set; }
        public decimal decNewMembershipPrice { get; set; }
        public int intTotalAdsAfterUpgrade { get; set; }
        public string strUpgradeMessage { get; set; } = string.Empty;
    }
}
