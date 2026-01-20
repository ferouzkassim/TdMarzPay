using System.Text.Json.Serialization;

namespace TdMarzPay.Shared
{
    public class MarzPayResponse<T>
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("data")]
        public T? Data { get; set; }
        [JsonPropertyName("error_code")]
        public string? ErrorCode { get; set; }
        [JsonPropertyName("errors")]
        public ICollection<string> Errors { get; set; } = [];
    }
}
