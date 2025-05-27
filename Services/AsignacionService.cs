public class AsignacionService
{

private readonly List<IVehiculo> _vehiculos;
private readonly List<Envio> _envios;
public AsignacionService(List<IVehiculo> vehiculos, List<Envio> envios)
{
_vehiculos = vehiculos;
_envios = envios;
}

public void AsignarVehiculosAPendientes()
{
    var pendientes = _envios
        .Where(e => 
            !string.IsNullOrWhiteSpace(e.Estado) &&
            e.Estado.Trim().Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
        .ToList();
        
    Console.WriteLine($"Envíos pendientes encontrados: {pendientes.Count}");

    if (pendientes.Count == 0)
    {
        Console.WriteLine("No hay envíos pendientes para asignar.");
        return;
    }

    // 🔹 Lista para registrar los envíos que no pudieron ser asignados
    List<Envio> enviosNoAsignados = new();

    foreach (var envio in pendientes)
    {
        Console.WriteLine($"[Debug Estado] Envío {envio.Id}: Estado='{envio.Estado}'");

        var vehiculo = _vehiculos
            .OfType<Vehiculo>() // Cambio importante
            .Where(v => v.PuedeAsignarseA(envio))
            .OrderBy(v => v.CapacidadKg)
            .FirstOrDefault();

        if (vehiculo is IVehiculo v)
        {
            envio.VehiculoAsignadoId = v.Id;
            envio.Estado = "En tránsito";
            v.Estado = "En ruta";
            Console.WriteLine($"Envío {envio.Id} asignado al vehículo {v.Id} ({v.Tipo})");
        }
        else
        {
            Console.WriteLine($"No se encontró vehículo disponible para el envío {envio.Id}");
            enviosNoAsignados.Add(envio); // Agrega a la lista
        }
    }

    // 🔹 Mostrar reporte de envíos no asignados al final
    if (enviosNoAsignados.Any())
    {
        Console.WriteLine("\nEnvíos no asignados:");
        foreach (var noAsignado in enviosNoAsignados)
        {
            Console.WriteLine($"- Envío ID: {noAsignado.Id}, Producto: {noAsignado.Producto}, Peso: {noAsignado.PesoKg} kg");
        }
    }
}


/*public void AsignarVehiculosAPendientes()
{
foreach (var envio in _envios.Where(e => e.Estado ==
"Pendiente"))
{
var vehiculo = _vehiculos
.OfType<IAsignable>()
.FirstOrDefault(v => v.PuedeAsignarseA(envio));
if (vehiculo is IVehiculo v)
{
envio.VehiculoAsignadoId = v.Id;
envio.Estado = "En tránsito";
v.Estado = "En ruta";
Console.WriteLine($"Envío {envio.Id} asignado al vehículo {v.Id} ({v.Tipo})");
}
}
}*/
}