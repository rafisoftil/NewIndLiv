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
        public async Task<string> CreateSubCategory(ServiceSubCategoryViewModel subCategory)
        {
            string result = "An error occured";
            try
            {
                ServiceSubCategoryModel subCat = new ServiceSubCategoryModel()
                {
                    CategoryId = subCategory.CategoryId,
                    Name = subCategory.Name,
                    Description = subCategory.Description,
                    BasePrice = subCategory.BasePrice,
                    DurationMin = subCategory.DurationMin,
                    CreatedBy = subCategory.CreatedBy
                };
                result = await ServiceHelper.CreateServiceSubCategory(subCat);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public async Task<string> UpdateSubCategory(ServiceSubCategoryViewModel subCategory)
        {
            string result = "An error occured";
            try
            {
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
                result = await ServiceHelper.UpdateServiceSubCategory(subCat);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public async Task<string> DeleteSubCategory(int serviceId, string username)
        {
            string result = "An error occured";
            try
            {
                result = await ServiceHelper.DeleteServiceSubCategory(serviceId, username);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public async Task<List<ServiceSubCategoryViewModel>> GetAllSubCategories()
        {
            List<ServiceSubCategoryViewModel> subCategorieslst = new List<ServiceSubCategoryViewModel>();
            try
            {
                List<ServiceSubCategoryModel> subCategories = await ServiceHelper.GetAllServiceSubCategories();
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
        public async Task<ServiceSubCategoryViewModel> GetSubCategoryById(int serviceId)
        {
            ServiceSubCategoryViewModel serviceSubCategoryViewModel = new ServiceSubCategoryViewModel();
            try
            {
                ServiceSubCategoryModel subCat = await ServiceHelper.GetServiceSubCategoryById(serviceId);
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
        public async Task<List<ServiceSubCategoryViewModel>> GetSubServicesByCategory(int categoryId)
        {
            List<ServiceSubCategoryViewModel> subCategorieslst = new List<ServiceSubCategoryViewModel>();
            try
            {
                List<ServiceSubCategoryModel> subCategories = await ServiceHelper.GetSubServiceByCategory(categoryId);
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
