public class Furgoneta : Vehiculo, IRefrigerado
{
public bool TieneRefrigeracion { get; set; }
public Furgoneta()
{
Tipo = "Furgoneta";
}
public override bool PuedeAsignarseA(Envio envio)
{
if (!EstaDisponible()) return false;
if (envio.PesoKg > CapacidadKg) return false;

if (envio.RequiereRefrigeracion && !TieneRefrigeracion) return
false;
return true;
}
}