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
        SERVICE,
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
        public InvoiceStatus Status { get; set; }// = InvoiceStatus.PENDING;
        public decimal discountAmount { get; set; }
        public DateTime createdDate { get; set; }// = DateTime.MinValue;
        public DateTime dueDate { get; set; }//= DateTime.MinValue;
         public string description { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; }//= DateTime.MinValue;
         public string updatedBy { get; set; } = string.Empty;
         public string invoiceItemName { get; set; } = string.Empty;
         public string invoiceItemDescription { get; set; } = string.Empty;
    }
    public class CreateCreditCardRequest
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; } = string.Empty;
        public int InvoiceId { get; set; }
        public string CCRequestJSON { get; set; } = string.Empty;
    }

    public class CreditCardResponse
    {
        public int ccId { get; set; }
        public int UserId { get; set; }
        public string MaskedCardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public int InvoiceId { get; set; }
        public string CCRequestJSON { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

    }
    public class CreditCardTransactionResponse
    {
        public int CreditCardId { get; set; }
        public int InvoiceId { get; set; }
        public string PaymentGatewayTransactionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ResponseCode { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string RawResponseJson { get; set; } = string.Empty;
    }

}
