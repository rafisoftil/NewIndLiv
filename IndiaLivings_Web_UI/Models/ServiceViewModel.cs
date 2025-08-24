using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ServiceViewModel
    {
        public int CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int ServiceCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public List<ServiceViewModel> getAllActiveCategories()
        {
            List<ServiceViewModel> lstCategories = new List<ServiceViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            List<ServiceModel> categories = AH.GetActiveServiceCategories();
            foreach(var category in categories)
            {
                ServiceViewModel services = new ServiceViewModel();
                services.CategoryID = category.CategoryID;
                services.Name = category.Name;
                services.Slug = category.Slug;
                services.Description = category.Description;
                services.IsActive = category.IsActive;
                services.ServiceCount = category.ServiceCount;
                services.CreatedAt = category.CreatedAt;
                services.UpdatedAt = category.UpdatedAt;
                services.CreatedBy = category.CreatedBy;
                lstCategories.Add(services);
            }
            return lstCategories;
        }
    }
}
