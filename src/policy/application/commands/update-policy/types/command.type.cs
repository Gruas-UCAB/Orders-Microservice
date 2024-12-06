namespace OrdersMicroservice.src.policy.application.commands.update_policy.types
{
    public class UpdatePolicyByIdCommand(string Id, string? Name, decimal MonetaryCoverage, decimal KmCoverage)
    {
        public string Id = Id;
        public string? Name = Name;
        public decimal MonetaryCoverage = MonetaryCoverage;
        public decimal KmCoverage = KmCoverage;
    }
}
