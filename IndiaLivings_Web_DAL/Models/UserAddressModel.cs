using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class UserAddressModel
    {
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
    }
}
