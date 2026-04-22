using System.Text.Json.Serialization;
using TdMarzPay.Models.JsonConvertors;

namespace TdMarzPay.Models.Responses;

public class MarzRepsonse
{
    [JsonPropertyName("transaction")]
    public MarzTransaction Transaction { get; set; }
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; }
    [JsonPropertyName("event_type")]
    public string? EventType { get; set; }
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
    
   
}
    public class Amount
    {
        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }
        [JsonPropertyName("raw")]
        [JsonConverter(typeof(ConvertToMoney))]
        public string Raw { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Collection
    {
        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }
        [JsonPropertyName("provider")]
        public string Provider { get; set; }
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("mode")]
        public string Mode { get; set; }
        [JsonPropertyName("provider_transaction_id")]
        public string? ProviderReference { get; set; }
    }
   
   public class MarzPayCallBack
   {
       [JsonPropertyName("transaction")]
      public MarzTransaction? Transaction { get; set; }
      [JsonPropertyName("amount")]
      public Amount? Amount { get; set; }
      [JsonPropertyName("event_type")]
      public string? EventType { get; set; }
      [JsonPropertyName("provider")]
      public string? LocalProvider { get; set; }
      [JsonPropertyName("phone_number")]
      public string? PhoneNumber { get; set; }
      [JsonPropertyName("collection")]
      public Collection? Collection { get; set; }
      [JsonPropertyName("created_at")]
      public string? TransactionDate { get; set; }
      [JsonPropertyName("updated_at")]
       public string? LastUpdate { get; set; }
   }