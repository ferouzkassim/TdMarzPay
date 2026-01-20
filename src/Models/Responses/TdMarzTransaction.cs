using System.Text.Json.Serialization;

namespace TdMarzPay.Models.Responses;

public class TdMarzTransaction
{
        [JsonPropertyName("event_type")]
        public MarzPayEvents EventStatus { get; set; }

        
        [JsonPropertyName("transaction")]
        public MarzTransaction Transaction { get; set; }

       
        [JsonPropertyName("collection")]
        public Collection Collection { get; set; }

      
        [JsonPropertyName("business")]
        public object? Business { get; set; }

      
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
}