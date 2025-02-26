using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ErrorLog
    {
        public int LogID { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public static bool insertErrorLog(string Message, string StackTrace, string Source)
        {
            const string SP_Name = "usp_InsertErrorLog";
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@logMessage", Message, ParameterDirection.Input);
                _objDM.AddParameter("@logStackTrace", StackTrace, ParameterDirection.Input);
                _objDM.AddParameter("@logSource", Source, ParameterDirection.Input);
                int result = Convert.ToInt32(_objDM.GetScalar(SP_Name));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static List<ErrorLog> viewErrorLogs()
        {
            const string SP_Name = "usp_GetErrorLog";
            DataSet ds = null;
            List<ErrorLog> lsErrorLog = new List<ErrorLog>();
            ErrorLog _errorlog = null;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _errorlog = new ErrorLog();
                        _errorlog.LogID = (int)ds.Tables[0].Rows[i]["logID"];
                        _errorlog.Message = ds.Tables[0].Rows[i]["logMessage"].ToString();
                        _errorlog.StackTrace = ds.Tables[0].Rows[i]["logStackTrace"].ToString();
                        _errorlog.Source = ds.Tables[0].Rows[i]["logSource"].ToString();
                        _errorlog.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["logTimestamp"].ToString());

                        lsErrorLog.Add(_errorlog);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsErrorLog;
        }
    }
}
