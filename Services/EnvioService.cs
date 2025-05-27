public static class EnvioService
{
    private static readonly List<string> DestinosValidos = new()
    {
        "Mendoza", "Córdoba", "Rosario", "La Plata", "Bahía Blanca", "Tandil", "Mar del Plata"
    };

    private static string GenerarNuevoId(List<Envio> envios)
    {
        int maxNumero = 0;

        foreach (var envio in envios)
        {
            if (envio.Id != null && envio.Id.StartsWith("E"))
            {
                string numeroStr = envio.Id.Substring(1);
                if (int.TryParse(numeroStr, out int numero))
                {
                    if (numero > maxNumero)
                        maxNumero = numero;
                }
            }
        }

        return $"E{(maxNumero + 1).ToString("D3")}";
    }

    public static Envio? CrearEnvio(List<Envio> envios)
    {
        Console.WriteLine("\n--- Registro de Envío ---");

        // Producto
        Console.WriteLine("Seleccione el tipo de producto:");
        foreach (var tipo in Enum.GetValues(typeof(TipoProducto)))
        {
            Console.WriteLine($"{(int)tipo} - {tipo}");
        }

        if (!int.TryParse(Console.ReadLine(), out int tipoSeleccionado) || !Enum.IsDefined(typeof(TipoProducto), tipoSeleccionado))
        {
            Console.WriteLine("Producto inválido.");
            return null;
        }

        var producto = (TipoProducto)tipoSeleccionado;

        // Peso
        Console.Write("Ingrese el peso en kg: ");
        if (!double.TryParse(Console.ReadLine(), out double peso) || peso <= 0)
        {
            Console.WriteLine("Peso inválido.");
            return null;
        }

        // Destino
        Console.WriteLine("Seleccione el destino:");
        for (int i = 0; i < DestinosValidos.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {DestinosValidos[i]}");
        }

        if (!int.TryParse(Console.ReadLine(), out int destinoSeleccionado) || destinoSeleccionado < 1 || destinoSeleccionado > DestinosValidos.Count)
        {
            Console.WriteLine("Destino inválido.");
            return null;
        }

        string destino = DestinosValidos[destinoSeleccionado - 1];

        // Fecha estimada
        Console.Write("Ingrese la fecha estimada de entrega (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime fechaEntrega))
        {
            Console.WriteLine("Fecha inválida.");
            return null;
        }

        // ID y refrigeración
        string nuevoId = GenerarNuevoId(envios);
        bool requiereRefrigeracion = producto == TipoProducto.Alimento || producto == TipoProducto.Medicamento;

        Console.WriteLine($"\nResumen del envío:");
        Console.WriteLine($"Producto: {producto}");
        Console.WriteLine($"Peso: {peso} kg");
        Console.WriteLine($"Destino: {destino}");
        Console.WriteLine($"Fecha de entrega: {fechaEntrega:yyyy-MM-dd}");
        Console.WriteLine($"Requiere refrigeración: {(requiereRefrigeracion ? "Sí" : "No")}");

        return new Envio
        {
            Id = nuevoId,
            Producto = producto,
            PesoKg = peso,
            Destino = destino,
            FechaEntrega = fechaEntrega
        };
    }
}
