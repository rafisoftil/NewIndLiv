namespace IndiaLivings_Web_UI.Models
{
    public class AdViewModel
    {
        #region Properties
        public int productId { get; set; } 
        public string productName { get; set; } 
        public string productDescription { get; set; } 
        public string productAdTags { get; set; }
        public decimal productPrice { get; set; }
        public int productQuantity { get; set; } 
        public int productCondition { get; set; } 
        public int productCategoryID { get; set; } 
        public string productCategoryName { get; set; } 
        public int productsubCategoryID { get; set; }
        public string productSubCategoryName { get; set; } 
        public string productPriceCondition { get; set; }
        public string productAdCategory { get; set; }
        public string strProductImageName { get; set; }        
        public byte[] byteProductImageData { get;set; }

        public string strProductImageType { get; set; }

        public bool productSold { get; set; }
        public int productOwner { get; set; } 
        public string productOwnerName { get; set; } 
        public int productMembershipID { get; set; } 
        public string productMembershipName { get; set; } 
        public bool productAdminReview { get; set; }
        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; } 
        public DateTime updatedDate { get; set; } 
        public string updatedBy { get; set; } 
        public int Error_Id { get; set; } 
        public string Error_Message { get; set; } 
        #endregion
    }
}
