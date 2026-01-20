using System.Text.Json.Serialization;

namespace TdMarzPay.Shared;

public class TimeLine
{
  
        public string created_at { get; set; }
        public string updated_at { get; set; }
        [JsonPropertyName("last_login_at")]
        public string? last_login_at { get; set; }
}