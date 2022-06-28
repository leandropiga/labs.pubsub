using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Calculation.Models
{
    public class CloudPubSubMessage
    {
        public CloudPubSubAttributes? Attributes { get; set; }
        public string Data { get; set; }
        public string MessageId { get; set; }
        public DateTime PublishTime { get; set; }

        public T ConvertDataToObject<T>()
        {
            if (string.IsNullOrWhiteSpace(Data))
                return default;

            var bytes = Convert.FromBase64String(Data);
            var text = Encoding.UTF8.GetString(bytes);
            var response = JsonSerializer.Deserialize<T>(text);
            return response;
        }
    }
}
