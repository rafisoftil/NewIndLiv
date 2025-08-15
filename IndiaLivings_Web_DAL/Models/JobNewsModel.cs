using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class JobNewsModel
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
        public DateTime? PublishedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

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
    }
}
