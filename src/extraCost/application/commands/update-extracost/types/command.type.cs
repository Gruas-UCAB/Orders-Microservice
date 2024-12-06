namespace OrdersMicroservice.src.extracost.application.commands.update_extracos.types
{
    public class UpdateExtraCostByIdCommand(string Id, string? Description, decimal Price)
    {
        public string Id = Id;
        public string? Description = Description ;
        public decimal Price = Price;
        
    }
}
