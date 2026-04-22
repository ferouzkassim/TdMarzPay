using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TdMarzPay.Models.JsonConvertors;

    public class ConvertFromMoney : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        { 
            if (reader.TokenType == JsonTokenType.String &&
                decimal.TryParse(reader.GetString(), out var result))
            {
                return result;
            }
  
            return reader.TokenType == JsonTokenType.Number ? reader.GetDecimal() : 0;
            // throw new JsonException("Invalid amount format");


        }
        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        { 
            writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
