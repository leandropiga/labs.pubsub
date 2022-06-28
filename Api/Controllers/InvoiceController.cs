using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Api.Messages;
using Api.Models.Requests;
using Api.Models.Responses;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{    
    private readonly ILogger<InvoiceController> _logger;
    private readonly PublisherClient _publisher;

    public InvoiceController(ILogger<InvoiceController> logger)
    {
        _logger = logger;
        var topicName = new TopicName("lp-labs-pubsub", "invoice-topic");
        _publisher = PublisherClient.Create(topicName);
    }

    [HttpPost(Name = "PostInvoice")]
    public async Task<IActionResult> Post(InvoicePostRequest invoice)
    {
        var message = new InvoiceMessage 
        {
            InvoiceId = Guid.NewGuid().ToString(),
            CustomerIdentification = invoice.CustomerIdentification,
            CustomerName = invoice.CustomerName,
            IssuanceDate = invoice.IssuanceDate,
            Total = invoice.Total
        };
        var value = JsonSerializer.Serialize(message);
        string messageId = await _publisher.PublishAsync(value);    
        //
        return new OkObjectResult(new InvoicePostResponse(message.InvoiceId));
    }
}
