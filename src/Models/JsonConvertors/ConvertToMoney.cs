using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TdMarzPay.Models.JsonConvertors;

public class ConvertToMoney:JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            decimal.TryParse(reader.GetString(), out var result))
        {
            return result.ToString(CultureInfo.InvariantCulture);
        }

        return reader.TokenType == JsonTokenType.Number ? reader.GetDecimal().ToString(CultureInfo.InvariantCulture) : "0";
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}