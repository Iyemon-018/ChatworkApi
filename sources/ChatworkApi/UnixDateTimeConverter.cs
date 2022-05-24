namespace ChatworkApi;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class UnixDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new FormatException($"Since token type is not integer, it cannot be converted DateTime.({reader.TokenType})");

        var ticks = reader.GetInt64();

        return ticks == 0 ? default : ticks.FromUnixTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToUnixTime());
    }
}