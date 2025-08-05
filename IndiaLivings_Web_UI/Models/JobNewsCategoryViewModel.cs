using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    public class JobNewsCategoryViewModel
    {
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int JobCount { get; set; } = 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsActive { get; set; } = true;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? UpdatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string UpdatedBy { get; set; } = string.Empty;

        public List<JobNewsCategoryViewModel> GetJobCategories()
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<JobNewsCategoryModel> categoryList = AH.GetJobCategoryModels();
            List<JobNewsCategoryViewModel> jobCategoriesList = [];
            try
            {
                if (categoryList != null && categoryList.Count > 0)
                {
                    foreach (var category in categoryList)
                    {
                        JobNewsCategoryViewModel categoryVM = new JobNewsCategoryViewModel
                        {
                            CategoryID = category.CategoryID,
                            CategoryName = category.CategoryName,
                            Description = category.Description,
                            JobCount = category.JobCount,
                            IsActive = category.IsActive,
                            CreatedDate = category.CreatedDate,
                            CreatedBy = category.CreatedBy,
                            UpdatedBy = category.UpdatedBy
                        };
                        jobCategoriesList.Add(categoryVM);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return jobCategoriesList;
        }
    }
}
