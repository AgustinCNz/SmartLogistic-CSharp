using System;
using System.Collections.Generic;

public class ReporteService
{
    private readonly List<IReporte> _reportes;

    public ReporteService(List<Vehiculo> vehiculos, List<Envio> envios)
    {
        // Inyectamos reportes, podemos agregar más en el futuro sin modificar este código (Open/Closed)
        _reportes = new List<IReporte>
        {
            new ReporteEnviosPorEstado(envios),
            new ReporteVehiculosPorEstado(vehiculos),
            new ReporteCargaPorTipoProducto(envios)
        };
    }

    public void MostrarMenuReportes()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Menú de Reportes ---");
            for (int i = 0; i < _reportes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_reportes[i].Nombre}");
            }
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Seleccione un reporte: ");

            var opcion = Console.ReadLine();
            if (int.TryParse(opcion, out int num) && num >= 0 && num <= _reportes.Count)
            {
                if (num == 0) break;
                Console.Clear();
                _reportes[num - 1].Mostrar();
                Console.WriteLine("\nPresione una tecla para volver al menú de reportes...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Opción inválida. Intente nuevamente.");
                System.Threading.Thread.Sleep(1500);
            }
        }
    }
}
