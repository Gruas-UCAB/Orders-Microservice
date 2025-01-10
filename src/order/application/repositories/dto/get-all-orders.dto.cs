namespace OrdersMicroservice.src.order.application.repositories.dto
{
    public record GetAllOrdersDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
