namespace Api.Models.Responses
{
    public class InvoicePostResponse
    {
        public InvoicePostResponse(string invoiceId) 
        {
            InvoiceId = invoiceId;
        }
        public string InvoiceId { get; } = string.Empty;
    }
}