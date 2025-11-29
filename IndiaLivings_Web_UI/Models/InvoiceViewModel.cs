
using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{

    public class InvoiceViewModel
    {
        public int intInvoiceID { get; set; } = 0;
        public int userID { get; set; } = 0;
        public int ProductID { get; set; } = 0;
        public InvoiceTypes InvoiceType { get; set; }
        public string? invoiceNumber { get; set; } = string.Empty;
        public decimal invoiceTotal { get; set; }
        public decimal taxAmount { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.PENDING;
        public decimal discountAmount { get; set; }
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public DateTime dueDate { get; set; } = DateTime.MinValue;
        public string description { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
        public string invoiceItemName { get; set; } = string.Empty;
        public string invoiceItemDescription { get; set; } = string.Empty;

        public List<InvoiceViewModel> InvoiceListByUser(int userid)
        {
            List<InvoiceViewModel> invoiceViewModels = new List<InvoiceViewModel>();
            try
            {
                PaymentHelper PH = new PaymentHelper();
                List<InvoiceModel> invoiceModel = PH.GetInvoiceByUser(userid);
                foreach (var item in invoiceModel)
                {
                    InvoiceViewModel ivm = new InvoiceViewModel
                    {
                        intInvoiceID = item.intInvoiceID,
                        userID = item.userID,
                        ProductID = item.ProductID,
                        InvoiceType = item.InvoiceType,
                        invoiceNumber = item.invoiceNumber,
                        invoiceTotal = item.invoiceTotal,
                        taxAmount = item.taxAmount,
                        Status = item.Status,
                        discountAmount = item.discountAmount,
                        createdDate = item.createdDate,
                        dueDate = item.dueDate,
                        description = item.description,
                        updatedDate = item.updatedDate,
                        updatedBy = item.updatedBy,
                        invoiceItemName = item.invoiceItemName,
                        invoiceItemDescription = item.invoiceItemDescription
                    };
                    invoiceViewModels.Add(ivm);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return invoiceViewModels;
        }
    }
}
