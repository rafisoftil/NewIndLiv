using IndiaLivings_Web_DAL;
using System.Data;

namespace IndiaLivingsAPI.Model.ErrorLogs
{
    public class ErrorLog
    {
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
    public static class clsErrorLog
    {
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
    }
}
