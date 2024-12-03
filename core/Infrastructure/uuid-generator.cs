using OrdersMicroservice.core.Application;

namespace OrdersMicroservice.core.Infrastructure
{
    public class UUIDGenerator : IIdGenerator<string>
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
