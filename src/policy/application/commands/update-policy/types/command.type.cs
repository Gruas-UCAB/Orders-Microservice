namespace OrdersMicroservice.src.policy.application.commands.update_policy.types
{
    public class UpdatePolicyByIdCommand(string Id, string? Name, string? MonetaryCoverage, string? KmCoverage)
    {
        public string Id = Id;
        public string? Name = Name;
        public string? MonetaryCoverage = MonetaryCoverage;
        public string? KmCoverage = KmCoverage;
    }
}
