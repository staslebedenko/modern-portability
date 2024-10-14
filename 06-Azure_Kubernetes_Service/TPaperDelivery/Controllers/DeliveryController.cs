using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TPaperDelivery
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController
    {
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(ILogger<DeliveryController> logger)
        {
            _logger = logger;
        }

        [Topic("pubsub-rabbitmq", "aksdelivery")]
        //[Topic("pubsub-super-new", "aksdelivery")]
        [HttpPost]
        public async Task<IActionResult> ProcessEdiOrder(Delivery delivery)
        {
            _logger.LogWarning("Triggered method");

            var newDelivery = new Delivery
            {
                Id = 0,
                ClientId = delivery.ClientId,
                EdiOrderId = delivery.EdiOrderId,
                Number = delivery.Number,
                ProductId = 1,
                ProductCode = 333,
                Notes = "Prepared for shipment"
            };

            _logger.LogWarning("Saved delivery");

            return new OkObjectResult("");
        }
        
        [HttpGet]
        [Route("health")]
        public async Task<IActionResult> Health(CancellationToken cts)
        {
            return new OkObjectResult("Started");
        }
    }
}
