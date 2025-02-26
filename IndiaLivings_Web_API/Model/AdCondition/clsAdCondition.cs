using IndiaLivingsAPI.Model.ErrorLogs;
using System.Data;
using IndiaLivings_Web_DAL;
namespace IndiaLivingsAPI.Model.AdConditions
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
    public class clsAdCondition
    {

        public bool insertAdCondition(string strAdConditionName, string strAdConditionType, string strCreatedBy)
        {
            int result = 0;
            const string SP_Name = "usp_insertAdCondition";
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionName", strAdConditionName, ParameterDirection.Input);
                _objDM.AddParameter("@AdConditionType", strAdConditionType, ParameterDirection.Input);
                //_objDM.AddParameter("@IsActive", true, ParameterDirection.Input);
                _objDM.AddParameter("@createdBy", strCreatedBy, ParameterDirection.Input);
                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool updateAdCondition(int intAdConditionID, string strAdConditionName, string strAdConditionType, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateAdCondition";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionID", intAdConditionID, ParameterDirection.Input);
                _objDM.AddParameter("@AdConditionName", strAdConditionName, ParameterDirection.Input);
                _objDM.AddParameter("@AdConditionType", strAdConditionType, ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", true, ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", strUpdatedBy, ParameterDirection.Input);
                _objDM.AddParameter("@flagDelete", 1, ParameterDirection.Input);

                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool deleteAdCondition(int intAdConditionID, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateAdCondition";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionID", intAdConditionID, ParameterDirection.Input);
                _objDM.AddParameter("@AdConditionName", "", ParameterDirection.Input);
                _objDM.AddParameter("@AdConditionType", "", ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", false, ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", strUpdatedBy, ParameterDirection.Input);
                _objDM.AddParameter("@flagDelete", 0, ParameterDirection.Input);

                 result = Convert.ToInt32(_objDM.GetScalar(SP_Name));
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public List<AdConditionModel> viewAllAdConditions(int intAdConditionID)
        {
            const string SP_Name = "usp_getAdConditions";
            DataSet ds = null;
            List<AdConditionModel> lsAdCondition = new List<AdConditionModel>();
            AdConditionModel _adCondition = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionID", intAdConditionID, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _adCondition = new AdConditionModel();
                        _adCondition.intAdConditionID = (int)ds.Tables[0].Rows[i]["AdConditionID"];
                        _adCondition.strAdConditionName = ds.Tables[0].Rows[i]["AdConditionName"].ToString();
                        _adCondition.strAdConditionType = ds.Tables[0].Rows[i]["AdConditionType"].ToString();
                        _adCondition.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _adCondition.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _adCondition.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _adCondition.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _adCondition.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();

                        lsAdCondition.Add(_adCondition);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lsAdCondition;
        }
        public List<clsAdConditionType> viewAllAdConditionByType(string strAdConditionTypeName)
        {
            const string SP_Name = "usp_getAdConditionByTypes";
            DataSet ds = null;
            List<clsAdConditionType> lsAdConditionType = new List<clsAdConditionType>();
            List<AdConditionModel> lsAdCondition = new List<AdConditionModel>();
            clsAdConditionType _adCondition = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionTypeName", strAdConditionTypeName, ParameterDirection.Input);
                _objDM.AddParameter("@flag", 0, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _adCondition = new clsAdConditionType();
                        if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["AdConditionType"].ToString()))
                        {
                            _adCondition.AdConditionTypeName = ds.Tables[0].Rows[i]["AdConditionType"].ToString();
                            lsAdCondition = viewAllAdConditionByTypeName(_adCondition.AdConditionTypeName.ToString());
                            _adCondition.strAdConditionType = lsAdCondition;
                        }

                        lsAdConditionType.Add(_adCondition);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsAdConditionType;
        }
        public List<AdConditionModel> viewAllAdConditionByTypeName(string strAdConditionTypeName)
        {
            const string SP_Name = "usp_getAdConditionByTypes";
            DataSet ds = null;
            List<AdConditionModel> lsAdCondition = new List<AdConditionModel>();
            AdConditionModel _adCondition = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@AdConditionTypeName", strAdConditionTypeName, ParameterDirection.Input);
                _objDM.AddParameter("@flag", 1, ParameterDirection.Input);

                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _adCondition = new AdConditionModel();
                        _adCondition.intAdConditionID = (int)ds.Tables[0].Rows[i]["AdConditionID"];
                        _adCondition.strAdConditionName = ds.Tables[0].Rows[i]["AdConditionName"].ToString();
                        _adCondition.strAdConditionType = ds.Tables[0].Rows[i]["AdConditionType"].ToString();
                        _adCondition.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _adCondition.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _adCondition.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _adCondition.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _adCondition.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();

                        lsAdCondition.Add(_adCondition);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsAdCondition;
        }
    }

    public class clsAdConditionType
    {
        public string AdConditionTypeName { get; set; }
        public List<AdConditionModel> strAdConditionType { get; set; }
    }
}
