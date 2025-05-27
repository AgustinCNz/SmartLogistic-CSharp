

public class VehiculoService
{
    private string GenerarNuevoId()
{
    List<Vehiculo> vehiculos = JsonStorage.CargarVehiculos();
    int max = 0;

    foreach (var v in vehiculos)
    {
        if (!string.IsNullOrEmpty(v.Id) && v.Id.StartsWith("V"))
        {
            if (int.TryParse(v.Id.Substring(1), out int numero))
            {
                if (numero > max)
                    max = numero;
            }
        }
    }

    return $"V{(max + 1).ToString("D3")}";
}

    public Vehiculo? RegistrarVehiculo()
    {
        Console.WriteLine("Seleccione tipo de vehículo:");
        Console.WriteLine("1. Camión");
        Console.WriteLine("2. Furgoneta");
        Console.WriteLine("3. Moto");
        Console.Write("Opción: ");
        string? opcionVehiculo = Console.ReadLine();

        string tipoVehiculo = opcionVehiculo switch
        {
            "1" => "Camión",
            "2" => "Furgoneta",
            "3" => "Moto",
            _ => throw new ArgumentException("Opción no válida.")
        };

        Vehiculo nuevoVehiculo = VehiculoFactory.CrearVehiculo(tipoVehiculo);

          nuevoVehiculo.Id = GenerarNuevoId();

        Console.Write("Capacidad de carga (kg): ");
        if (double.TryParse(Console.ReadLine(), out double capacidad))
        {
            nuevoVehiculo.CapacidadKg = capacidad;
        }
        else
        {
            Console.WriteLine("Capacidad inválida.");
            return null;
        }

        Console.WriteLine("Estado inicial del vehículo:");
        Console.WriteLine("1. Disponible");
        Console.WriteLine("2. En ruta");
        Console.WriteLine("3. Mantenimiento");
        Console.Write("Opción: ");
        string estado = Console.ReadLine() switch
        {
            "1" => "Disponible",
            "2" => "En ruta",
            "3" => "Mantenimiento",
            _ => "Disponible"
        };
        nuevoVehiculo.Estado = estado;

        if (nuevoVehiculo is IRefrigerado refrig)
        {
            Console.Write("¿Tiene refrigeración? (s/n): ");
            string? resp = Console.ReadLine()?.ToLower();
            refrig.TieneRefrigeracion = string.Equals(resp, "s", StringComparison.OrdinalIgnoreCase);

        }

        return nuevoVehiculo;
    }
}

