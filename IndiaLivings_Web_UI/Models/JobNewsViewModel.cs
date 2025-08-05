using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    public class JobNewsViewModel
    {
        public int JobNewsID { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobLocation { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty; // Full-time, Part-time, Contract, etc.
        public string ExperienceLevel { get; set; } = string.Empty; // Entry, Mid, Senior, etc.
        public string SalaryRange { get; set; } = string.Empty;
        public string ApplicationDeadline { get; set; } = string.Empty;
        public string ApplicationUrl { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public int ViewCount { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedDate { get; set; } = DateTime.Now;
        public DateTime? ExpiryDate { get; set; } = DateTime.Now.AddDays(90);

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsActive { get; set; } = true;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? UpdatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string UpdatedBy { get; set; } = string.Empty;

        public string CreateJob(JobNewsViewModel Job)
        {
            string response = "An error occured";
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                JobNewsModel jobModel = new JobNewsModel
                {
                    JobNewsID = Job.JobNewsID,
                    Title = Job.Title,
                    Content = Job.Content,
                    Summary = Job.Summary,
                    CompanyName = Job.CompanyName,
                    JobLocation = Job.JobLocation,
                    JobType = Job.JobType,
                    ExperienceLevel = Job.ExperienceLevel,
                    SalaryRange = Job.SalaryRange,
                    ApplicationDeadline = Job.ApplicationDeadline,
                    ApplicationUrl = Job.ApplicationUrl,
                    ContactEmail = Job.ContactEmail,
                    FeaturedImage = Job.FeaturedImage,
                    Tags = Job.Tags,
                    CategoryID = Job.CategoryID,
                    CategoryName = Job.CategoryName,
                    ViewCount = Job.ViewCount,
                    IsFeatured = Job.IsFeatured,
                    IsPublished = Job.IsPublished,
                    PublishedDate = Job.PublishedDate ?? DateTime.Now,
                    ExpiryDate = Job.ExpiryDate ?? DateTime.Now.AddDays(30),
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Job.CreatedBy
                };
                response = AH.CreateJob(jobModel);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public JobNewsViewModel GetJobNewsByID(int jobId)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            JobNewsModel job = AH.GetJobNewsByID(jobId);
            JobNewsViewModel jobNews = null;
            try
            {
                if (job != null)
                {
                    jobNews = new JobNewsViewModel
                    {
                        JobNewsID = job.JobNewsID,
                        Title = job.Title,
                        Content = job.Content,
                        Summary = job.Summary,
                        CompanyName = job.CompanyName,
                        JobLocation = job.JobLocation,
                        JobType = job.JobType,
                        ExperienceLevel = job.ExperienceLevel,
                        SalaryRange = job.SalaryRange,
                        ApplicationDeadline = job.ApplicationDeadline,
                        ApplicationUrl = job.ApplicationUrl,
                        ContactEmail = job.ContactEmail,
                        FeaturedImage = job.FeaturedImage,
                        Tags = job.Tags,
                        CategoryID = job.CategoryID,
                        CategoryName = job.CategoryName,
                        ViewCount = job.ViewCount,
                        IsFeatured = job.IsFeatured,
                        IsPublished = job.IsPublished,
                        PublishedDate = job.PublishedDate,
                        IsActive = job.IsActive,
                        CreatedDate = job.CreatedDate,
                        CreatedBy = job.CreatedBy,
                        UpdatedDate = job.UpdatedDate,
                        UpdatedBy = job.UpdatedBy
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return jobNews;
        }
        public List<JobNewsViewModel> GetAllJobNews(int pageNumber, int pageSize, int categoryId, bool publishedOnly, bool activeOnly)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<JobNewsModel> jobList = AH.GetAllJobNews(pageNumber, pageSize, categoryId, publishedOnly, activeOnly);
            List<JobNewsViewModel> jobNews = new List<JobNewsViewModel>();
            try
            {
                if (jobList != null && jobList.Count > 0)
                {
                    foreach (var job in jobList)
                    {
                        JobNewsViewModel jobVM = new JobNewsViewModel
                        {
                            JobNewsID = job.JobNewsID,
                            Title = job.Title,
                            Content = job.Content,
                            Summary = job.Summary,
                            CompanyName = job.CompanyName,
                            JobLocation = job.JobLocation,
                            JobType = job.JobType,
                            ExperienceLevel = job.ExperienceLevel,
                            SalaryRange = job.SalaryRange,
                            ApplicationDeadline = job.ApplicationDeadline,
                            ApplicationUrl = job.ApplicationUrl,
                            ContactEmail = job.ContactEmail,
                            FeaturedImage = job.FeaturedImage,
                            Tags = job.Tags,
                            CategoryID = job.CategoryID,
                            CategoryName = job.CategoryName,
                            ViewCount = job.ViewCount,
                            IsFeatured = job.IsFeatured,
                            IsPublished = job.IsPublished,
                            PublishedDate = job.PublishedDate,
                            IsActive = job.IsActive,
                            CreatedDate = job.CreatedDate,
                            CreatedBy = job.CreatedBy,
                            UpdatedDate = job.UpdatedDate,
                            UpdatedBy = job.UpdatedBy
                        };
                        jobNews.Add(jobVM);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return jobNews;
        }
        public string UpdateJob(JobNewsViewModel Job)
        {
            string response = "An error occured";
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                JobNewsModel jobModel = new JobNewsModel
                {
                    JobNewsID = Job.JobNewsID,
                    Title = Job.Title,
                    Content = Job.Content,
                    Summary = Job.Summary,
                    CompanyName = Job.CompanyName,
                    JobLocation = Job.JobLocation,
                    JobType = Job.JobType,
                    ExperienceLevel = Job.ExperienceLevel,
                    SalaryRange = Job.SalaryRange,
                    ApplicationDeadline = Job.ApplicationDeadline,
                    ApplicationUrl = Job.ApplicationUrl,
                    ContactEmail = Job.ContactEmail,
                    FeaturedImage = Job.FeaturedImage,
                    Tags = Job.Tags,
                    CategoryID = Job.CategoryID,
                    CategoryName = Job.CategoryName,
                    ViewCount = Job.ViewCount,
                    IsFeatured = Job.IsFeatured,
                    IsPublished = Job.IsPublished,
                    PublishedDate = DateTime.Now, // Assuming you want to update the published date to now
                    ExpiryDate = DateTime.Now.AddDays(30), // Assuming you want to set a default expiry date
                    IsActive = true, // Assuming you want to keep it active
                    CreatedDate = DateTime.Now, // Assuming you want to update the created date to now
                    CreatedBy = Job.CreatedBy, // Assuming you want to keep the original creator
                    UpdatedDate = DateTime.Now, // Set the updated date to now
                    UpdatedBy = Job.UpdatedBy // Keep the updated by field
                };
                response = AH.UpdateJobNews(jobModel);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteJob(int jobId, string updatedBy)
        {
            string response = "An error occured";
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                response = AH.DeleteJobNews(jobId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
