using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    public class SubCategoryViewModel
    {
        #region Properties
        public int subCategoryID { get; set; }
        public string subCatergoryName { get; set; }
        public string strCategoryName { get; set; }
        public int intCategoryID { get; set; } = 0;
        public bool IsActive { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public int intSubCategoryCount { get; set; }
        #endregion

        #region Methods
        public List<SubCategoryViewModel> GetSubCategories(int categoryId) {
            List<SubCategoryViewModel> subCategories=new List<SubCategoryViewModel>();
            ProductHelper productHelper = new ProductHelper();
            try
            {
                var subcategories = productHelper.GetSubCategories(categoryId);
                foreach (var subcategory in subcategories)
                {
                    SubCategoryViewModel subCat = new SubCategoryViewModel();
                    subCat.subCategoryID = subcategory.subCategoryID;
                    subCat.subCatergoryName = subcategory.subCatergoryName;
                    subCat.IsActive = subcategory.IsActive;
                    subCat.intCategoryID = subcategory.intCategoryID;
                    subCategories.Add(subCat);
                }
            }
            catch (Exception ex) {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategories;
        }
        #endregion
    }
}
