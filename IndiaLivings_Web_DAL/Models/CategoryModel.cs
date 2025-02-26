
namespace IndiaLivings_Web_DAL.Models
{
    public class CategoryModel
    {
        public int intCategoryID { get; set; }
        public string strCategoryName { get; set; } = string.Empty;
        public string strCategoryImage { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string updatedBy { get; set; }
        public int intCategoryCount { get; set; }
        public List<SubCategoryModel> strSubCategoryDetails { get; set; }
    }
}
