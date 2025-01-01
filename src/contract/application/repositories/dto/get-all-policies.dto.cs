namespace OrdersMicroservice.src.contract.application.repositories.dto
{
    public record GetAllPolicesDto
    (
        int limit = 10,
        int offset = 1
    );
}
