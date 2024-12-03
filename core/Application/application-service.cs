using OrdersMicroservice.Core.Common;

namespace OrdersMicroservice.Core.Application
{
    public interface IApplicationService<T, R>
    {
        Task<Result<R>> Execute(T data);
    }
}
