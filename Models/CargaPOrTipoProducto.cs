
using System;
using System.Collections.Generic;
using System.Linq;
public class ReporteCargaPorTipoProducto : IReporte
{
    private readonly List<Envio> _envios;
    public string Nombre => "Carga transportada por Tipo de Producto";

    public ReporteCargaPorTipoProducto(List<Envio> envios)
    {
        _envios = envios;
    }

    public void Mostrar()
    {
        Console.WriteLine("\n--- Total de carga transportada por Tipo de Producto ---");

        var agrupados = _envios
            .GroupBy(e => e.Producto)
            .OrderBy(g => g.Key);

        foreach (var grupo in agrupados)
        {
            var totalPeso = grupo.Sum(e => e.PesoKg);
            Console.WriteLine($"Producto: {grupo.Key}, Total de carga: {totalPeso} kg");
        }
    }
}