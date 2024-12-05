namespace OrdersMicroservice.src.policy.application.commands.create_policy.types
{
    public record CreatePolicyCommand(
        string Name,
        decimal MonetaryCoverage,
        decimal KmCoverage
        
     );
}
