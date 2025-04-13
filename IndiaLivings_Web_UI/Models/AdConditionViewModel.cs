using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class AdConditionViewModel
    {
        #region
        public int intAdConditionID { get; set; }
        public string strAdConditionName { get; set; }
        public string strAdConditionType { get; set; }

        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime updatedDate { get; set; }
        public string updatedBy { get; set; }

        #endregion

        #region Methods
        public List<AdConditionViewModel> GetAllAdConditionsTypeName(string AdConditionType)
        {
            List<AdConditionViewModel> AdConditions = new List<AdConditionViewModel>();
            ProductHelper product = new ProductHelper();
            // Ensure the parameter is not null or empty, default to empty string if necessary
            if (string.IsNullOrEmpty(AdConditionType))
            {
                AdConditionType = "";
            }

            try
            {
                var lst = product.GetAdConditions();
                foreach (var item in lst)
                {
                    foreach (var condition in item.strAdConditionType)
                    {
                        AdConditionViewModel ACVM = new AdConditionViewModel();
                        ACVM.intAdConditionID = condition.intAdConditionID;
                        ACVM.strAdConditionName = condition.strAdConditionName;
                        ACVM.strAdConditionType = condition.strAdConditionType;
                        ACVM.IsActive = condition.IsActive;
                        AdConditions.Add(ACVM);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);

            }


            return AdConditions;
        }

        #endregion
    }
}
