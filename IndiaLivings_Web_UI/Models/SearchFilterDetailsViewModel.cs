using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class SearchFilterDetailsViewModel
    {
        public string CategoryType { get; set; }
        public string CategoryValue { get; set; }
        public int totalCount { get; set; }

        public async Task<List<SearchFilterDetailsViewModel>> GetSearchFilterDetails()
        {
            List<SearchFilterDetailsViewModel> filter = new List<SearchFilterDetailsViewModel>();
            try
            {
                var filterDetails = await ProductHelper.GetProductFilterDetails();
                if (filterDetails != null)
                {
                    foreach (var item in filterDetails)
                    {
                        SearchFilterDetailsViewModel filDetModel = new SearchFilterDetailsViewModel();
                        filDetModel.CategoryType = item.CategoryType;
                        filDetModel.CategoryValue = item.CategoryValue;
                        filDetModel.totalCount = item.totalCount;
                        filter.Add(filDetModel);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return filter;
        }

        public async Task<List<SearchFilterDetailsViewModel>> GetSearchFilter()
        {
            List<SearchFilterDetailsViewModel> filter = new List<SearchFilterDetailsViewModel>();
            try
            {
                var filterDetails = await ProductHelper.GetProductFilter();
                if (filterDetails != null)
                {
                    foreach (var item in filterDetails)
                    {
                        SearchFilterDetailsViewModel filDetModel = new SearchFilterDetailsViewModel();
                        filDetModel.CategoryType = item.CategoryType;
                        filDetModel.CategoryValue = item.CategoryValue;
                        filDetModel.totalCount = item.totalCount;
                        filter.Add(filDetModel);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return filter;
        }
    }
}
