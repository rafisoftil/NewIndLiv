using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class NewsletterHelper
    {
        public static async Task<string> CreateNewsletter(NewsletterModel newsletter)
        {
            string result = "An error occured";
            try
            {
                result = await ServiceAPI.PostApiAsync("EmailSubscription/CreateNewsletter", newsletter);
                //response = JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public static async Task<string> UpdateNewsletter(NewsletterModel newsletter)
        {
            string response = "An error occured";
            try
            {
                var result = await ServiceAPI.PostApiAsync("EmailSubscription/UpdateNewsletter", newsletter);
                response = JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> DeleteNewsletter(int newsletterId, string updatedBy)
        {
            string result = "An error occured";
            try
            {
                result = await ServiceAPI.PostApiAsync($"EmailSubscription/DeleteNewsletter?newsletterId={newsletterId}&updatedBy={updatedBy}");
                //response = JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public static async Task<List<NewsletterModel>> GetAllNewsletters()
        {
            List<NewsletterModel> newsletter = new List<NewsletterModel>();
            try
            {
                var result = await ServiceAPI.GetAsyncApi("EmailSubscription/GetAllNewsletters");
                newsletter = JsonConvert.DeserializeObject<List<NewsletterModel>>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return newsletter;
        }
        public static async Task<NewsletterModel> GetNewsletterById(int newsletterId)
        {
            NewsletterModel newsletter = new NewsletterModel();
            try
            {
                var result = await ServiceAPI.GetAsyncApi($"EmailSubscription/GetNewsletter/{newsletterId}?newsletterId={newsletterId}");
                newsletter = JsonConvert.DeserializeObject<NewsletterModel>(result);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return newsletter;
        }
        public static async Task<string> SendNewsletter(SendNewsletterRequestModel sendNewsletter)
        {
            string result = "An error occured";
            try
            {
                result = await ServiceAPI.PostApiAsync("EmailSubscription/SendNewsletter", sendNewsletter);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
    }
}
