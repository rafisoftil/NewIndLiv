using IndiaLivingsAPI.Model.ErrorLogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLoggingController : ControllerBase
    {
        [HttpPost("InsertErrorLog")]
        public ActionResult insertErrorLog(string Message, string StackTrace, string Source)
        {
         
            bool blnUserFlag = false;
            string strStatus = "Add Category Failed. Please check with Admin.";
            try
            {
                blnUserFlag = clsErrorLog.insertErrorLog(Message, StackTrace, Source);
                if (blnUserFlag == true)
                {
                    strStatus = "Log Added.";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(strStatus);
        }
    }
}
