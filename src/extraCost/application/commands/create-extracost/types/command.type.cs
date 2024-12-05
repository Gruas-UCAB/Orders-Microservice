namespace OrdersMicroservice.src.extracost.application.commands.create_extracost.types
{
    public record CreateExtraCostCommand(
       
        string Description,
        decimal Price
        
     );
}
