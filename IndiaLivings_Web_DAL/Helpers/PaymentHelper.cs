using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class PaymentHelper
    {
        public int AddInvoice(InvoiceModel IM)
        {
            string response = string.Empty;
            int invoiceId = 0;
            try
            {
                response = ServiceAPI.Post_Api("https://apis.indialivings.com/api/Invoices/addInvoice", IM).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return invoiceId;
        }

        public int UpdateInvoice(InvoiceModel IM)
        {
            string response = string.Empty;
            int invoiceId = 0;
            try
            {
                response = ServiceAPI.Post_Api("https://apis.indialivings.com/api/Invoices/updateInvoice", IM).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return invoiceId;
        }

        public List<InvoiceModel> GetInvoiceByUser(int userid)
        {
            List<InvoiceModel> IM = new List<InvoiceModel>();
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Get_async_Api("https://apis.indialivings.com/api/Invoices/GetInvoiceByUser?UserID=" + userid);
                IM = JsonConvert.DeserializeObject<List<InvoiceModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return IM;
        }
    }
}
