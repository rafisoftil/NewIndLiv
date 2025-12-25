using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ServicesSubCategoriesViewModel
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double BasePrice { get; set; }
        public int DurationMin { get; set; }
        public bool IsActive { get; set; }
        public int ProviderCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public async Task<List<ServicesSubCategoriesViewModel>> GetAllActiveSubCategoriesByCategoryId(int categoryId)
        {
            List<ServicesSubCategoriesViewModel> lstSubCategories = new List<ServicesSubCategoriesViewModel>();
            List<ServicesSubCategoriesModel> subCategories = await ServiceHelper.GetServiceSubCategories(categoryId);
            foreach (var subCategory in subCategories)
            {
                ServicesSubCategoriesViewModel servicesSubCategories = new ServicesSubCategoriesViewModel();
                servicesSubCategories.ServiceId = subCategory.ServiceId;
                servicesSubCategories.CategoryId = subCategory.CategoryId;
                servicesSubCategories.CategoryName = subCategory.CategoryName;
                servicesSubCategories.Name = subCategory.Name;
                servicesSubCategories.Description = subCategory.Description;
                servicesSubCategories.BasePrice = subCategory.BasePrice;
                servicesSubCategories.DurationMin = subCategory.DurationMin;
                servicesSubCategories.IsActive = subCategory.IsActive;
                servicesSubCategories.ProviderCount = subCategory.ProviderCount;
                servicesSubCategories.CreatedAt = subCategory.CreatedAt;
                servicesSubCategories.UpdatedAt = subCategory.UpdatedAt;
                servicesSubCategories.CreatedBy = subCategory.CreatedBy;
                lstSubCategories.Add(servicesSubCategories);
            }
            return lstSubCategories;
        }
    }
}
