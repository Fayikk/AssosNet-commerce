using System.IO;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private const string WhSecret = "";

        private readonly ILogger<IPaymentService> _logger;
        public PaymentsController(IPaymentService paymentService,ILogger<IPaymentService> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{basketId}")]

        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId){
            return await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook(){
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json,Request.Headers["Stripe-Signature"],
            WhSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeded":
                    intent = (PaymentIntent) stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeded:",intent.Id);
                     order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received",order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed",intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed",order.Id);
                    break;    
            }

            return new EmptyResult();
        }
    }
}