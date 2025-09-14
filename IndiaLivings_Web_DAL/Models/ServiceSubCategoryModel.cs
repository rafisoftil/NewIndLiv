using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ServiceSubCategoryModel
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public decimal? BasePrice { get; set; }
        public string? ProviderName { get; set; }
        public int? DurationMin { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
