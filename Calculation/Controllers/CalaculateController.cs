using System.Text.Json;
using Calculation.Messages;
using Calculation.Models;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Mvc;

namespace Calculation.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculateController : ControllerBase
{    
    private readonly ILogger<CalculateController> _logger;
    private readonly PublisherClient _publisher;

    public CalculateController(ILogger<CalculateController> logger)
    {
        _logger = logger;
        var topicName = new TopicName("lp-labs-pubsub", "calculated-invoice-topic");
        _publisher = PublisherClient.Create(topicName);
    }

    [HttpPost(Name = "PostCalculate")]
    public async Task<IActionResult> Post(CloudPubSubBody body)
    {
        // Calculate tax
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };        
        var invoice = JsonSerializer.Deserialize<InvoiceMessage>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(body.Message.Data)), options);
        var calculated = new CalculatedInvoiceMessage 
        {
            InvoiceId = invoice.InvoiceId,
            CustomerIdentification = invoice.CustomerIdentification,
            CustomerName = invoice.CustomerName,
            IssuanceDate = invoice.IssuanceDate,
            Total = invoice.Total,
            Tax = invoice.Total * 18 / 100
        };

        var value = JsonSerializer.Serialize(calculated);
        string messageId = await _publisher.PublishAsync(value);    
        //
        return new OkResult();
    }
}

