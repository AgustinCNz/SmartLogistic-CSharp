public interface IVehiculo
{
string Id { get; }
string Tipo { get; }
double CapacidadKg { get; }
string Estado { get; set; }
bool EstaDisponible();
}