using System.Text.Json;
using System.Text.Json.Serialization;
using TdMarzPay.Models.JsonConvertors;

namespace TdMarzPay.Models.Responses;

public class GenericResponse<T>
{
   [JsonPropertyName("status")]
   public string Status { get; set; }
   [JsonPropertyName("message")]
   public string Message { get; set; }
   [JsonPropertyName("data")]
   public T? Data { get; set;}
   [JsonPropertyName("error_code")]
   [JsonConverter(typeof(ErrorResponseConverter))]
   public string? ErrorCode { get; set; }
}

