using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderDate : IValueObject<OrderDate>
    {
        private readonly DateTime _date;
        public OrderDate(DateTime date)
        {
            if (date < DateTime.UtcNow)
            {
                throw new InvalidOrderDateException();
            }
            _date = date;
        }

        public DateTime GetDate()
        {
            return _date;
        }
        public bool Equals(OrderDate other)
        {
            return _date == other.GetDate();
        }
    }
}
