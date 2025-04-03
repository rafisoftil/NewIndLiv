using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class MembershipViewModel
    {
        public int MembershipID { get; set; }
        public string MembershipName { get; set; }

        public string MembershipDescription { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        //public List<ProductViewModel> AdsList(int status)
        //{
        //    List<ProductViewModel> products = new List<ProductViewModel>();
        //    ProductHelper PH = new ProductHelper();
        //    try
        //    {
        //        var productList = PH.GetAdsList(status);
        //        if (productList != null)
        //        {
        //            foreach (var productDetails in productList)
        //            {
        //                ProductViewModel product = new ProductViewModel();
        //                product.productId = productDetails.productId;
        //                product.productName = productDetails.productName;
        //                product.productCategoryName = productDetails.productCategoryName;
        //                product.productDescription = productDetails.productDescription;
        //                product.productPrice = productDetails.productPrice;
        //                product.productAdminReviewStatus = productDetails.productAdminReviewStatus;
        //                product.productOwner = productDetails.productOwner;
        //                product.IsActiveStatus = productDetails.IsActiveStatus;
        //                product.productAdminReview = productDetails.productAdminReview;
        //                product.productPriceCondition = productDetails.productPriceCondition;
        //                product.createdDate = productDetails.createdDate;
        //                product.createdBy = productDetails.createdBy;
        //                products.Add(product);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
        //    }


        //    return products;
        //}
    }
}
