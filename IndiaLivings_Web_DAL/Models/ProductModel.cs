using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ProductModel
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
        public string IsActiveStatus { get; set; } = string.Empty;
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
        public int Error_Id { get; set; } = 0;
        public string Error_Message { get; set; } = string.Empty;
        public string strProductImageName { get; set; } = string.Empty;
        public byte[] byteProductImageData { get; set; } = [];
        public string strProductImageType { get; set; } = string.Empty;
        public string productAdminReviewStatus { get; set; } = string.Empty;
        public int intproductCount { get; set; }

    }
}
