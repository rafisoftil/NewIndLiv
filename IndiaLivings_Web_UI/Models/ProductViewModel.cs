using IndiaLivings_Web_DAL;
using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;
namespace IndiaLivings_Web_UI.Models
{
    public class ProductViewModel
    {
        public int productId { get; set; } = 0;
        public string productName { get; set; } = string.Empty;
        public string productDescription { get; set; } = string.Empty;
        public string productAdTags { get; set; } = string.Empty;
        public decimal productPrice { get; set; } = 0;
        public int productQuantity { get; set; } = 0;
        public int productCondition { get; set; } = 0;
        public int productCategoryID { get; set; } = 0;
        public string productCategoryName { get; set; } = string.Empty;
        public int productsubCategoryID { get; set; } = 0;
        public string productSubCategoryName { get; set; } = string.Empty;
        public string productPriceCondition { get; set; } = string.Empty;
        public string productAdCategory { get; set; } = string.Empty;
        public string productImageName { get; set; } = string.Empty;
        public string productImagePath { get; set; } = string.Empty;
        public string productImageType { get; set; } = string.Empty;
        public bool productSold { get; set; }
        public int productOwner { get; set; } = 0;
        public string productOwnerName { get; set; } = string.Empty;
        public int productMembershipID { get; set; } = 0;
        public string productMembershipName { get; set; } = string.Empty;
        public bool productAdminReview { get; set; }
        public string productAdminReviewStatus { get; set; } = string.Empty;
        public string IsActiveStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
        public int Error_Id { get; set; } = 0;
        public string Error_Message { get; set; } = string.Empty;
        public byte[] byteProductImageData { get; set; }

        public List<ProductViewModel> GetAllWishlist(int userid)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var wishList = PH.GetWishlistItems(userid);

                if (wishList != null)
                {
                    foreach (var wishDetails in wishList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = wishDetails.productId;
                        product.productName = wishDetails.productName;
                        product.productImageName = wishDetails.productImageName;
                        product.productPrice = wishDetails.productPrice;
                        product.productDescription = wishDetails.productDescription;
                        product.byteProductImageData = wishDetails.byteProductImageData;
                        product.createdBy = wishDetails.createdBy;
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

        public string UpdateWishlist(int productID, int userID, string createdBy, int status)
        {
            string response = String.Empty;
            ProductHelper PH = new ProductHelper();
            
            try
            {
                response = PH.UpdateWishlist(productID, userID, createdBy, status);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ProductViewModel> AdsList(int status)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetAdsList(status);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = productDetails.productId;
                        product.productName = productDetails.productName;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productDescription = productDetails.productDescription;
                        product.productPrice = productDetails.productPrice;
                        product.productAdminReviewStatus = productDetails.productAdminReviewStatus;
                        product.productOwner = productDetails.productOwner;
                        product.IsActiveStatus = productDetails.IsActiveStatus;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.createdDate = productDetails.createdDate;
                        product.createdBy = productDetails.createdBy;
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

        public bool CreateNewAdd(ProductViewModel product)
        {
            bool isCreated = false;
            ProductHelper PH = new ProductHelper();
            try
            {
                ProductModel PVM = new ProductModel();
                PVM.productName=product.productName;
                PVM.productDescription=product.productDescription;
                PVM.productAdTags = product.productAdTags;
                PVM.productPrice = product.productPrice;
                PVM.productQuantity = product.productQuantity;
                PVM.productCondition = product.productCondition;
                PVM.productCategoryID = product.productCategoryID; 
                PVM.productCategoryName = product.productCategoryName;
                PVM.productsubCategoryID = product.productsubCategoryID;
                PVM.productSubCategoryName = product.productSubCategoryName;
                PVM.productPriceCondition = product.productPriceCondition;
                PVM.productAdCategory = product.productAdCategory;
                PVM.productImageName = product.productImageName;
                PVM.strProductImageName = product.productImageName;
                PVM.strProductImageType = product.productImageType;
                PVM.byteProductImageData = [];
                PVM.productImagePath = "";//  [];//productImage.OpenReadStream();
                                          //PVM. = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
                PVM.productSold = false;
                PVM.productOwner = product.productOwner;
                PVM.productOwnerName = product.productOwnerName;
                PVM.productAdminReview = product.productAdminReview;
                PVM.createdDate = product.createdDate;
                PVM.createdBy = product.createdBy;
                PVM.updatedDate =product.updatedDate;
                PVM.updatedBy = product.updatedBy;
                isCreated = PH.InsertProduct(PVM);
            }
            catch (Exception ex) { 
            }
            return isCreated;
        }

        public string UpdateAdStatus(int productid, bool status, string username)
        {
            ProductHelper PH = new ProductHelper();
            var updatedStatus = "";
            try
            {
                updatedStatus = PH.UpdateAdAdminReview(productid, status, username);

            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }


            return updatedStatus;
        }

        public List<ProductViewModel> GetAds(int ownerid)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetAdsByOwner(ownerid);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = productDetails.productId;
                        product.productName = productDetails.productName;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productDescription = productDetails.productDescription;
                        product.productPrice = productDetails.productPrice;
                        product.productAdminReviewStatus = productDetails.productAdminReviewStatus;
                        product.productOwner = productDetails.productOwner;
                        product.IsActiveStatus = productDetails.IsActiveStatus;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.byteProductImageData = productDetails.byteProductImageData;
                        product.createdDate = productDetails.createdDate;
                        product.createdBy = productDetails.createdBy;
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