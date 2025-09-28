using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ProductRatingViewModel
    {
        public int ratingId { get; set; }
        public int productId { get; set; }
        public int userId { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }

        public List<ProductRatingViewModel> GetProductRatings(int productId)
        {
            List<ProductRatingViewModel> ratings = new List<ProductRatingViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var ratingList = PH.GetProductRatings(productId);
                if (ratingList != null)
                {
                    foreach (var ratingDetails in ratingList)
                    {
                        ProductRatingViewModel rating = new ProductRatingViewModel();
                        rating.ratingId = ratingDetails.ratingId;
                        rating.productId = ratingDetails.productId;
                        rating.userId = ratingDetails.userId;
                        rating.rating = ratingDetails.rating;
                        rating.comment = ratingDetails.comment;
                        rating.userName = ratingDetails.userName;
                        rating.userEmail = ratingDetails.userEmail;
                        rating.createdDate = ratingDetails.createdDate;
                        ratings.Add(rating);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return ratings;
        }
    }
}
