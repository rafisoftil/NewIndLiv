
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

public class PaymentService
{
    private RazorpayClient _client;

    public PaymentService(IConfiguration configuration)
    {
        var apiKey = configuration.GetValue<string>("RazorpayKey");
        var apiSecret = configuration.GetValue<string>("RazorpaySecret");

        _client = new RazorpayClient(apiKey, apiSecret);
    }

    public Order CreatePayment(int amount, string currency = "INR")
    {
        return CreatePayment(amount, _client, currency);
    }

    public Order CreatePayment(int amount, RazorpayClient _client, string currency = "INR")
    {
        var options = new Dictionary<string, object>
        {
            { "amount", amount * 100 }, // Razorpay expects the amount in the smallest currency unit
            { "currency", currency },
            { "receipt", "order_rcptid_11" },
            { "payment_capture", 1 }
        };

        var payment =  _client.Order.Create(options);
        return payment;
    }

}