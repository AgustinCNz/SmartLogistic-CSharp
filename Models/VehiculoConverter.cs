using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

public class VehiculoConverter : JsonConverter<Vehiculo>
{
    public override Vehiculo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("Tipo", out JsonElement tipoElement))
            throw new JsonException("No se encuentra la propiedad 'Tipo'");

        string tipo = tipoElement.GetString() ?? throw new JsonException("Tipo nulo");

        return tipo switch
{
    "Camión" => JsonSerializer.Deserialize<Camion>(root.GetRawText(), options)!,
    "Furgoneta" => JsonSerializer.Deserialize<Furgoneta>(root.GetRawText(), options)!,
    "Moto" => JsonSerializer.Deserialize<Moto>(root.GetRawText(), options)!,
    _ => throw new JsonException($"Tipo de vehículo desconocido: {tipo}")
};

    }

    public override void Write(Utf8JsonWriter writer, Vehiculo value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
