using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Invoices/addInvoice", IM).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return invoiceId;
        }
    }
}
