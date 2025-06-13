using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IndiaLivings_Web_UI.Models
{
    public class CategoryViewModel
    {
        #region Properties

        public int CategoryCount { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }
        public string SubCategoryName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public List<SubCategoryViewModel>? subCategories { get; set; }

        #endregion

        #region Methods       

        /// <summary>
        /// To get All the categories
        /// </summary>
        /// <returns>category List</returns>
        public List<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            ProductHelper category = new ProductHelper();
            var lst = category.GetCategoriesCount();
            return categories;
        }

        public List<CategoryViewModel> GetCategoryCount()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            ProductHelper category = new ProductHelper();
            try
            {
                var lst = category.GetCategoriesCount();

                foreach (CategoryModel cat in lst)
                {
                    CategoryViewModel categoryModel = new CategoryViewModel();
                    categoryModel.CategoryID = cat.intCategoryID;
                    categoryModel.CategoryName = cat.strCategoryName;
                    categoryModel.CategoryImage = cat.strCategoryImage;
                    categoryModel.CategoryCount = cat.intCategoryCount;
                    List<SubCategoryViewModel> subCategories = new List<SubCategoryViewModel>();
                    foreach (var subcat in cat.strSubCategoryDetails)
                    {
                        SubCategoryViewModel subCategory = new SubCategoryViewModel();
                        subCategory.subCategoryID = subcat.subCategoryID;
                        subCategory.subCatergoryName = subcat.subCatergoryName;
                        subCategory.intCategoryID = subcat.subCategoryID;
                        subCategory.intSubCategoryCount = subcat.intSubCategoryCount;
                        subCategories.Add(subCategory);
                    }
                    categoryModel.subCategories = subCategories;
                    categories.Add(categoryModel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return categories;
        }
        #endregion
    }
}
