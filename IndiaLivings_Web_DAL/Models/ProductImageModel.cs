using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ProductImageModel
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
    }
}
