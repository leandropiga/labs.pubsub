using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Calculation.Models
{
    public class CloudPubSubAttributes : Dictionary<string, string>
    {
        public static CloudPubSubAttributes Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new CloudPubSubAttributes();
            else
                return JsonSerializer.Deserialize<CloudPubSubAttributes>(value);
        }
    }
}
