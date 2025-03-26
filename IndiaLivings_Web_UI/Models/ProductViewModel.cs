using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

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
        public bool productSold { get; set; }
        public int productOwner { get; set; } = 0;
        public string productOwnerName { get; set; } = string.Empty;
        public int productMembershipID { get; set; } = 0;
        public string productMembershipName { get; set; } = string.Empty;
        public bool productAdminReview { get; set; }
        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
        public int Error_Id { get; set; } = 0;
        public string Error_Message { get; set; } = string.Empty;



        public List<ProductViewModel> GetAllWishlist(int userid)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductHelper PH = new ProductHelper();
            try
            {
                var wishList = PH.GetProductsbyOwner(userid );
                if (wishList != null)
                {
                    foreach (var wishDetails in wishList)
                    {
                        ProductViewModel product = new ProductViewModel();
                        product.productId = wishDetails.productId;
                        product.productName = wishDetails.productName;
                        product.productImageName = wishDetails.productImageName;
                        product.productPrice= wishDetails.productPrice;
                        product.productDescription = wishDetails.productDescription;
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
                PVM.productImagePath = "";//  [];//productImage.OpenReadStream();
                                          //PVM. = productImage.FileName != "" ? productImage.FileName.Split(".")[1] : "";
                PVM.productSold = false;
                PVM.productOwner = product.productOwner;
                PVM.productOwnerName = product.productOwnerName;
                //PVM.productMembershipID = FormData[""];
                //PVM.productMembershipName = FormData[""];
                //PVM.productAdminReview = FormData[""];
                PVM.createdDate = product.createdDate;
                PVM.createdBy = product.createdBy;//HttpContext.Session.GetString("userName").ToString();
                PVM.updatedDate =product.updatedDate;
                PVM.updatedBy = product.updatedBy;
                isCreated = PH.InsertProduct(PVM);
            }
            catch (Exception ex) { 
            }
            return isCreated;
        }

    }
}
