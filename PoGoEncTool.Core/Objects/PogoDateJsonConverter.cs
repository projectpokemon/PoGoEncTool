using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoGoEncTool.Core;

public sealed class PogoDateJsonConverter : JsonConverter<PogoDate>
{
    private const string Format = "yyyy.MM.dd";

    public override PogoDate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // New format: "2018.04.17"
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (value is not null && DateTime.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return new PogoDate(date.Year, date.Month, date.Day);

            throw new JsonException($"Invalid PogoDate string '{value}'. Expected format {Format}.");
        }

        // Legacy format: {"Y":2018,"M":4,"D":17}
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            int y = 0, m = 0, d = 0;
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;
                var name = reader.GetString();
                reader.Read();
                switch (name)
                {
                    case "Y": y = reader.GetInt32(); break;
                    case "M": m = reader.GetInt32(); break;
                    case "D": d = reader.GetInt32(); break;
                }
            }
            return new PogoDate(y, m, d);
        }
        throw new JsonException($"Unexpected JSON token {reader.TokenType} when reading PogoDate.");
    }

    public override void Write(Utf8JsonWriter writer, PogoDate value, JsonSerializerOptions options)
    {
        Span<char> buffer = stackalloc char[10];
        value.Year.TryFormat(buffer, out var yearWritten, "D4");
        buffer[yearWritten++] = '.';
        value.Month.TryFormat(buffer[yearWritten..], out var monthWritten, "D2");
        buffer[yearWritten + monthWritten++] = '.';
        value.Day.TryFormat(buffer[(yearWritten + monthWritten)..], out _, "D2");
        writer.WriteStringValue(buffer);
    }
}
