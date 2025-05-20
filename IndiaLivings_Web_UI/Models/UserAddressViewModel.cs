using System.Net;
using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class UserAddressViewModel
    {
        
        //public int intUserID { get; set; }
        //public string strUserContactFullAddress { get; set; }
        //public string strUserContactCity { get; set; }
        //public string strUserContactState { get; set; }
        //public string strUserContactCountry { get; set; }
        //public string strUserContactPinCode { get; set; }
        //public int intUserAddressType { get; set; }
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
        public string UserShippingPinCode { get; set; }

        public string UpdateAddress(int intUserID, string strUserContactFullAddress, string strUserContactCity, string strUserContactState, string strUserContactCountry, string strUserContactPinCode, int intUserAddressType)
        {
            AuthenticationHelper PH = new AuthenticationHelper();
            var response = "";
            try
            {
                response = PH.UpdateAddress(intUserID, strUserContactFullAddress, strUserContactCity, strUserContactState, strUserContactCountry, strUserContactPinCode, intUserAddressType);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }



    }
}
