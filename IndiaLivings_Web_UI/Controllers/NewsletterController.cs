using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class NewsletterController : Controller
    {
        public async Task<IActionResult> Newsletter()
        {
            return View();
        }
        public async Task<string> PostNewsletter(NewsletterViewModel newsletter)
        {
            string response = "An error occured";
            try
            {
                response = await new NewsletterViewModel().PostNewsletter(newsletter);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public async Task<IActionResult> NewslettersList()
        {
            List<NewsletterViewModel> newsletters = new List<NewsletterViewModel>();
            try
            {
                newsletters = await new NewsletterViewModel().GetAllNewsletter();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return View(newsletters);
        }
        public async Task<IActionResult> NewsletterDetails(int newsletterId)
        {
            NewsletterViewModel newsletter = new NewsletterViewModel();
            try
            {
                newsletter = await new NewsletterViewModel().GetNewsletterByID(newsletterId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return View("Newsletter", newsletter);
        }
        public async Task<string> DeleteNewsletter(int newsletterId, string updatedBy)
        {
            string response = "An error occured";
            try
            {
                response = await new NewsletterViewModel().DeleteNewsletter(newsletterId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public async Task<string> PublishNewsletter(int newsletterId, string emails, bool sendToAll=false)
        {
            string response = "An error occured";
            try
            {
                SendNewsletterRequestViewModel sendRequest = new SendNewsletterRequestViewModel
                {
                    NewsletterID = newsletterId,
                    SendToAll = sendToAll,
                    SpecificEmails = string.IsNullOrEmpty(emails) ? new List<string>() : emails.Split(',').ToList()
                };
                response = await new SendNewsletterRequestViewModel().SendNewsletter(sendRequest);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
