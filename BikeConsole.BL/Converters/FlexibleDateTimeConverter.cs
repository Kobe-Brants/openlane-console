using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BikeConsole.BL.Converters;

public class FlexibleDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly string[] DateTimeFormats = {
        "yyyy-MM-dd HH:mm:ss.fffffff",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss.fffZ",
        "yyyy-MM-ddTHH:mm:ssZ",
        "yyyy-MM-dd"
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateStr = reader.GetString();
        
        foreach (var format in DateTimeFormats)
        {
            if (DateTime.TryParseExact(dateStr, 
                    format, 
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, 
                    out DateTime result))
            {
                return result;
            }
        }
        
        if (DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedResult))
        {
            return parsedResult;
        }

        throw new JsonException($"Unable to parse datetime: {dateStr}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
    }
}
