using System;
using System.Collections.Generic;
using System.Linq;


public class ReporteVehiculosPorEstado : IReporte
{
    private readonly List<Vehiculo> _vehiculos;
    public string Nombre => "Vehículos por Estado";

    public ReporteVehiculosPorEstado(List<Vehiculo> vehiculos)
    {
        _vehiculos = vehiculos;
    }

    public void Mostrar()
    {
        Console.WriteLine("\n--- Vehículos por Estado ---");

        var agrupados = _vehiculos.GroupBy(v => v.Estado)
            .OrderBy(g => g.Key);

        foreach (var grupo in agrupados)
        {
            Console.WriteLine($"Estado: {grupo.Key}, Cantidad: {grupo.Count()}");
            foreach (var vehiculo in grupo)
            {
                Console.WriteLine($"  ID: {vehiculo.Id}, Tipo: {vehiculo.Tipo}, CapacidadKg: {vehiculo.CapacidadKg}");
            }
            Console.WriteLine();
        }
    }
}