using System.Reflection;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.Core.Domain;

namespace OrdersMicroservice.core.Application
{
    public class Subscription(Action unsubscribe)
    {
        public Action _unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));

        public void Unsubscribe()
        {
            _unsubscribe();
        }
    }
    public interface IEventStore
    {
        Task<Result<Unit>> AppendEvents(string stream, List<DomainEvent<object>> events);
        Task<List<DomainEvent<object>>> GetEventsByStream(string stream);
        Subscription Subscribe(string eventName, Func<DomainEvent<object>, Task> handler);
        public struct Unit { }
    }
}
