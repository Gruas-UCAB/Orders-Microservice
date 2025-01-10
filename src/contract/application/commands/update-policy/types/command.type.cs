namespace OrdersMicroservice.src.contract.application.commands.update_contract.types
{
    public record UpdatePolicyByIdCommand
        (
            string Id,
            string? Name,
            decimal? MonetaryCoverage,
            decimal? KmCoverage,
            decimal? BaseKmPrice
        );
}
