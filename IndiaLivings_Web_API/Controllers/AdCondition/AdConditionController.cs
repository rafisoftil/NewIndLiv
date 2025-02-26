using IndiaLivingsAPI.Model.AdConditions;
using IndiaLivingsAPI.Model.ErrorLogs;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers.AdCondition
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdConditionsController : ControllerBase
    {
        #region Membership

        // Sub Categories
        [HttpPost("addAdTypeCondtions")]
        public ActionResult insertAdConditions(string strAdConditionName, string strAdConditionType, string strCreatedBy)
        {
            clsAdCondition _AdCondition = new clsAdCondition();
            bool blnUserFlag = false;
            string strStatus = "Add AdConditions Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _AdCondition.insertAdCondition(strAdConditionName, strAdConditionType, strCreatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "AdConditions Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpPost("updateAdTypeCondtions")]
        public ActionResult updateAdConditions(int intAdConditionID, string strAdConditionName, string strAdConditionType, string strUpdatedBy)
        {
            clsAdCondition _AdCondition = new clsAdCondition();
            bool blnUserFlag = false;
            string strStatus = "AdConditions Update Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _AdCondition.updateAdCondition(intAdConditionID, strAdConditionName, strAdConditionType, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "AdConditions Update Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpDelete("deleteAdTypeCondtions")]
        public ActionResult DeleteAdConditions(int intAdConditionID, string strUpdatedBy)
        {
            clsAdCondition _AdCondition = new clsAdCondition();
            bool blnUserFlag = false;
            string strStatus = "AdConditions Delete Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _AdCondition.deleteAdCondition(intAdConditionID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "AdConditions Deleted Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpGet("GetAllAdConditions")]
        public List<AdConditionModel> GetAllAdConditions(int intAdConditionID)
        {
            List<AdConditionModel> _lstAdCondition = new List<AdConditionModel>();
            clsAdCondition _AdCondition = new clsAdCondition();
            try
            {
                _lstAdCondition = _AdCondition.viewAllAdConditions(intAdConditionID);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstAdCondition;

        }
        [HttpGet("GetAllAdConditionsTypeName")]
        public List<clsAdConditionType> GetAllAdConditionsTypeName(string strAdConditionTypeName = null)
        {
            List<clsAdConditionType> _lstAdCondition = new List<clsAdConditionType>();
            clsAdCondition _AdCondition = new clsAdCondition();
            if (string.IsNullOrEmpty(strAdConditionTypeName) == true)
            {
                strAdConditionTypeName = "";
            }
            try
            {
                _lstAdCondition = _AdCondition.viewAllAdConditionByType(strAdConditionTypeName);
            }
            catch (Exception ex)
            {

                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstAdCondition;

        }
        #endregion

    }
}
