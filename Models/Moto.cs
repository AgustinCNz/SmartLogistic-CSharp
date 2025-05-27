public class Moto : Vehiculo
{
public Moto()
{
Tipo = "Moto";
}
public override bool PuedeAsignarseA(Envio envio)
{
return EstaDisponible() && envio.PesoKg <= CapacidadKg &&
!envio.RequiereRefrigeracion;
}
}