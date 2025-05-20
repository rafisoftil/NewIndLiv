using IndiaLivings_Web_DAL;
using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    #region Properties
    public class PasswordResetViewModel
    {
        public int UserPasswordID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UserToken { get; set; }
        public DateTime UserTokenExpiration { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        #endregion

        public string AddPasswordReset(int userid, string username, string token, string expirationTime, string createdby)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string response = string.Empty;
            try
            {
                response = AH.AddPasswordReset(userid, username, token, expirationTime, createdby);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public List<PasswordResetViewModel> GetPasswordReset(string token)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<PasswordResetViewModel> passwordModel = new List<PasswordResetViewModel>();
            try
            {
                var response = AH.GetPasswordReset(token);
                foreach (var item in response) 
                {
                    PasswordResetViewModel model = new PasswordResetViewModel();
                    model.UserPasswordID = item.UserPasswordID;
                    model.UserID = item.UserID;
                    model.Username = item.Username;
                    model.UserToken = item.UserToken;
                    model.UserTokenExpiration = item.UserTokenExpiration;
                    model.CreatedDate = item.CreatedDate;
                    model.CreatedBy = item.CreatedBy;
                    model.UpdatedDate = item.UpdatedDate;
                    model.UpdatedBy = item.UpdatedBy;
                    passwordModel.Add(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return passwordModel;
        }

        public string PasswordReset(string newPassword, string token)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<PasswordResetViewModel> passwordModel = new List<PasswordResetViewModel>();
            string response = "Password Update Failed. Please check with Admin.";
            try
            {
                response = AH.PasswordReset(newPassword, token);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdatePassword(int userId, string newPassword)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<PasswordResetViewModel> passwordModel = new List<PasswordResetViewModel>();
            string response = "Password Update Failed. Please check with Admin.";
            try
            {
                response = AH.UpdatePassword(userId, newPassword);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }


}
