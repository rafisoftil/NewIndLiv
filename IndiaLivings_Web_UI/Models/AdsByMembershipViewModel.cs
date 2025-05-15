using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class AdsByMembershipViewModel
    {
        public int userTotalAdsPosted { get; set; }
        public int userMembershipAds { get; set; }
        public int userTotalAdsRemaining { get; set; }
        public int userMembershipID { get; set; }
        public int userMemberID { get; set; }
        public string userMemberName { get; set; }

        public List<AdsByMembershipViewModel> GetUserAdsRemaining(int userId)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<AdsByMembershipViewModel> adsRemaining = new List<AdsByMembershipViewModel>();
            try
            {
                List<AdsByMembershipModel> adRemInfo = AH.GetUserAdsRemaining(userId);
                AdsByMembershipViewModel adRemDetails = new AdsByMembershipViewModel();
                adRemDetails.userTotalAdsPosted = adRemInfo[0].userTotalAdsPosted;
                adRemDetails.userMembershipAds = adRemInfo[0].userMembershipAds;
                adRemDetails.userTotalAdsRemaining = adRemInfo[0].userTotalAdsRemaining;
                adRemDetails.userMembershipID = adRemInfo[0].userMembershipID;
                adRemDetails.userMemberID = adRemInfo[0].userMemberID;
                adRemDetails.userMemberName = adRemInfo[0].userMemberName;
                adsRemaining.Add(adRemDetails);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return adsRemaining;
        }
    }
}
