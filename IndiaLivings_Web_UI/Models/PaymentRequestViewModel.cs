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


        public PaymentRequestViewModel ProcessRequest(int requestedAmout, string ApiKey, string SecretKey)
        {
            HttpContext context = null;
            PaymentRequestViewModel paymentRequest = new PaymentRequestViewModel();
            Random randomObj = new Random();
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

            }
            catch (Exception ex)
            {

            }

            return paymentRequest;
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
