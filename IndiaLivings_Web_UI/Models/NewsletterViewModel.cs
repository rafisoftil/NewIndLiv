using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    public class NewsletterViewModel
    {
        public int NewsletterID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string HtmlBody { get; set; } = string.Empty;
        public string PlainTextBody { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public DateTime? ScheduledDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string Status { get; set; } = "Draft"; // Draft, Scheduled, Sending, Sent, Failed
        public int TotalRecipients { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? UpdatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string UpdatedBy { get; set; } = string.Empty;

        public async Task<string> PostNewsletter(NewsletterViewModel newsletter)
        {
            string response = "An error occured";
            try
            {
                NewsletterModel nl = new NewsletterModel
                {
                    NewsletterID = newsletter.NewsletterID,
                    Subject = newsletter.Subject,
                    HtmlBody = newsletter.HtmlBody,
                    PlainTextBody = newsletter.PlainTextBody,
                    SenderName = newsletter.SenderName,
                    SenderEmail = newsletter.SenderEmail,
                    ScheduledDate = newsletter.ScheduledDate,
                    SentDate = newsletter.SentDate,
                    Status = newsletter.Status,
                    TotalRecipients = newsletter.TotalRecipients,
                    SuccessCount = newsletter.SuccessCount,
                    FailureCount = newsletter.FailureCount,
                    IsActive = newsletter.IsActive,
                    CreatedDate = newsletter.CreatedDate,
                    CreatedBy = newsletter.CreatedBy,
                    UpdatedDate = newsletter.UpdatedDate,
                    UpdatedBy = newsletter.UpdatedBy
                };
                response = await NewsletterHelper.CreateNewsletter(nl);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public async Task<List<NewsletterViewModel>> GetAllNewsletter()
        {
            List<NewsletterViewModel> newsletter = new List<NewsletterViewModel>();
            try
            {
                List<NewsletterModel> nl = await NewsletterHelper.GetAllNewsletters();
                foreach (var item in nl)
                {
                    newsletter.Add(new NewsletterViewModel
                    {
                        NewsletterID = item.NewsletterID,
                        Subject = item.Subject,
                        HtmlBody = item.HtmlBody,
                        PlainTextBody = item.PlainTextBody,
                        SenderName = item.SenderName,
                        SenderEmail = item.SenderEmail,
                        ScheduledDate = item.ScheduledDate,
                        SentDate = item.SentDate,
                        Status = item.Status,
                        TotalRecipients = item.TotalRecipients,
                        SuccessCount = item.SuccessCount,
                        FailureCount = item.FailureCount,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedBy = item.UpdatedBy
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return newsletter;
        }
        public async Task<NewsletterViewModel> GetNewsletterByID(int newsletterId)
        {
            NewsletterViewModel newsletter = new NewsletterViewModel();
            try
            {
                NewsletterModel item = await NewsletterHelper.GetNewsletterById(newsletterId);
                if (item != null)
                {
                    newsletter = new NewsletterViewModel
                    {
                        NewsletterID = item.NewsletterID,
                        Subject = item.Subject,
                        HtmlBody = item.HtmlBody,
                        PlainTextBody = item.PlainTextBody,
                        SenderName = item.SenderName,
                        SenderEmail = item.SenderEmail,
                        ScheduledDate = item.ScheduledDate,
                        SentDate = item.SentDate,
                        Status = item.Status,
                        TotalRecipients = item.TotalRecipients,
                        SuccessCount = item.SuccessCount,
                        FailureCount = item.FailureCount,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedBy = item.UpdatedBy
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return newsletter;
        }
        public async Task<string> DeleteNewsletter(int newsletterId, string updatedBy)
        {
            string response = "An error occured";
            try
            {
                response = await NewsletterHelper.DeleteNewsletter(newsletterId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
    public class SendNewsletterRequestViewModel
    {
        public int NewsletterID { get; set; }
        public bool SendToAll { get; set; } = true;
        public List<string> SpecificEmails { get; set; } = new List<string>();

        public async Task<string> SendNewsletter(SendNewsletterRequestViewModel request)
        {
            string response = "An error occured";
            try
            {
                SendNewsletterRequestModel req = new SendNewsletterRequestModel
                {
                    NewsletterID = request.NewsletterID,
                    SendToAll = request.SendToAll,
                    SpecificEmails = request.SpecificEmails
                };
                response = await NewsletterHelper.SendNewsletter(req);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
