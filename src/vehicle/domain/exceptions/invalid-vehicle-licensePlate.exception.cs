namespace OrdersMicroservice.src.vehicle.domain.exceptions
{
    public class InvalidLicensePlateException : System.Exception
    {
        public InvalidLicensePlateException()
            : base("The license plate provided is invalid.")
        {
        }
    }
}
