using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class AdConditionModel
    {
        public int intAdConditionID { get; set; } = 0;
        public string strAdConditionName { get; set; } = string.Empty;
        public string strAdConditionType { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
    }
}
