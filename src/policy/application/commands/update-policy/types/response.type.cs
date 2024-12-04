namespace OrdersMicroservice.src.policy.application.commands.update_policy.types
{
    public class UpdatePolicyByIdResponse(string id)
    {
        public readonly string Id = id;
    }
}
