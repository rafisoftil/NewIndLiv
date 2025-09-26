using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ServiceViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Image { get; set; } = string.Empty;
        public int ServiceCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public double? Rating { get; set; }
        public int? RatingCount { get; set; }

        public List<ServiceViewModel> getActiveCategories()
        {
            List<ServiceViewModel> lstCategories = new List<ServiceViewModel>();
            ServiceHelper SH = new ServiceHelper();
            List<ServiceModel> categories = SH.GetActiveServiceCategories();
            foreach(var category in categories)
            {
                ServiceViewModel services = new ServiceViewModel();
                services.CategoryId = category.CategoryId;
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
        public List<ServiceViewModel> getAllCategories()
        {
            List<ServiceViewModel> lstCategories = new List<ServiceViewModel>();
            ServiceHelper SH = new ServiceHelper();
            List<ServiceModel> categories = SH.GetAllServices();
            foreach (var category in categories)
            {
                ServiceViewModel services = new ServiceViewModel();
                services.CategoryId = category.CategoryId;
                services.Name = category.Name;
                services.Slug = category.Slug;
                services.Description = category.Description;
                services.IsActive = category.IsActive;
                services.Image = category.Image;
                services.ServiceCount = category.ServiceCount;
                services.CreatedAt = category.CreatedAt;
                services.UpdatedAt = category.UpdatedAt;
                services.CreatedBy = category.CreatedBy;
                lstCategories.Add(services);
            }
            return lstCategories;
        }

        public string CreateServiceCategory(string name, string slug, string description, string image, string username)
        {
            ServiceHelper SH = new ServiceHelper();
            string result = "An error occured";
            try
            {
                ServiceModel service = new ServiceModel();
                service.Name = name;
                service.Slug = slug;
                service.Description = description;
                service.Image = string.Empty;
                service.CreatedBy = username;
                result = SH.CreateServiceCategory(service);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }            
            return result;
        }
        public string UpdateServiceCategory(int categoryId, string name, string slug, string description, bool isActive, string username)
        {
            ServiceHelper SH = new ServiceHelper();
            string result = "An error occured";
            try
            {
                ServiceCategoryUpdateRequest service = new ServiceCategoryUpdateRequest();
                service.CategoryId = categoryId;
                service.Name = name;
                service.Slug = slug;
                service.Description = description;
                service.IsActive = isActive;
                service.UpdatedBy = username;
                result = SH.UpdateServiceCategory(service);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string DeleteServiceCategory(int categoryId, string username)
        {
            ServiceHelper SH = new ServiceHelper();
            string result = "An error occured";
            try
            {
                result = SH.DeleteServiceCategory(categoryId, username);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public ServiceViewModel ViewServiceCategory(int categoryId)
        {
            ServiceViewModel service = new ServiceViewModel();
            ServiceHelper SH = new ServiceHelper();
            try
            {
                ServiceModel category = SH.ViewServiceSubCategory(categoryId);
                service.CategoryId = category.CategoryId;
                service.Name = category.Name;
                service.Slug = category.Slug;
                service.Description = category.Description;
                service.IsActive = category.IsActive;
                service.ServiceCount = category.ServiceCount;
                service.CreatedAt = category.CreatedAt;
                service.UpdatedAt = category.UpdatedAt;
                service.CreatedBy = category.CreatedBy;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return service;
        }
    }
}
