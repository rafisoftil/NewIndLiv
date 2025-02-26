using IndiaLivings_Web_DAL;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;

namespace IndiaLivingsAPI.Model.User
{
    public class clsUser
    {
        public int userID { get; set; } = 0;
        public string username { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string password { get; set; }
        public string userFirstName { get; set; } = string.Empty;
        public string userMiddleName { get; set; } = string.Empty;
        public string userLastName { get; set; } = string.Empty;
        public string userDescription { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string? userMobile { get; set; }
        public int userAddressID { get; set; } = 0;
        public string userFullAddress { get; set; } = string.Empty;
        public string userImagePath { get; set; } = string.Empty;
        public int userRoleID { get; set; } = 0;
        public string userRoleName { get; set; } = string.Empty;
        public string userWebsite { get; set; } = string.Empty;
        public DateTime? userDOB { get; set; } = null;
        public string emailConfirmed { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? createdDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string createdBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? updatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? updatedBy { get; set; }

        public string Error_Message { get; set; } = string.Empty;

        public string membershipName { get; set; } = string.Empty;
    }
    public class clsUserDetails
    {
        public bool InsertUser(clsUser user)
        {
            const string SP_Name = "usp_piInsertUser";
            int intUserRoleID = Convert.ToInt32(user.userRoleID);
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@userName", user.username, ParameterDirection.Input);
                _objDM.AddParameter("@userFirstName", user.userFirstName, ParameterDirection.Input);
                _objDM.AddParameter("@userMiddleName", user.userMiddleName, ParameterDirection.Input);
                _objDM.AddParameter("@userLastName", user.userLastName, ParameterDirection.Input);
                _objDM.AddParameter("@userDescription", user.userDescription, ParameterDirection.Input);
                _objDM.AddParameter("@useEmail", user.userEmail, ParameterDirection.Input);
                _objDM.AddParameter("@userMobile", user.userMobile, ParameterDirection.Input);
                _objDM.AddParameter("@userFullAddress", user.userFullAddress, ParameterDirection.Input);
                _objDM.AddParameter("@userImagePath", user.userImagePath, ParameterDirection.Input);
                _objDM.AddParameter("@userPassword", user.password, ParameterDirection.Input);
                _objDM.AddParameter("@userRoleID", intUserRoleID, ParameterDirection.Input);
                _objDM.AddParameter("@userWebsite", user.userWebsite, ParameterDirection.Input);
                _objDM.AddParameter("@userDOB", user.userDOB, ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", Convert.ToBoolean(user.IsActive), ParameterDirection.Input);
                _objDM.AddParameter("@createdBy", user.createdBy, ParameterDirection.Input);

                //   result = Convert.ToInt32(_objDM.GetScalar(SP_Name));
                object scalarResult = _objDM.GetScalar(SP_Name);

                // Check if the result is not null and is a valid numeric value
                if (scalarResult != DBNull.Value && int.TryParse(scalarResult.ToString(), out int _result))
                {
                    // If it's valid, assign it
                    result = _result;
                }
                else
                {
                    // Handle the error or assign a default value
                    result = 0; // or any default value you want to set
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }

        public List<clsUser> viewActiveUsers(string strUserName, int isActiveFlag)
        {
            const string SP_Name = "usp_GetALLUserListByUserName";
            DataSet ds = null;
            List<clsUser> lsUser = new List<clsUser>();
            clsUser _user = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@userID", strUserName, ParameterDirection.Input);
                _objDM.AddParameter("@isActiveFlag", isActiveFlag, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _user = new clsUser();
                        _user.userID = (int)ds.Tables[0].Rows[i]["userID"];
                        _user.username = ds.Tables[0].Rows[i]["userName"].ToString();
                        _user.userFirstName = ds.Tables[0].Rows[i]["userFirstName"].ToString();
                        _user.userMiddleName = ds.Tables[0].Rows[i]["userMiddleName"].ToString();
                        _user.userLastName = ds.Tables[0].Rows[i]["userLastName"].ToString();
                        _user.userDescription = ds.Tables[0].Rows[i]["userDescription"].ToString();
                        _user.userEmail = ds.Tables[0].Rows[i]["useEmail"].ToString();
                        _user.userMobile = ds.Tables[0].Rows[i]["userMobile"].ToString();
                        _user.userFullAddress = ds.Tables[0].Rows[i]["userFullAddress"].ToString();
                        _user.userImagePath = ds.Tables[0].Rows[i]["userImagePath"].ToString();
                        _user.userRoleID = (int)ds.Tables[0].Rows[i]["userRoleID"];
                        _user.userRoleName = ds.Tables[0].Rows[i]["membershipName"].ToString();
                        _user.userWebsite = ds.Tables[0].Rows[i]["userWebsite"].ToString();
                        _user.userDOB = (DateTime)ds.Tables[0].Rows[i]["userDOB"];
                        _user.IsActive = (bool)ds.Tables[0].Rows[i]["IsActive"];
                        _user.createdDate = (DateTime)ds.Tables[0].Rows[i]["createdDate"];
                        _user.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _user.updatedDate = (DateTime)ds.Tables[0].Rows[i]["updatedDate"];
                        _user.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();
                        // _user.updatedBy = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["updatedBy"].ToString()) ? null : ds.Tables[0].Rows[i]["updatedBy"].ToString();
                        lsUser.Add(_user);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsUser;
        }

        public List<clsUser> viewAllUsers(int intUserID, string email_id = null, string username = null)
        {
            const string SP_Name = "usp_GetALLUserList";
            DataSet ds = null;
            List<clsUser> lsUser = new List<clsUser>();
            clsUser _user = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@userID", intUserID, ParameterDirection.Input);
                _objDM.AddParameter("@email_id", email_id, ParameterDirection.Input);
                _objDM.AddParameter("@username", username, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _user = new clsUser();
                        _user.userID = (int)ds.Tables[0].Rows[i]["userID"];
                        _user.username = ds.Tables[0].Rows[i]["userName"].ToString();
                        _user.userFirstName = ds.Tables[0].Rows[i]["userFirstName"].ToString();
                        _user.userMiddleName = ds.Tables[0].Rows[i]["userMiddleName"].ToString();
                        _user.userLastName = ds.Tables[0].Rows[i]["userLastName"].ToString();
                        _user.userDescription = ds.Tables[0].Rows[i]["userDescription"].ToString();
                        _user.userEmail = ds.Tables[0].Rows[i]["useEmail"].ToString();
                        _user.userMobile = ds.Tables[0].Rows[i]["userMobile"].ToString();
                        _user.userFullAddress = ds.Tables[0].Rows[i]["userFullAddress"].ToString();
                        _user.userImagePath = ds.Tables[0].Rows[i]["userImagePath"].ToString();
                        _user.userRoleID = (int)ds.Tables[0].Rows[i]["userRoleID"];
                        _user.userRoleName = ds.Tables[0].Rows[i]["roleName"].ToString();
                        _user.membershipName = ds.Tables[0].Rows[i]["membershipName"].ToString();
                        _user.userWebsite = ds.Tables[0].Rows[i]["userWebsite"].ToString();
                        _user.userDOB = (DateTime)ds.Tables[0].Rows[i]["userDOB"];
                        _user.IsActive = (bool)ds.Tables[0].Rows[i]["IsActive"];
                        _user.createdDate = (DateTime)ds.Tables[0].Rows[i]["createdDate"];
                        _user.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _user.updatedDate = (DateTime)ds.Tables[0].Rows[i]["updatedDate"];
                        _user.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();
                        lsUser.Add(_user);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsUser;
        }


        public bool chkUserLogin(string strEmail, string strPWD)
        {
            const string SP_Name = "usp_chkUserLogin";
            SqlDataReader da = null;
            bool blnFlag = false;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@strEmail", strEmail, ParameterDirection.Input);
                _objDM.AddParameter("@strPWD", strPWD, ParameterDirection.Input);

                da = _objDM.GetDataReader(SP_Name);
                if (da.HasRows)
                {
                    da.Read();
                    blnFlag = Convert.ToBoolean(Convert.ToInt32(da["Flag"].ToString()));
                }

            }
            catch (Exception)
            {

                throw;
            }
            return blnFlag;
        }

        public bool UpdateUser(clsUser user)
        {
            const string SP_Name = "usp_UpdateUser";
            int intUserRoleID = Convert.ToInt32(user.userRoleID);
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@userID", user.userID, ParameterDirection.Input);
                _objDM.AddParameter("@userName", user.username, ParameterDirection.Input);
                _objDM.AddParameter("@userFirstName", user.userFirstName, ParameterDirection.Input);
                _objDM.AddParameter("@userMiddleName", user.userMiddleName, ParameterDirection.Input);
                _objDM.AddParameter("@userLastName", user.userLastName, ParameterDirection.Input);
                _objDM.AddParameter("@userDescription", user.userDescription, ParameterDirection.Input);
                _objDM.AddParameter("@useEmail", user.userEmail, ParameterDirection.Input);
                _objDM.AddParameter("@userMobile", user.userMobile, ParameterDirection.Input);
                _objDM.AddParameter("@userFullAddress", user.userFullAddress, ParameterDirection.Input);
                _objDM.AddParameter("@userImagePath", user.userImagePath, ParameterDirection.Input);
                _objDM.AddParameter("@userPassword", user.password, ParameterDirection.Input);
                _objDM.AddParameter("@userRoleID", intUserRoleID, ParameterDirection.Input);
                _objDM.AddParameter("@userWebsite", user.userWebsite, ParameterDirection.Input);
                _objDM.AddParameter("@userDOB", user.userDOB, ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", Convert.ToBoolean(user.IsActive), ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", user.updatedBy, ParameterDirection.Input);

                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool DeleteUser(int userID, string strUpdatedBy)
        {
            const string SP_Name = "usp_DeleteUser";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@userID", userID, ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", strUpdatedBy, ParameterDirection.Input);

                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
    }

}
