using SegurOsCar.Models;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SegurOsCar.Utilities
{
    public class VehicleJsonDeserializer : JsonConverter<Vehicle>
    {
     
        public override Vehicle? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = document.RootElement;
                
                var type = jsonObject.TryGetProperty("Type", out var typeProperty) 
                    ? jsonObject.GetProperty("Type").GetString() : null;
                return type switch
                {
                    "Car" => JsonSerializer.Deserialize<Car>(jsonObject.GetRawText(), options),
                    null => throw new JsonException("Missing 'Type' property in JSON."),
                    _ => throw new NotSupportedException($"Tipo de vehiculo no soportado: {type}")
                };
            }
            
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.GetType().Name);
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
            writer.WriteEndObject();
        }
    }
}
