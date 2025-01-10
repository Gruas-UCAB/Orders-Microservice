namespace OrdersMicroservice.src.contract.application.repositories.exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() : base("Vehicle not found") { }
    }
}
