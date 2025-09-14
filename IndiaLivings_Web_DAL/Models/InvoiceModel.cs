using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public enum InvoiceStatus
    {
        SUCCESS,
        FAILED,
        ONHOLD,
        PENDING,
        CANCELLED,
    }
    public enum InvoiceTypes
    {
        PRODUCT,
        MEMBERSHIP,
        OTHERS,
    }
    public class InvoiceModel
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
    }
}
