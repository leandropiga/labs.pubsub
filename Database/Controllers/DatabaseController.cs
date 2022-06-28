using System.Text.Json;
using Database.Messages;
using Database.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ILogger<DatabaseController> _logger;
    private readonly CollectionReference _collection;

    public DatabaseController(ILogger<DatabaseController> logger)
    {
        _logger = logger;
        var db = FirestoreDb.Create("lp-labs-pubsub");
        // Create a document with a random ID in the "users" collection.
        _collection = db.Collection("invoice");
    }

    [HttpPost(Name = "PostDatabase")]
    public async Task<IActionResult> Post(CloudPubSubBody body)
    {
        var message = JsonSerializer.Deserialize<CalculatedInvoiceMessage>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(body.Message.Data)));
        var invoice = new Invoice 
        {
                InvoiceId = message.InvoiceId,
                CustomerIdentification = message.CustomerIdentification,
                CustomerName = message.CustomerName,
                IssuanceDate = message.IssuanceDate,
                Total = message.Total,
                Tax = message.Tax
        };
        
        DocumentReference document = await _collection.AddAsync(invoice);
        return new OkResult();
    }
}
