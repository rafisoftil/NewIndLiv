using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;


namespace IndiaLivings_Web_UI.Models
{
    public class PaymentRequestViewModel
    {
        public string PaymentId { get; set; }
        public string RazorPayKey { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }


        public PaymentRequestViewModel ProcessRequest(int requestedAmout, string ApiKey, string SecretKey,int? loggedInUser,string? invoiceType)
        {
            HttpContext context = null;
            PaymentRequestViewModel paymentRequest = new PaymentRequestViewModel();
            Random randomObj = new Random();
            InvoiceModel IVM = new InvoiceModel();
            PaymentHelper PH = new PaymentHelper();
            int isInsert = 0;
            try
            {
                string TransactionId = randomObj.Next(10000000, 100000000).ToString();
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(ApiKey, SecretKey);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", requestedAmout * 100);
                options.Add("receipt", TransactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", 0);
                Razorpay.Api.Order Response = client.Order.Create(options);
                string paymentId = Response["id"].ToString();
                paymentRequest.PaymentId = paymentId;
                paymentRequest.Amount = requestedAmout * 100;
                paymentRequest.RazorPayKey = ApiKey;
                paymentRequest.Currency = "INR";
                paymentRequest.Name = "";
                paymentRequest.Email = "";
                paymentRequest.Description = "";
                paymentRequest.Address = "";

                IVM.userID = loggedInUser.HasValue ? loggedInUser.Value : 0;
                IVM.invoiceNumber = paymentId;
                IVM.invoiceTotal = requestedAmout;
                IVM.InvoiceType = invoiceType.ToLower() == "membership" ? InvoiceTypes.MEMBERSHIP : InvoiceTypes.OTHERS;
                IVM.createdDate = DateTime.Now;
                IVM.dueDate = DateTime.Now.AddDays(365);
                IVM.Status = InvoiceStatus.PENDING;
                isInsert = PH.AddInvoice(IVM);


            }
            catch (Exception ex)
            {

            }

            return paymentRequest;
        }

        public int ProcessUpdateRequest(int requestedAmout, string orderId, string ApiKey, string SecretKey, int? loggedInUser, string? invoiceType)
        {
            HttpContext context = null;
            PaymentRequestViewModel paymentRequest = new PaymentRequestViewModel();
            Random randomObj = new Random();
            InvoiceModel IVM = new InvoiceModel();
            PaymentHelper PH = new PaymentHelper();
            int isInsert = 0;
            try
            {
                IVM.userID = loggedInUser.HasValue ? loggedInUser.Value : 0;
                IVM.invoiceNumber = orderId;
                IVM.invoiceTotal = requestedAmout/100;
                IVM.InvoiceType = invoiceType.ToLower() == "membership" ? InvoiceTypes.MEMBERSHIP : InvoiceTypes.OTHERS;
                IVM.createdDate = DateTime.Now;
                IVM.dueDate = DateTime.Now.AddDays(365);
                IVM.Status = InvoiceStatus.SUCCESS;
                isInsert = PH.UpdateInvoice(IVM);


            }
            catch (Exception ex)
            {

            }

            return isInsert;
        }

        public string CompleteRequest(string rzpId,string rzpRqstId,string ApiKey, string SecurityKey)
        {
            try
            {
                //string paymentId = _httpContextAccessor.HttpContext.Request.Form["rzp_paymentid"];
                // This is orderId
                //string orderId = _httpContextAccessor.HttpContext.Request.Form["rzp_orderid"];
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(ApiKey, SecurityKey);
                Razorpay.Api.Payment payment = client.Payment.Fetch(rzpId);
                // This code is for capture the payment
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", payment.Attributes["amount"]);
                Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
                string amt = paymentCaptured.Attributes["amount"];
                return paymentCaptured.Attributes["status"];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
