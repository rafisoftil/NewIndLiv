using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;

namespace RazorpayIntegration.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string _key;
        private readonly string _secret;

        public PaymentController(IConfiguration configuration)
        {
            _key = configuration["Razorpay:Key"];
            _secret = configuration["Razorpay:Secret"];
        }

        public IActionResult Index()
        {
            return View();
        }

        // Create Order API
        [HttpPost]
        public IActionResult CreateOrder(int amount)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);

                Dictionary<string, object> options = new Dictionary<string, object>
                {
                    { "amount", amount * 100 },   // amount in paise
                    { "currency", "INR" },
                    { "payment_capture", 1 }
                };

                Order order = client.Order.Create(options);

                var orderId = order["id"].ToString();

                return Json(new { orderId = orderId, key = _key, amount = amount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Payment Success callback
        [HttpPost]
        public IActionResult PaymentCallback(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            try
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "razorpay_payment_id", razorpay_payment_id },
                    { "razorpay_order_id", razorpay_order_id },
                    { "razorpay_signature", razorpay_signature }
                };

                //Utils.verifyPaymentSignature(attributes, _secret);

                return Ok("Payment Successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Payment Verification Failed", error = ex.Message });
            }
        }
    }
}
