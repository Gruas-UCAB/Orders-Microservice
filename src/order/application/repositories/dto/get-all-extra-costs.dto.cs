namespace OrdersMicroservice.src.order.application.repositories.dto
{
    public record GetAllExtraCostsDto
    (
        int limit = 10,
        int offset = 1
    );
}
