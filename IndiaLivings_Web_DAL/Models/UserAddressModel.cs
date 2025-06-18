using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class UserAddressModel
    {
        public string UserContactFullAddress { get; set; }
        public string UserContactCity { get; set; }
        public string UserContactState { get; set; }
        public string UserContactCountry { get; set; }
        public string UserContactPinCode { get; set; }
        public string UserBillingFullAddress { get; set; }
        public string UserBillingCity { get; set; }
        public string UserBillingState { get; set; }
        public string UserBillingCountry { get; set; }
        public string UserBillingPinCode { get; set; }
        public string UserShippingFullAddress { get; set; }
        public string UserShippingType { get; set; }
        public string UserShippingCity { get; set; }
        public string UserShippingState { get; set; }
        public string UserShippingCountry { get; set; }
        public string UserShippingPinCode { get; set;}
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
