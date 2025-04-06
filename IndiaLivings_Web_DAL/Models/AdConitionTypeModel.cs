using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class AdConitionTypeModel
    {
        public string AdConditionTypeName { get; set; }
        public List<AdConditionModel> AdConditionType { get; set; }
    }
}
