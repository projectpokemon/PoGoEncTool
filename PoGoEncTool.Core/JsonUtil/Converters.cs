using System;
using Newtonsoft.Json;

namespace PoGoEncTool.WinForms
{
    public class FlatConverter<T> : JsonConverter
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new Exception();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteRawValue(JsonConvert.SerializeObject(value, Settings));
        }
    }
}
