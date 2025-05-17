using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class AdsByMembershipModel
    {
        public int userTotalAdsPosted { get; set; }
        public int userMembershipAds { get; set; }
        public int userTotalAdsRemaining { get; set; }
        public int userMembershipID { get; set; }
        public int userMemberID { get; set; }
        public string userMemberName { get; set; }
    }
}
