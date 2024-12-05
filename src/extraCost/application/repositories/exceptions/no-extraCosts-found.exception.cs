namespace OrdersMicroservice.src.extracost.application.repositories.exceptions
{
    public class NoExtraCostsFoundException : Exception
    {
        public NoExtraCostsFoundException() : base("No extra costs found") { }
    }
}
