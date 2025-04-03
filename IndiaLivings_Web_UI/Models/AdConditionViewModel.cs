using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json;

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
            ProductHelper product=new ProductHelper();
            // Ensure the parameter is not null or empty, default to empty string if necessary            
            try
            {
                var lst=product.GetAdConditions();
                foreach (var item in lst) {
                    foreach (var adCondition in item.strAdConditionType) { 
                        AdConditionViewModel adConditionViewModel = new AdConditionViewModel();
                        adConditionViewModel.strAdConditionType=adCondition.strAdConditionType;
                        adConditionViewModel.strAdConditionName = adCondition.strAdConditionName;
                        adConditionViewModel.intAdConditionID = adCondition.intAdConditionID;
                        adConditionViewModel.IsActive = adCondition.IsActive;
                        adConditionViewModel.createdDate = adCondition.createdDate;
                        adConditionViewModel.createdBy = adCondition.createdBy;
                        adConditionViewModel.updatedDate = adCondition.updatedDate;
                        adConditionViewModel.updatedBy = adCondition.updatedBy;
                        AdConditions.Add(adConditionViewModel);
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
