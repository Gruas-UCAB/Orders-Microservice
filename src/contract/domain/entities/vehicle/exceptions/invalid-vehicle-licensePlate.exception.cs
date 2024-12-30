namespace OrdersMicroservice.src.contract.domain.entities.vehicle.exceptions
{
    public class InvalidLicensePlateException : Exception
    {
        public InvalidLicensePlateException()
            : base("The license plate provided is invalid.")
        {
        }
    }
}
