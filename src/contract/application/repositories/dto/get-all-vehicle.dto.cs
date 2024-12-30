namespace OrdersMicroservice.src.contract.application.repositories.dto
{
    public record GetAllVehiclesDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
