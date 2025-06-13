using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class SearchFilterDetailsModel
    {
        public string CategoryType { get; set; }
        public string CategoryValue { get; set; }
        public int totalCount { get; set; }
    }
}
