namespace IndiaLivings_Web_UI.Models
{
    public class AdListFiltersViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<SearchFilterDetailsViewModel> Filters { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductViewModel> RecommendedAds { get; set; }
    }

    public class DashboardAdsViewModel
    {
        public List<FilteredAd> FeaturedAds { get; set; }
        public List<FilteredAd> RecommendedAds { get; set; }
        public List<FilteredAd> TopRatedAds { get; set; }
        public List<FilteredAd> TopEngagedAds { get; set; }
        public List<SearchFilterDetailsViewModel> Cities { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class FilteredAd
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

    // For faceted filters
    public class FilterCount
    {
        public string? FilterValue { get; set; }
        public int TotalCount { get; set; }
    }

    public class CategoryCount
    {
        public int CategoryId { get; set; }
        public int TotalCount { get; set; }
    }

    public class SubCategoryCount
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int TotalCount { get; set; }
    }

    public class RatingCount
    {
        public decimal Rating { get; set; }
        public int TotalCount { get; set; }
    }

    // Full API Response for ads
    public class AdsResponse
    {
        public List<FilteredAd> Ads { get; set; } = new();
        public int TotalRecords { get; set; }

        // Filter counts
        public List<FilterCount> Cities { get; set; } = new();
        public List<FilterCount> AdTypes { get; set; } = new();
        public List<CategoryCount> Categories { get; set; } = new();
        public List<SubCategoryCount> SubCategories { get; set; } = new();
        public List<RatingCount> Ratings { get; set; } = new();
        public List<CategoryViewModel> CategoryList { get; set; } = new();
        public List<SearchFilterDetailsViewModel> FilterDetails { get; set; } = new();
        public List<FilteredAd> FeaturedAds { get; set; } = new();
    }
}
