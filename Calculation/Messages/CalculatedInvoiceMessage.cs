namespace Calculation.Messages
{
    public class CalculatedInvoiceMessage
    {

        public string InvoiceId {get; set;} = string.Empty;
        public string CustomerIdentification {get; set;} = string.Empty;
        public string CustomerName {get; set;} = string.Empty;
        public DateTime IssuanceDate {get; set;} = DateTime.Now;
        public double Total {get; set;}    
        public double Tax {get; set;}    
    }
}