namespace OrdersMicroservice.src.vehicle.application.repositories.exceptions
{
    public class NoVehiclesFoundException : Exception
    {
        public NoVehiclesFoundException() : base("No vehicles found")
        {
        }
    }
}
