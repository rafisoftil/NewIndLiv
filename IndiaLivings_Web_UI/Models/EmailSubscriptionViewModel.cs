using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Razorpay.Api;

namespace IndiaLivings_Web_UI.Models
{
    public class EmailSubscriptionViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public async Task<string> Subscribe(EmailSubscriptionViewModel subscribe)
        {
            string response = string.Empty;
            EmailSubscriptionModel sub = new EmailSubscriptionModel
            {
                Email = subscribe.Email,
                FullName = subscribe.FullName,
                //Token = subscribe.Token
            };
            try
            {
                response = await AuthenticationHelper.Subscribe(sub);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public async Task<string> VerifySubscription(string token)
        {
            string response = string.Empty;
            try
            {
                response = await AuthenticationHelper.VerifySubscription(token);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public async Task<string> Unsubscribe(string token)
        {
            string response = string.Empty;
            try
            {
                response = await AuthenticationHelper.Unsubscribe(token);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
