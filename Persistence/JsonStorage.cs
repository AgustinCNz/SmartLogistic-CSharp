using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class JsonStorage
{
    private static string vehiculoPath = Path.Combine("Persistence", "vehiculos.json");
    private static string envioPath = Path.Combine("Persistence", "envios.json");

    private static JsonSerializerOptions opcionesVehiculos = new JsonSerializerOptions
    {
        Converters = { new VehiculoConverter() },
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static List<Vehiculo> CargarVehiculos()
    {
        Console.WriteLine("Leyendo archivo vehiculos.json...");
        if (!File.Exists(vehiculoPath)) return new List<Vehiculo>();

        string json = File.ReadAllText(vehiculoPath);
        return JsonSerializer.Deserialize<List<Vehiculo>>(json, opcionesVehiculos)
               ?? new List<Vehiculo>();
    }

    public static List<Envio> CargarEnvios()
    {
        if (!File.Exists(envioPath)) return new List<Envio>();

        string json = File.ReadAllText(envioPath);
        return JsonSerializer.Deserialize<List<Envio>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<Envio>();
    }

    public static void GuardarVehiculos(List<Vehiculo> vehiculos)
    {
        string json = JsonSerializer.Serialize(vehiculos, opcionesVehiculos);
        File.WriteAllText(vehiculoPath, json);
    }

    public static void GuardarEnvios(List<Envio> envios)
    {
        string json = JsonSerializer.Serialize(envios, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        });
        File.WriteAllText(envioPath, json);
    }
}
