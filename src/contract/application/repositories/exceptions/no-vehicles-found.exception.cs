namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class NoVehiclesFoundException : Exception
    {
        public NoVehiclesFoundException() : base("No vehicles found")
        {
        }
    }
}
