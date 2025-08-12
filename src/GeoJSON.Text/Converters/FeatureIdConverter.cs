// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Text.Feature;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Text.Converters
{
    internal class FeatureIdConverter : JsonConverter<FeatureId>
    {
        public override FeatureId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => reader.TryGetUInt64(out var value) ? value : throw new JsonException(),
            _ => throw new JsonException(),
        };

        public override void Write(Utf8JsonWriter writer, FeatureId value, JsonSerializerOptions options)
        {
            if (value.IsNumeric) writer.WriteNumberValue(value);
            else                 writer.WriteStringValue(value);
        }
    }
}