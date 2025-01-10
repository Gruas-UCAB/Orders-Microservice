namespace OrdersMicroservice.core.Application
{
    public interface IIdGenerator<T>
    {
        T GenerateId();
    }
}
