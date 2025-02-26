using IndiaLivingsAPI.Model.ErrorLogs;
using IndiaLivingsAPI.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet("CheckUserLogin")]
        public bool chkUserLogin(string strEmail, string strPWD)
        {
            bool blnUserFlag = false;
            clsUserDetails _userDetails = new clsUserDetails();
            try
            {
                blnUserFlag = _userDetails.chkUserLogin(strEmail, strPWD);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return blnUserFlag;

        }

        [HttpPost("AddUser")]
        public ActionResult AddUser(clsUser user)
        {
            clsUserDetails _userDetail = new clsUserDetails();
            bool blnUserFlag = false;
            string strStatus = "Add User Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _userDetail.InsertUser(user);
                if (blnUserFlag == true)
                {
                    strStatus = "User Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpGet("GetActiveListofUsers")]
        public IActionResult GetActiveListofUsers()
        {
            string strUserName = null;
            List<clsUser> _lstUsers = new List<clsUser>();
            clsUserDetails _userDetail = new clsUserDetails();
            string strJsonResponse = null;
            try
            {
                _lstUsers = _userDetail.viewActiveUsers(strUserName, 1);
                strJsonResponse = clsHelper.RemoveEmptyProps(_lstUsers);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return Ok(strJsonResponse);

        }

        [HttpGet("GetInActiveListofUsers")]
        public IActionResult GetInActiveListofUsers()
        {
            string strUserName = null;
            List<clsUser> _lstUsers = new List<clsUser>();
            clsUserDetails _userDetail = new clsUserDetails();
            string strJsonResponse = null;
            try
            {
                _lstUsers = _userDetail.viewActiveUsers(strUserName, 0);
                strJsonResponse = clsHelper.RemoveEmptyProps(_lstUsers);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return Ok(strJsonResponse);

        }

        [HttpGet("GetUserDetailsByUserName")]
        public string GetUserDetailsByUserName(string strUserName)
        {
            List<clsUser> _lstUsers = new List<clsUser>();
            clsUserDetails _userDetail = new clsUserDetails();
            string strJsonResponse = null;
            try
            {
                _lstUsers = _userDetail.viewActiveUsers(strUserName, 1);
                strJsonResponse = clsHelper.RemoveEmptyProps(_lstUsers);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return strJsonResponse ;

        }

        [HttpPost("UpdateUser")]
        public ActionResult UpdateUser(clsUser _user)
        {
            clsUserDetails _userDetail = new clsUserDetails();
            bool blnUserFlag = false;
            string strStatus = "User Update Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _userDetail.UpdateUser(_user);
                if (blnUserFlag == true)
                {
                    strStatus = "User Update Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int UserID, string strUpdatedBy)
        {
            clsUserDetails _userDetail = new clsUserDetails();
            bool blnUserFlag = false;
            string strStatus = "User Delete Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _userDetail.DeleteUser(UserID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "User Deleted Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpGet("GetAllListofUsers")]
        public List<clsUser> GetAllUsers(int intUserID, string email_id = null, string username = null)
        {
            List<clsUser> _lstUsers = new List<clsUser>();
            clsUserDetails _userDetail = new clsUserDetails();
            try
            {
                _lstUsers = _userDetail.viewAllUsers(intUserID, email_id, username);
            }
            catch (Exception ex)
            {

                throw;
            }
            return _lstUsers;

        }

    }
}
