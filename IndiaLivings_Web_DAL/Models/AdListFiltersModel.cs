using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class AdListFiltersModel
    {
        public List<ProductModel> Products { get; set; }
        public List<SearchFilterDetailsModel> Filters { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
    public class FilteredAdModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductPriceCondition { get; set; }
        public string? ProductAdCategory { get; set; }
        public int ProductCategoryID { get; set; }
        public int ProductSubCategoryID { get; set; }
        public string? UserContactCity { get; set; }
        public bool ProductAdminReview { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ProductOwner { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string UserRoleID { get; set; }

        // Computed fields
        public int RatingCount { get; set; }
        public decimal AverageRating { get; set; }
        public int IsWishlisted { get; set; }
        public decimal RecommendationScore { get; set; }
        public int IsFeatured { get; set; }
        public int EngagementScore { get; set; }
        public int AdvertiserAdsCount { get; set; }
    }
}
