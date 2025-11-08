using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace IndiaLivings_Web_UI.Controllers
{
    public class JobNewsController : Controller
    {
        public IActionResult JobDetails()
        {
            return View();
        }
        public IActionResult CreateJob()
        {
            List<JobNewsCategoryViewModel> categoriesList = new JobNewsCategoryViewModel().GetJobCategories();
            dynamic data = new ExpandoObject();
            data.Categories = categoriesList;
            data.JobNews = null;
            return View(data);
        }
        public IActionResult JobPost(IFormCollection formData)
        {
            JobNewsViewModel jobViewModel = new JobNewsViewModel();
            jobViewModel.Title = formData["Title"];
            jobViewModel.Content = formData["Content"];
            jobViewModel.Summary = formData["Summary"];
            jobViewModel.CompanyName = formData["CompanyName"];
            jobViewModel.JobLocation = formData["JobLocation"];
            jobViewModel.JobType = formData["JobType"];
            jobViewModel.ExperienceLevel = formData["ExperienceLevel"];
            jobViewModel.SalaryRange = formData["SalaryRange"];
            jobViewModel.ApplicationDeadline = formData["Deadline"];
            jobViewModel.ApplicationUrl = formData["ApplicationUrl"];
            jobViewModel.ContactEmail = formData["Email"];
            jobViewModel.FeaturedImage = "";
            jobViewModel.Tags = formData["Tags"];
            jobViewModel.CategoryID = int.Parse(formData["CategoryID"]);
            jobViewModel.CategoryName = formData["CategoryName"].ToString().Trim();
            jobViewModel.ViewCount = 0;
            jobViewModel.CreatedBy = HttpContext.Session.GetString("userName");
            jobViewModel.UpdatedBy = HttpContext.Session.GetString("userName");
            string response = jobViewModel.CreateJob(jobViewModel);
            return RedirectToAction("CreateJob");
        }
        public IActionResult JobInfo(int pageNumber = 1, int pageSize = 10, int categoryId = 0, bool publishedOnly = false, bool activeOnly = false)
        {
            JobNewsViewModel jobNews = new JobNewsViewModel();
            List<JobNewsViewModel> jobList = jobNews.GetAllJobNews(pageNumber, pageSize, categoryId, publishedOnly, activeOnly);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.Count = jobList.Count();
            return View(jobList);
        }
        public IActionResult GetJobNewsByID(int jobId)
        {
            JobNewsViewModel jobNews = new JobNewsViewModel().GetJobNewsByID(jobId);
            return PartialView("_JobByID", jobNews);
        }
        public IActionResult UpdateJob(int jobId)
        {
            JobNewsViewModel jobVM = new JobNewsViewModel();
            JobNewsViewModel job = jobVM.GetJobNewsByID(jobId);
            JobNewsCategoryViewModel jobCatVM = new JobNewsCategoryViewModel();
            List<JobNewsCategoryViewModel> jobCategories = jobCatVM.GetJobCategories();

            dynamic data = new ExpandoObject();
            data.JobNews = job;
            data.Categories = jobCategories;

            return View("CreateJob", data);
        }
        public string EditJob(IFormCollection formData)
        {
            JobNewsViewModel jobViewModel = new JobNewsViewModel();
            jobViewModel.JobNewsID = int.Parse(formData["JobNewsID"]);
            jobViewModel.Title = formData["Title"];
            jobViewModel.Content = formData["Content"];
            jobViewModel.Summary = formData["Summary"];
            jobViewModel.CompanyName = formData["CompanyName"];
            jobViewModel.JobLocation = formData["JobLocation"];
            jobViewModel.JobType = formData["JobType"];
            jobViewModel.ExperienceLevel = formData["ExperienceLevel"];
            jobViewModel.SalaryRange = formData["SalaryRange"];
            jobViewModel.ApplicationDeadline = formData["Deadline"];
            jobViewModel.ApplicationUrl = formData["ApplicationUrl"];
            jobViewModel.ContactEmail = formData["Email"];
            jobViewModel.FeaturedImage = "";
            jobViewModel.Tags = formData["Tags"];
            jobViewModel.CategoryID = int.Parse(formData["CategoryID"]);
            jobViewModel.CategoryName = formData["CategoryName"].ToString().Trim();
            jobViewModel.ViewCount = 0;
            jobViewModel.UpdatedBy = HttpContext.Session.GetString("userName");
            string response = jobViewModel.UpdateJob(jobViewModel);
            return response;
        }
        public IActionResult DeleteJob(int jobId)
        {
            JobNewsViewModel jobViewModel = new JobNewsViewModel();
            string updatedBy = HttpContext.Session.GetString("userName");
            string response = jobViewModel.DeleteJob(jobId, updatedBy);
            return RedirectToAction("JobInfo");
        }
    }
}
