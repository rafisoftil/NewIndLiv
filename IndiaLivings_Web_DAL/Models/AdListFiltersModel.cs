using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class AdListFiltersModel
    {
        public List<ProductModel> Products { get; set; }
        public List<SearchFilterDetailsModel> Filters { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
