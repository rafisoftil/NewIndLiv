using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class BlogModel
    {
        public int blogId { get; set; }
        public string title { get; set; } = string.Empty;
        public string content {  get; set; } = string.Empty;
        public string summary {  get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public byte[] featuredImage { get; set; } = [];
        public string tags { get; set; } = string.Empty;
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public int viewCount { get; set; }
        public bool isFeatured { get; set; }
        public bool isPublished { get; set; }
        public DateTime publishedDate { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; }
        public string updatedBy { get; set; } = string.Empty;
    }
    public class BlogCategoriesModel
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int blogCount { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; } = string.Empty;
        public string updatedBy { get; set; } = string.Empty;
    }
}
