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

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        //public List<SubCategoryModel>? subCategories { get; set; }

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
            //var categoryList = ServiceAPI.GetApi("https://api.indialivings.com/api/Category/GetActiveListofCategory?intCategoryID=0");
            //var cats = JsonConvert.DeserializeObject<List<Category>>(categoryList);
            ////categoryList.ToList();
            //foreach (var category in cats)
            //{
            //    CategoryViewModel categoryModel = new CategoryViewModel();
            //    categoryModel.CategoryID = category.intCategoryID;
            //    categoryModel.CategoryName = category.strCategoryName;
            //    categoryModel.CategoryImage = category.strCategoryImage;
            //    categoryModel.IsActive = category.IsActive;
            //    categories.Add(categoryModel);
            //}
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
                    categories.Add(categoryModel);
                }
            }
            catch (Exception ex) {

            }

            return categories;
        }
        #endregion
    }
}
