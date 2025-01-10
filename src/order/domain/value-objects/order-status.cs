using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.order.domain.exceptions;

namespace OrdersMicroservice.src.order.domain.value_objects
{
    public class OrderStatus : IValueObject<OrderStatus>
    {
        private readonly List<string> _validStatus = 
            ["por asignar", "por aceptar", "aceptado", 
            "localizado", "cancelado", "en proceso", 
            "finalizado", "pagado"];
        private readonly string _status;

        public OrderStatus(string status)
        {
            if (!_validStatus.Contains(status.ToLower()))
            {
                throw new InvalidOrderStatusException();
            }
            _status = status.ToLower();
        }
        
        public string GetStatus()
        {
            return _status.ToLower();
        }   
        public bool Equals(OrderStatus other)
        {
            return _status == other.GetStatus();
        }
    }
}
