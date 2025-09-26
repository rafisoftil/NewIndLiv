using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ServiceSubCategoryViewModel
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? ProviderName { get; set; }
        public decimal? BasePrice { get; set; }
        public bool status { get; set; } = true;
        public bool IsActive { get; set; }
        public int? DurationMin { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string CreateSubCategory(ServiceSubCategoryViewModel subCategory)
        {
            string result = "An error occured";
            try
            {
                ServiceHelper SH = new ServiceHelper();
                ServiceSubCategoryModel subCat = new ServiceSubCategoryModel()
                {
                    CategoryId = subCategory.CategoryId,
                    Name = subCategory.Name,
                    Description = subCategory.Description,
                    BasePrice = subCategory.BasePrice,
                    DurationMin = subCategory.DurationMin,
                    CreatedBy = subCategory.CreatedBy
                };
                result = SH.CreateServiceSubCategory(subCat);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string UpdateSubCategory(ServiceSubCategoryViewModel subCategory)
        {
            string result = "An error occured";
            try
            {
                ServiceHelper SH = new ServiceHelper();
                ServiceUpdateRequest subCat = new ServiceUpdateRequest()
                {
                    CategoryId = subCategory.CategoryId,
                    ServiceId = subCategory.ServiceId,
                    Name = subCategory.Name,
                    Description = subCategory.Description,
                    BasePrice = subCategory.BasePrice,
                    DurationMin = subCategory.DurationMin,
                    UpdatedBy = subCategory.UpdatedBy
                };
                result = SH.UpdateServiceSubCategory(subCat);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string DeleteSubCategory(int serviceId, string username)
        {
            string result = "An error occured";
            try
            {
                ServiceHelper SH = new ServiceHelper();
                result = SH.DeleteServiceSubCategory(serviceId, username);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public List<ServiceSubCategoryViewModel> GetAllSubCategories()
        {
            List<ServiceSubCategoryViewModel> subCategorieslst = new List<ServiceSubCategoryViewModel>();
            try
            {
                ServiceHelper SH = new ServiceHelper();
                List<ServiceSubCategoryModel> subCategories = SH.GetAllServiceSubCategories();
                foreach (var subCat in subCategories)
                {
                    ServiceSubCategoryViewModel serviceSubCategoryViewModel = new ServiceSubCategoryViewModel();
                    serviceSubCategoryViewModel.CategoryId = subCat.CategoryId;
                    serviceSubCategoryViewModel.Name = subCat.Name;
                    serviceSubCategoryViewModel.Description = subCat.Description;
                    serviceSubCategoryViewModel.CategoryName = subCat.CategoryName;
                    serviceSubCategoryViewModel.ProviderName = subCat.ProviderName;
                    serviceSubCategoryViewModel.BasePrice = subCat.BasePrice;
                    serviceSubCategoryViewModel.DurationMin = subCat.DurationMin;
                    serviceSubCategoryViewModel.CreatedBy = subCat.CreatedBy;
                    subCategorieslst.Add(serviceSubCategoryViewModel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategorieslst;
        }
        public ServiceSubCategoryViewModel GetSubCategoryById(int serviceId)
        {
            ServiceSubCategoryViewModel serviceSubCategoryViewModel = new ServiceSubCategoryViewModel();
            try
            {
                ServiceHelper SH = new ServiceHelper();
                ServiceSubCategoryModel subCat = SH.GetServiceSubCategoryById(serviceId);
                if (subCat != null)
                {
                    serviceSubCategoryViewModel.CategoryId = subCat.CategoryId;
                    serviceSubCategoryViewModel.Name = subCat.Name;
                    serviceSubCategoryViewModel.Description = subCat.Description;
                    serviceSubCategoryViewModel.BasePrice = subCat.BasePrice;
                    serviceSubCategoryViewModel.DurationMin = subCat.DurationMin;
                    serviceSubCategoryViewModel.CreatedBy = subCat.CreatedBy;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return serviceSubCategoryViewModel;
        }
        public List<ServiceSubCategoryViewModel> GetSubServicesByCategory(int categoryId)
        {
            List<ServiceSubCategoryViewModel> subCategorieslst = new List<ServiceSubCategoryViewModel>();
            try
            {
                ServiceHelper SH = new ServiceHelper();
                List<ServiceSubCategoryModel> subCategories = SH.GetSubServiceByCategory(categoryId);
                foreach (var subCat in subCategories)
                {
                    ServiceSubCategoryViewModel serviceSubCategoryViewModel = new ServiceSubCategoryViewModel();
                    serviceSubCategoryViewModel.CategoryId = subCat.CategoryId;
                    serviceSubCategoryViewModel.Name = subCat.Name;
                    serviceSubCategoryViewModel.Description = subCat.Description;
                    serviceSubCategoryViewModel.CategoryName = subCat.CategoryName;
                    serviceSubCategoryViewModel.ProviderName = subCat.ProviderName;
                    serviceSubCategoryViewModel. ServiceId = subCat.ServiceId;
                    serviceSubCategoryViewModel.IsActive = subCat.IsActive;
                    serviceSubCategoryViewModel.BasePrice = subCat.BasePrice;
                    serviceSubCategoryViewModel.DurationMin = subCat.DurationMin;
                    serviceSubCategoryViewModel.CreatedBy = subCat.CreatedBy;
                    subCategorieslst.Add(serviceSubCategoryViewModel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategorieslst;
        }
    }
}
