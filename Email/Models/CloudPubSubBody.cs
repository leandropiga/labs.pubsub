using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Models
{
    public class CloudPubSubBody
    {
        public int DeliveryAttempt { get; set; }
        public CloudPubSubMessage Message { get; set; }
        public string Subscription { get; set; }
    }
}
