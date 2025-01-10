namespace OrdersMicroservice.Core.Domain
{
    public interface IDomainService<T,R>
    {
        R Execute(T data);   
    }
}
