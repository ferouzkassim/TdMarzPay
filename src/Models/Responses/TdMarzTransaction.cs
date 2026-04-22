using System.Text.Json.Serialization;

namespace TdMarzPay.Models.Responses;

public class MarzTransaction
{
    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }
    [JsonPropertyName("amount")]
    public Amount? Amount { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("provider")]
    public string? Provider { get; set; }
    [JsonPropertyName("reference")]
    public string? MarzReference { get; set; }
    [JsonPropertyName("provider_reference")]
    public string? ProviderTransactionReference { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
}