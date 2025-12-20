using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace IndiaLivings_Web_UI.Models
{
    public class MembershipViewModel
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

        public List<MembershipViewModel> GetMembershipDetails(int userId)
        {
            List<MembershipViewModel> memDetails = new List<MembershipViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var memDetail = AH.GetMembershipDetails(userId);
                if (memDetails != null)
                {
                    MembershipViewModel mem = new MembershipViewModel();
                    mem.intMembershipID = memDetail[0].intMembershipID;
                    mem.intMembershipUserID = memDetail[0].intMembershipUserID;
                    mem.strMembershipName = memDetail[0].strMembershipName;
                    mem.strMembershipDescription = memDetail[0].strMembershipDescription;
                    mem.intMembershipAdListing = memDetail[0].intMembershipAdListing;
                    mem.decMembershipPrice = memDetail[0].decMembershipPrice;
                    mem.IsActive = memDetail[0].IsActive;
                    mem.CreatedDate = memDetail[0].CreatedDate;
                    mem.CreatedBy = memDetail[0].CreatedBy;
                    mem.UpdatedDate = memDetail[0].UpdatedDate;
                    mem.UpdatedBy = memDetail[0].UpdatedBy;
                    memDetails.Add(mem);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return memDetails;
        }
        public List<MembershipViewModel> GetAllListofMembership(int memId)
        {
            List<MembershipViewModel> memDetails = new List<MembershipViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                List<MembershipModel> details = AH.GetAllListofMembership(memId);
                foreach (var mem in details)
                {
                    MembershipViewModel memViewModel = new MembershipViewModel();
                    memViewModel.intMembershipID = mem.intMembershipID;
                    memViewModel.intMembershipUserID = mem.intMembershipUserID;
                    memViewModel.strMembershipName = mem.strMembershipName;
                    memViewModel.strMembershipDescription = mem.strMembershipDescription;
                    memViewModel.intMembershipAdListing = mem.intMembershipAdListing;
                    memViewModel.decMembershipPrice = mem.decMembershipPrice;
                    memViewModel.IsActive = mem.IsActive;
                    memViewModel.CreatedDate = mem.CreatedDate;
                    memViewModel.CreatedBy = mem.CreatedBy;
                    memViewModel.UpdatedDate = mem.UpdatedDate;
                    memViewModel.UpdatedBy = mem.UpdatedBy;
                    memDetails.Add(memViewModel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return memDetails;
        }
        public string AddUserMembership(int MembershipId, int userId, string createdBy)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string response = "Membership insertion Unsuccessful";
            try
            {
                response = AH.AddUserMembership(MembershipId, userId, createdBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateMembership(int intMembershipID, string strMembershipName, int intMembershipAdsLimit, double decMembershipPrice, string strMembershipDescription, string strUpdatedBy)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string response = "Membership Update Unsuccessful";
            try
            {
                response = AH.UpdateMembership(intMembershipID, strMembershipName, intMembershipAdsLimit, decMembershipPrice, strMembershipDescription, strUpdatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteMembership(int intMembershipID, string strUpdatedBy)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string response = "Membership Deletion Unsuccessful";
            try
            {
                response = AH.DeleteMembership(intMembershipID, strUpdatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }

    public class MembershipUpgradePreviewViewModel
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

        public async Task<MembershipUpgradePreviewViewModel> GetMembershipUpgradePreviewViewModels(int userId, int newMembershipId)
        {
            MembershipUpgradePreviewViewModel memUpgradeDetails = new MembershipUpgradePreviewViewModel();
            
            try
            {
                MembershipUpgradePreviewModel memUpgradeDetail = await AuthenticationHelper.GetMembershipUpgradePreview(userId, newMembershipId);
                if (memUpgradeDetail != null)
                {
                    memUpgradeDetails.intUserID = memUpgradeDetail.intUserID;
                    memUpgradeDetails.intCurrentMembershipID = memUpgradeDetail.intCurrentMembershipID;
                    memUpgradeDetails.strCurrentMembershipName = memUpgradeDetail.strCurrentMembershipName;
                    memUpgradeDetails.intCurrentMembershipAds = memUpgradeDetail.intCurrentMembershipAds;
                    memUpgradeDetails.intCurrentAdsPosted = memUpgradeDetail.intCurrentAdsPosted;
                    memUpgradeDetails.intCurrentAdsRemaining = memUpgradeDetail.intCurrentAdsRemaining;
                    memUpgradeDetails.intNewMembershipID = memUpgradeDetail.intNewMembershipID;
                    memUpgradeDetails.strNewMembershipName = memUpgradeDetail.strNewMembershipName;
                    memUpgradeDetails.intNewMembershipAds = memUpgradeDetail.intNewMembershipAds;
                    memUpgradeDetails.decNewMembershipPrice = memUpgradeDetail.decNewMembershipPrice;
                    memUpgradeDetails.intTotalAdsAfterUpgrade = memUpgradeDetail.intTotalAdsAfterUpgrade;
                    memUpgradeDetails.strUpgradeMessage = memUpgradeDetail.strUpgradeMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return memUpgradeDetails;
        }

        public async Task<string> UpgradeMembership(int userId, int currentMembership, int newMembershipId, string updatedBy)
        {
            string response = "Membership Upgrade Unsuccessful";
            try
            {
                response = await AuthenticationHelper.UpgradeUserMembership(userId, currentMembership, newMembershipId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }   
    }
}
