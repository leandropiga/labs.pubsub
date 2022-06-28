using System.Text;
using System.Text.Json;
using Database.Messages;
using Email.Models;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace Email.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly StorageClient _client;

    public EmailController(ILogger<EmailController> logger)
    {
        _logger = logger;
        _client = StorageClient.Create();
    }

    [HttpPost(Name = "SendInvoice")]
    public IActionResult Post(CloudPubSubBody body)
    {
        var message = JsonSerializer.Deserialize<CalculatedInvoiceMessage>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(body.Message.Data)));
        // Upload to a bucket to simulate a e-mail
        _client.UploadObject("labs-pubsub", $"invoice-email/email-{message.InvoiceId}", "text/plain", new MemoryStream(Convert.FromBase64String(body.Message.Data)));
        return new OkResult();
    }
}
