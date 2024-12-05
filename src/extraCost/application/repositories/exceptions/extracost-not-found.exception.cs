namespace OrdersMicroservice.src.extracost.application.repositories.exceptions
{
    public class ExtraCostNotFoundException : Exception
    {
        public ExtraCostNotFoundException() : base("Extra cost not found") { }
    }
}
