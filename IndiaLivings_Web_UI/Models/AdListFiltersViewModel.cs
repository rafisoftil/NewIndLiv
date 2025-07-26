namespace IndiaLivings_Web_UI.Models
{
    public class AdListFiltersViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<SearchFilterDetailsViewModel> Filters { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductViewModel> RecommendedAds { get; set; }
    }
}
