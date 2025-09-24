using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace IndiaLivings_Web_UI.Models
{
    public class ProductViewModel
    {
        public int productCount { get; set; }
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
        public int productImageId { get; set; } = 0;
        public string productImageName { get; set; } = string.Empty;
        public string productImagePath { get; set; } = string.Empty;
        public bool productSold { get; set; }
        public int productOwner { get; set; } = 0;
        public string userContactCity { get; set; } = string.Empty;
        public string userContactState { get; set; } = string.Empty;
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
        public byte[] byteProductImageData { get; set; } = [];
        public string ProductType { get; set; } = string.Empty;
        public string ProductSearchText { get; set; } = string.Empty;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 0;

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
                        product.productCategoryName = wishDetails.productCategoryName;
                        product.productSubCategoryName = wishDetails.productSubCategoryName;
                        product.productPrice = wishDetails.productPrice;
                        product.productAdCategory = wishDetails.productAdCategory;
                        product.productDescription = wishDetails.productDescription;
                        product.byteProductImageData = wishDetails.byteProductImageData;
                        product.createdBy = wishDetails.createdBy;
                        product.updatedDate = wishDetails.updatedDate;
                        product.userContactCity = wishDetails.userContactCity;
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

        public int GetwishlistCount(int productOwnerID)
        {
            try
            {
                ProductHelper productHelper = new ProductHelper();
                int wishlistCount = productHelper.GetCount(productOwnerID);
                return wishlistCount;
            }
            catch (Exception ex)
            {

                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return 0;
            }
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
                        product.productOwnerName = productDetails.productOwnerName;
                        product.IsActiveStatus = productDetails.IsActiveStatus;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.productCategoryID = productDetails.productCategoryID;
                        product.createdDate = productDetails.createdDate;
                        product.createdBy = productDetails.createdBy;
                        product.updatedDate = productDetails.updatedDate;
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


        public bool CreateNewAdd(ProductViewModel product, IFormFile productImage)
        {
            bool isCreated = false;
            int productId = 0;
            ProductHelper PH = new ProductHelper();
            try
            {
                ProductModel PVM = new ProductModel();
                PVM.productId = product.productId;
                PVM.productName = product.productName;
                PVM.productDescription = product.productDescription;
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
                PVM.productImageId = product.productImageId;
                PVM.productImageName = product.productImageName;
                PVM.strProductImageName = product.productImageName;
                PVM.strProductImageType = Path.GetExtension(product.productImageName)?.TrimStart('.').ToLower();
                PVM.byteProductImageData = product.byteProductImageData;
                PVM.productImagePath = "";//  [];//productImage.OpenReadStream();
                                          //PVM. = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
                PVM.productSold = false;
                PVM.productOwner = product.productOwner;
                PVM.productOwnerName = product.productOwnerName;
                PVM.productAdminReview = product.productAdminReview;
                PVM.createdDate = product.createdDate;
                PVM.createdBy = product.createdBy;
                PVM.updatedDate = product.updatedDate;
                PVM.updatedBy = product.updatedBy;
                PVM.userContactCity = product.userContactCity;
                PVM.userContactState = product.userContactState;
                productId = PH.InsertProduct(PVM);
                if (PVM.productId > 0)
                {
                    isCreated = PH.InserProductImage(PVM.productId, PVM.productImageId, PVM.productImageName, PVM.strProductImageType, PVM.createdBy, productImage);
                }
                else
                {
                    isCreated = PH.InserProductImage(productId, PVM.productImageId, PVM.productImageName, PVM.strProductImageType, PVM.createdBy, productImage);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
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
                        product.productAdCategory = productDetails.productAdCategory;
                        product.productCategoryID = productDetails.productCategoryID;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productDescription = productDetails.productDescription;
                        product.productSubCategoryName = productDetails.productSubCategoryName;
                        product.productsubCategoryID = productDetails.productsubCategoryID;
                        product.productQuantity = productDetails.productQuantity;
                        product.productPrice = productDetails.productPrice;
                        product.productAdminReviewStatus = productDetails.productAdminReviewStatus;
                        product.productOwner = productDetails.productOwner;
                        product.userContactCity = productDetails.userContactCity;
                        product.IsActiveStatus = productDetails.IsActiveStatus;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.byteProductImageData = productDetails.byteProductImageData;
                        product.productMembershipID = productDetails.productMembershipID;
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
        public List<ProductViewModel> GetProducts(int productCategoryID)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetProduct(productCategoryID);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = productDetails.productId;
                        product.productName = productDetails.productName;
                        product.productDescription = productDetails.productDescription;
                        product.productAdTags = productDetails.productAdTags;
                        product.productPrice = productDetails.productPrice;
                        product.productQuantity = productDetails.productQuantity;
                        product.productCondition = productDetails.productCondition;
                        product.productCategoryID = productDetails.productCategoryID;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productsubCategoryID = productDetails.productsubCategoryID;
                        product.productSubCategoryName = productDetails.productSubCategoryName;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.productAdCategory = productDetails.productAdCategory;
                        product.productOwner = productDetails.productOwner;
                        product.userContactCity = productDetails.userContactCity;
                        product.productOwnerName = productDetails.productOwnerName;
                        product.productMembershipID = productDetails.productMembershipID;
                        product.productMembershipName = productDetails.productMembershipName;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.byteProductImageData = productDetails.byteProductImageData;
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
        public List<ProductViewModel> GetProductsList(string strProductName, string strCity, string strState, decimal decMinPrice, decimal decMaxPrice, string strSearchType, string strSearchText)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetProductList(strProductName, strCity, strState, decMinPrice, decMaxPrice, strSearchType, strSearchText);
                //var filteredProducts = productList[0].ToJson();
                if (productList != null)
                {
                    var filteredProducts = JsonConvert.DeserializeObject<dynamic>(productList);
                    //filteredProducts = JsonConvert.DeserializeObject(filteredProducts);
                    var productDetails = filteredProducts.productDetails;
                    foreach (var PVM in productDetails)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = PVM.productId;
                        product.productName = PVM.productName;
                        product.productDescription = PVM.productDescription;
                        product.productAdTags = PVM.productAdTags;
                        product.productPrice = PVM.productPrice;
                        product.productQuantity = (int)PVM.productQuantity;
                        product.productCondition = PVM.productCondition != null ? (int)PVM.productCondition : 0;
                        product.productCategoryID = (int)PVM.productCategoryID;
                        product.productCategoryName = PVM.productCategoryName;
                        product.productsubCategoryID = (int)PVM.productsubCategoryID;
                        product.productSubCategoryName = PVM.productSubCategoryName;
                        product.productPriceCondition = PVM.productPriceCondition;
                        product.productAdCategory = PVM.productAdCategory;
                        product.productOwner = PVM.productOwner;
                        product.productOwnerName = PVM.productOwnerName;
                        product.userContactCity = PVM.userContactCity;
                        product.productMembershipID = PVM.productMembershipID != null ? (int)PVM.productMembershipID : 0; //(int)PVM.productMembershipID;
                        product.productMembershipName = PVM.productMembershipName;
                        product.productAdminReview = PVM.productAdminReview;
                        //product.byteProductImageData = PVM.ProductImageData;
                        product.IsActive = PVM.IsActive == null ? Convert.ToBoolean(0) :Convert.ToBoolean(PVM.IsActive);
                        product.createdBy = PVM.createdBy;
                        if (PVM.createdDate != null)
                            product.createdDate = (DateTime)PVM.createdDate;
                        if (PVM.updatedDate != null)
                            product.createdDate = (DateTime)PVM.updatedDate;
                        product.updatedBy = PVM.updatedBy;
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

        public string AddRating(int productId, int userId, int rating, string comments, string createdBy)
        {
            ProductHelper PH = new ProductHelper();
            string response = "Opeartion Failed";
            try
            {
                response = PH.AddRating(productId, userId, rating, comments, createdBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ProductViewModel> GetProductById(int productId)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetProductById(productId);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = productDetails.productId;
                        product.productName = productDetails.productName;
                        product.productCondition = productDetails.productCondition;
                        product.productAdCategory = productDetails.productAdCategory;
                        product.productCategoryID = productDetails.productCategoryID;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productDescription = productDetails.productDescription;
                        product.productAdTags = productDetails.productAdTags;
                        product.productSubCategoryName = productDetails.productSubCategoryName;
                        product.productsubCategoryID = productDetails.productsubCategoryID;
                        product.productQuantity = productDetails.productQuantity;
                        product.productPrice = productDetails.productPrice;
                        product.productAdminReviewStatus = productDetails.productAdminReviewStatus;
                        product.productOwner = productDetails.productOwner;
                        product.userContactCity = productDetails.userContactCity;
                        product.IsActiveStatus = productDetails.IsActiveStatus;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.productImageId = productDetails.productImageId;
                        product.productImageName = productDetails.productImageName;
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
        public List<ProductViewModel> GetTopRatedProducts(int count)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var productList = PH.GetTopRatedProducts(count);
                if (productList != null)
                {
                    foreach (var productDetails in productList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = productDetails.productId;
                        product.productName = productDetails.productName;
                        product.productDescription = productDetails.productDescription;
                        product.productAdTags = productDetails.productAdTags;
                        product.productPrice = productDetails.productPrice;
                        product.productQuantity = productDetails.productQuantity;
                        product.productCondition = productDetails.productCondition;
                        product.productCategoryID = productDetails.productCategoryID;
                        product.productCategoryName = productDetails.productCategoryName;
                        product.productsubCategoryID = productDetails.productsubCategoryID;
                        product.productSubCategoryName = productDetails.productSubCategoryName;
                        product.productPriceCondition = productDetails.productPriceCondition;
                        product.productAdCategory = productDetails.productAdCategory;
                        product.productOwner = productDetails.productOwner;
                        product.userContactCity = productDetails.userContactCity;
                        product.productOwnerName = productDetails.productOwnerName;
                        product.productMembershipID = productDetails.productMembershipID;
                        product.productMembershipName = productDetails.productMembershipName;
                        product.productAdminReview = productDetails.productAdminReview;
                        product.byteProductImageData = productDetails.byteProductImageData;
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