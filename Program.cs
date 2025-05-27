using System;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        var vehiculos = JsonStorage.CargarVehiculos();
        var envios = JsonStorage.CargarEnvios();
      foreach (var envio in envios)
{
    Console.WriteLine($"[Debug] ID={envio.Id}, Producto={envio.Producto}, Estado='{envio.Estado}'");
}
        Console.WriteLine($"Envios cargados desde JSON: {envios.Count}");

       
        var asignador = new AsignacionService(vehiculos.Cast<IVehiculo>().ToList(), envios);

        while (true)
        {
            Console.WriteLine("\n--- Menú SmartLogistics ---");
            Console.WriteLine("1. Registrar vehículo");
            Console.WriteLine("2. Registrar envío");
            Console.WriteLine("3. Asignar envíos");
            Console.WriteLine("4. Ver reportes");
            Console.WriteLine("5. Salir");
            Console.Write("Opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                // Lógica de registro de vehículo
                
                    Vehiculo? nuevo = new VehiculoService().RegistrarVehiculo();
                   if (nuevo != null)
                    {
                        vehiculos.Add(nuevo);
                        JsonStorage.GuardarVehiculos(vehiculos); // 💾 GUARDA EN JSON
                        Console.WriteLine($"Vehículo registrado con ID: {nuevo.Id}");
                    }
                 break;    
                case "2":
                    // Lógica de registro de envío
                    var nuevoEnvio = EnvioService.CrearEnvio(envios);
                    if (nuevoEnvio != null)
                    {
                        envios.Add(nuevoEnvio);
                        JsonStorage.GuardarEnvios(envios);
                        Console.WriteLine($"✅ Envío registrado con ID: {nuevoEnvio.Id}");
                    }
                 break;
                case "3":
                asignador.AsignarVehiculosAPendientes();
                 Console.WriteLine("\nListado actualizado de envíos:");
                foreach (var envio in envios)  // asumamos que 'envios' es la lista original accesible
                    {
                          Console.WriteLine($"ID: {envio.Id}, Estado: {envio.Estado}, Vehículo Asignado: {envio.VehiculoAsignadoId ?? "Ninguno"}");
                     }
                Console.WriteLine("Presione una tecla para continuar...");
                 Console.ReadKey();
                 break;
                case "4":
                    // Mostrar reportes 
                    var reporteService = new ReporteService(vehiculos, envios);
                    reporteService.MostrarMenuReportes();
                    break;
   
                case "5":
                    JsonStorage.GuardarVehiculos(vehiculos);
                    JsonStorage.GuardarEnvios(envios);
                    return;
            }
        }
    }
}
