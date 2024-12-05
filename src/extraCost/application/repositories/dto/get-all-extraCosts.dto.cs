namespace OrdersMicroservice.src.extracost.application.repositories.dto
{
    public record GetAllExtraCostsDto
    (
        int limit = 10,
        int offset = 1,
        bool active = true
    );
}
