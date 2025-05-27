public static class VehiculoFactory
{
    public static Vehiculo CrearVehiculo(string tipo)
    {
        switch (tipo.ToLower())
        {
            case "camión":
                return new Camion();
            case "furgoneta":
                return new Furgoneta();
            case "moto":
                return new Moto();
            default:
                throw new ArgumentException("Tipo de vehículo no válido.");
        }
    }
}
