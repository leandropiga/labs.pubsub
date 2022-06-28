using Google.Cloud.Firestore;

namespace Database.Messages
{
    [FirestoreData]
    public class Invoice
    {
        [FirestoreDocumentId]
        public string Id {get; set;} = string.Empty;
        [FirestoreProperty]
        public string InvoiceId {get; set;} = string.Empty;
        [FirestoreProperty]
        public string CustomerIdentification {get; set;} = string.Empty;
        [FirestoreProperty]
        public string CustomerName {get; set;} = string.Empty;
        public DateTime IssuanceDate {get; set;} = DateTime.Now;
        [FirestoreProperty]
        public double Total {get; set;}    
        [FirestoreProperty]
        public double Tax {get; set;}    
    }
}