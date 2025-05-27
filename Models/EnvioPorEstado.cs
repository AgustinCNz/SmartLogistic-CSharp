using System;
using System.Collections.Generic;
using System.Linq;

public class ReporteEnviosPorEstado : IReporte
{
    private readonly List<Envio> _envios;
    public string Nombre => "Envíos por Estado";

    public ReporteEnviosPorEstado(List<Envio> envios)
    {
        _envios = envios;
    }

    public void Mostrar()
    {
        Console.WriteLine("\n--- Envíos por Estado ---");

        var agrupados = _envios.GroupBy(e => e.Estado)
            .OrderBy(g => g.Key);

        foreach (var grupo in agrupados)
        {
            Console.WriteLine($"Estado: {grupo.Key}, Cantidad: {grupo.Count()}");
            foreach (var envio in grupo)
            {
                Console.WriteLine($"  ID: {envio.Id}, Producto: {envio.Producto}, PesoKg: {envio.PesoKg}, Destino: {envio.Destino}, FechaEntrega: {envio.FechaEntrega:dd/MM/yyyy}, VehículoAsignadoId: {envio.VehiculoAsignadoId ?? "Ninguno"}");
            }
            Console.WriteLine();
        }
    }
}