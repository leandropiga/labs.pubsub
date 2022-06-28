namespace Api.Messages
{
    public class InvoiceMessage
    {

        public string InvoiceId {get; set;} = string.Empty;
        public string CustomerIdentification {get; set;} = string.Empty;
        public string CustomerName {get; set;} = string.Empty;
        public DateTime IssuanceDate {get; set;} = DateTime.Now;
        public double Total {get; set;}        
    }
}