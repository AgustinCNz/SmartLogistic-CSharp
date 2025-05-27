public abstract class Vehiculo : IVehiculo, IAsignable
{
public string Id { get; set; } = Guid.NewGuid().ToString();
public string Tipo { get; protected set; } = "";
public double CapacidadKg { get; set; }
public string Estado { get; set; } = "Disponible";
public bool EstaDisponible() => Estado == "Disponible";
public abstract bool PuedeAsignarseA(Envio envio);
}