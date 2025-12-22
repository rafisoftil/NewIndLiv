using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace IndiaLivings_Web_UI.Controllers
{
    public class NewsletterController : Controller
    {
        public async Task<IActionResult> Newsletter()
        {
            NewsletterViewModel newsletterViewModel = null;
            return View(newsletterViewModel);
        }
        public async Task<JsonResult> PostNewsletter(NewsletterViewModel newsletter)
        {
            try
            {
                var response = await new NewsletterViewModel().PostNewsletter(newsletter);
                var message = JObject.Parse(response)["message"]?.ToString();
                var newsletterId = JObject.Parse(response)["NewsletterID"]?.ToObject<int>() ?? 0;
                return Json(new { message = message, newsletterId = newsletterId });
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return Json(new { message = "An error occured while saving newsletter", newsletterId = 0 });
            }
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
        public async Task<JsonResult> DeleteNewsletter(int newsletterId)
        {
            string updatedBy = HttpContext.Session.GetString("UserName") ?? "Unknown";
            try
            {
                var response = await new NewsletterViewModel().DeleteNewsletter(newsletterId, updatedBy);
                var message = JObject.Parse(response)["message"]?.ToString();
                return Json(new { message = message });
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return Json(new
                {
                    message = "An error ocuured. Please try again after sometime"
                });
            }
            
        }
        public async Task<JsonResult> PublishNewsletter(int newsletterId, string emails, bool sendToAll = false)
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
                return Json(JObject.Parse(response));
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return Json(new
                {
                    message = "An error ocuured. Please try again after sometime"
                });
            }
        }
    }
}
