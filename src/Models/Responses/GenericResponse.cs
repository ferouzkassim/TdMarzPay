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

   public static GenericResponse<T> Failed(string error, string message)
   {
      var apiErro = JsonDocument.Parse(message);
      return apiErro.RootElement.TryGetProperty("message", out var errorCode) ?
         new GenericResponse<T> { Status = "failed", Message = error, ErrorCode = errorCode.GetString() } :
         new GenericResponse<T> { Status = "failed", Message = message, ErrorCode = "Not Found" };
   }
   public static GenericResponse<T> Success(T data)
   {
      return new GenericResponse<T> { Status = "success", Data = data };
   }

   public static GenericResponse<T> Failed(string error)
   {
      return new GenericResponse<T> { Status = "failed", Message = error };
   }
   public GenericResponse()
   {
      
   }
}

