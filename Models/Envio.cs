public class Envio
{
public string Id { get; set; } = Guid.NewGuid().ToString();
public TipoProducto Producto { get; set; }
public double PesoKg { get; set; }
public string Destino { get; set; } = "";
public DateTime FechaEntrega { get; set; }
public string Estado { get; set; } = "Pendiente";
public bool RequiereRefrigeracion => Producto == TipoProducto.Alimento || Producto == TipoProducto.Medicamento;
public string? VehiculoAsignadoId { get; set; }
}