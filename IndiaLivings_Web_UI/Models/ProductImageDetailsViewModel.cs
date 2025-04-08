using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class ProductImageDetailsViewModel
    {
        public int intProductImageID { get; set; }
        public int intProductID { get; set; }
        public string strProductImageName { get; set; }
        public byte[] byteProductImageData { get; set; }
        public string strProductImageType { get; set; }
        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;

        public List<ProductImageDetailsViewModel> GetImage(int productId)
        {
            List<ProductImageDetailsViewModel> products = new List<ProductImageDetailsViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetProductImage(productId);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductImageDetailsViewModel product = new ProductImageDetailsViewModel();
                        product.intProductImageID = productDetails.intProductImageID;
                        product.intProductID = productDetails.intProductID;
                        product.strProductImageName = productDetails.strProductImageName;
                        product.byteProductImageData = productDetails.byteProductImageData;
                        product.strProductImageType = productDetails.strProductImageType;
                        product.IsActive = productDetails.IsActive;
                        product.createdDate = productDetails.createdDate;
                        product.createdBy = productDetails.createdBy;
                        product.updatedDate = productDetails.updatedDate;
                        product.updatedBy = productDetails.updatedBy;
                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;

        }
    }
}
