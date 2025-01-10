
using OrdersMicroservice.Core.Domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.domain
{
    public class Order(OrderId id) : AggregateRoot<OrderId>(id)
    {
        private OrderNumber _orderNumber;
        private OrderDate _date;
        private OrderStatus _status;
        private IncidentType _incidentType;
        private OrderDestination _destination;
        private OrderLocation _location;
        private OrderDispatcherId _dispatcherId;
        private ConductorAssignedId? _conductorAssignedId;
        private OrderCost _cost;
        private ContractId _contractId; 
        private bool _payed = false;
        private bool _costCoveredByPolicy = true;
        private List<ExtraCost> _extraCosts = [];
        private bool _isActive = true;

        protected override void ValidateState()
        {
            if (_orderNumber == null || _date == null || _status == null || 
                _incidentType == null || _destination == null || _location == null || 
                _dispatcherId == null || _cost == null)
            {
                throw new InvalidOrderException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }

        public int GetOrderNumber()
        {
            return _orderNumber.GetNumber();
        }

        public DateTime GetDate()
        {
            return _date.GetDate();
        }

        public string GetStatus()
        {
            return _status.GetStatus();
        }

        public void SetStatus(OrderStatus status)
        {
            _status = status;
        }

        public string GetIncidentType()
        {
            return _incidentType.GetIncidentType();
        }

        public string GetDestination()
        {
            return _destination.GetDestination();
        }

        public string GetLocation()
        {
            return _location.GetLocation();
        }

        public string GetDispatcherId()
        {
            return _dispatcherId.GetId();
        }

        public string? GetConductorAssignedId()
        {
            return _conductorAssignedId?.GetId();
        }

        public decimal GetCost()
        {
            return _cost.GetCost();
        }

        public void SetCost(OrderCost cost)
        {
            _cost = cost;
        }

        public bool IsPayed()
        {
            return _payed;
        }

        public string GetContractId()
        {
            return _contractId.GetId();
        }

        public List<ExtraCost> GetExtraCosts()
        {
            return _extraCosts;
        }
        public void CostNotCoveredByPolicy()
        {
            _costCoveredByPolicy = false;
        }

        public void CostCoveredByPolicy()
        {
            _costCoveredByPolicy = true;
        }
        public bool IsCostCoveredByPolicy()
        {
            return _costCoveredByPolicy;
        }
        public bool IsActive()
        {
            return _isActive;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }

        public static Order Create(OrderId id, OrderNumber orderNumber, OrderDate date, OrderStatus status, IncidentType incidentType, OrderDestination destination, OrderLocation location, OrderDispatcherId dispatcherId, OrderCost cost, ContractId contractId)
        {
            Order order = new(id);
            order.Apply(OrderCreated.CreateEvent(id, orderNumber, date, status, incidentType, destination, location, dispatcherId, cost, contractId));
            return order;
        }

        public ConductorAssignedId AssignConductor(ConductorAssignedId id)
        {
            Apply(ConductorAssigned.CreateEvent(_id, id));
            return _conductorAssignedId!;
        }

        public void AddExtraCosts(List<ExtraCost> extraCosts)
        {
            Apply(ExtraCostsAssigned.CreateEvent(_id, extraCosts));
        }

        public void Pay()
        {
            Apply(OrderPayed.CreateEvent(_id));
        }

        private void OnOrderCreatedEvent(OrderCreated Event) 
        {
            _orderNumber = new OrderNumber(Event.NumberOrder);
            _date = new OrderDate(Event.Date);
            _status = new OrderStatus(Event.Status);
            _incidentType = new IncidentType(Event.IncidentType);
            _destination = new OrderDestination(Event.Destination);
            _location = new OrderLocation(Event.Location);
            _dispatcherId = new OrderDispatcherId(Event.OrderDispatcherId);
            _cost = new OrderCost(Event.Cost);
            _contractId = new ContractId(Event.ContractId);
        }

        private void OnConductorAssignedEvent(ConductorAssigned Event)
        {
            _conductorAssignedId = new ConductorAssignedId(Event.ConductorId);
        }

        private void OnExtraCostsAssignedEvent(ExtraCostsAssigned Event)
        {
            _extraCosts = Event.ExtraCosts;
        }

        private void OnOrderPayedEvent(OrderPayed Event)
        {
            _payed = Event.Payed;
        }

    }
}