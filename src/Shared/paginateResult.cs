using System.Text.Json.Serialization;

namespace TdMarzPay.Shared;

public class PaginatedResult
{
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }
    [JsonPropertyName("last_page")]
    public int LastPage { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    [JsonPropertyName("total")]
    public int TotalItems { get; set; }
}