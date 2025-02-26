using IndiaLivingsAPI.Model.ErrorLogs;
using IndiaLivingsAPI.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        #region Membership

        // Sub Categories
        [HttpPost("addMembership")]
        public ActionResult insertMembership(string strMembershipName, int intMembershipAdsLimit, decimal decMembershipPrice, string strMembershipDescription, string strCreatedBy)
        {
            clsMembershipDetails _membershipDetails = new clsMembershipDetails();
            bool blnUserFlag = false;
            string strStatus = "Add Membership Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _membershipDetails.insertMembership(strMembershipName, intMembershipAdsLimit, decMembershipPrice,strMembershipDescription, strCreatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Membership Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpPost("updateMembership")]
        public ActionResult updateMembership(int intMembershipID, string strMembershipName, int intMembershipAdsLimit, decimal decMembershipPrice, string strMembershipDescription, string strUpdatedBy)
        {
            clsMembershipDetails _membershipDetails = new clsMembershipDetails();
            bool blnUserFlag = false;
            string strStatus = "Membership Update Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _membershipDetails.updateMembership(intMembershipID, strMembershipName, intMembershipAdsLimit, decMembershipPrice,strMembershipDescription, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Membership Update Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpDelete("deleteMembership")]
        public ActionResult DeleteMembership(int intMembershipID, string strUpdatedBy)
        {
            clsMembershipDetails _membershipDetails = new clsMembershipDetails();
            bool blnUserFlag = false;
            string strStatus = "Membership Delete Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _membershipDetails.deleteMembership(intMembershipID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Membership Deleted Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpGet("GetAllListofMembership")]
        public List<Membership> GetAllMembership(int intMembershipID)
        {
            List<Membership> _lstMembership = new List<Membership>();
            clsMembershipDetails _membershipDetails = new clsMembershipDetails();
            try
            {
                _lstMembership = _membershipDetails.viewAllMembership(intMembershipID);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstMembership;

        }
        #endregion
    }
}
