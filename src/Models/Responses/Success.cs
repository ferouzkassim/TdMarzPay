using System.Text.Json.Serialization;

namespace TdMarzPay.Models.Responses;

public class SuccessResponse
{
    [JsonPropertyName("transaction")]
    public MarzTransaction Transaction { get; set; }
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; }
    [JsonPropertyName("timeline")]
    public Timeline Timeline { get; set; }
}
    public class Amount
    {
        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }

        [JsonPropertyName("raw")]
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
        [JsonPropertyName("provider_reference")]
        public string? ProviderReference { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("response_timestamp")]
        public string ResponseTimestamp { get; set; }

        [JsonPropertyName("sandbox_mode")]
        public bool SandboxMode { get; set; }

        [JsonPropertyName("test_phone_number")]
        public bool TestPhoneNumber { get; set; }
    }


    public class Timeline
    {
        [JsonPropertyName("initiated_at")]
        public string InitiatedAt { get; set; }

        [JsonPropertyName("estimated_settlement")]
        public string EstimatedSettlement { get; set; }
    }

    public class MarzTransaction
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("provider_reference")]
        public string ProviderReference { get; set; }
    }
