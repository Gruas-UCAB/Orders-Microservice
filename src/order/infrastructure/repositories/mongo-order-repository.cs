using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.domain;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.domain.value_objects;
using OrdersMicroservice.src.order.infrastructure.models;

namespace OrdersMicroservice.src.order.infrastructure.repositories
{
    public class MongoOrderRepository : IOrderRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> _orderCollection;
        public MongoOrderRepository()
        {
            _orderCollection = _config.db.GetCollection<BsonDocument>("orders");
        }
        public async Task<Order> SaveOrder(Order order)
        {
            var mongoOrder = new MongoOrder
            {
                Id = order.GetId(),
                OrderNumber = order.GetOrderNumber(),
                Date = order.GetDate(),
                OrderStatus = order.GetStatus(),
                IncidentType = order.GetIncidentType(),
                Destination = order.GetDestination(),
                Location = order.GetLocation(),
                DispatcherId = order.GetDispatcherId(),
                ConductorAssignedId = order.GetConductorAssignedId(),
                ContractId = order.GetContractId(),
                Cost = order.GetCost(),
                ExtraCosts = order.GetExtraCosts(),
                IsCostCoveredByPolicy = order.IsCostCoveredByPolicy(),
                Payed = false,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var bsonOrder = new BsonDocument
            {
                {"_id", mongoOrder.Id},
                {"orderNumber", mongoOrder.OrderNumber},
                {"date", mongoOrder.Date},
                {"orderStatus", mongoOrder.OrderStatus},
                {"incidentType", mongoOrder.IncidentType},
                {"destination", mongoOrder.Destination},
                {"location", mongoOrder.Location},
                {"dispatcherId", mongoOrder.DispatcherId},
                {"conductorAssignedId", BsonNull.Value},
                {"contractId", mongoOrder.ContractId},
                {"cost", mongoOrder.Cost},
                {"isCostCoveredByPolicy", mongoOrder.IsCostCoveredByPolicy},
                {"extraCosts", new BsonArray()},
                {"payed", mongoOrder.Payed},
                {"isActive", mongoOrder.IsActive},
                {"createdAt", mongoOrder.CreatedAt},
                {"updatedAt", mongoOrder.UpdatedAt}
            };
            await _orderCollection.InsertOneAsync(bsonOrder);
            return order;
        }

        public async Task<_Optional<List<Order>>> GetAllOrders(GetAllOrdersDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var orders = await _orderCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
            var ordersList = new List<Order>();
            Console.WriteLine();
            foreach(var bsonOrder in orders)
            {
                var order = Order.Create(
                        new OrderId(bsonOrder["_id"].AsString),
                        new OrderNumber(bsonOrder["orderNumber"].AsInt32),
                        new OrderDate((DateTime)bsonOrder["date"]),
                        new OrderStatus(bsonOrder["orderStatus"].AsString),
                        new IncidentType(bsonOrder["incidentType"].AsString),
                        new OrderDestination(bsonOrder["destination"].AsString),
                        new OrderLocation(bsonOrder["location"].AsString),
                        new OrderDispatcherId(bsonOrder["dispatcherId"].AsString),
                        new OrderCost(bsonOrder["cost"].AsDecimal),
                        new ContractId(bsonOrder["contractId"].AsString)                          
                    );
                if (bsonOrder["conductorAssignedId"] != BsonNull.Value)
                {
                    order.AssignConductor(new ConductorAssignedId(bsonOrder["conductorAssignedId"].AsString));
                }
                if (bsonOrder["extraCosts"].AsBsonArray.Count > 0)
                {
                    var extraCosts = new List<ExtraCost>();
                    foreach (var extraCost in bsonOrder["extraCosts"].AsBsonArray)
                    {
                        extraCosts.Add(new ExtraCost(
                            new ExtraCostId(extraCost["id"].AsString),
                            new ExtraCostDescription(extraCost["description"].AsString),
                            new ExtraCostPrice(extraCost["price"].AsDecimal)
                        ));
                    }
                    order.AddExtraCosts(extraCosts);
                }
                if (!bsonOrder["isCostCoveredByPolicy"].AsBoolean)
                {
                    order.CostNotCoveredByPolicy();
                }
                if (bsonOrder["payed"].AsBoolean)
                {
                    order.Pay();
                }
                if (!bsonOrder["isActive"].AsBoolean)
                {
                    order.ChangeStatus();
                }
                ordersList.Add(order);
            }
            return (ordersList.Count == 0) ? _Optional<List<Order>>.Empty() : _Optional<List<Order>>.Of(ordersList);
        }

        public async Task<_Optional<List<Order>>> GetAllOrdersByConductorId(ConductorAssignedId id)
        {
            var ordersFind = await GetAllOrders(new GetAllOrdersDto());
            if (!ordersFind.HasValue())
                throw new NoOrdersFoundException();
            var ordersOfConductor = new List<Order>();
            var orders = ordersFind.Unwrap();
            orders.ForEach(order =>
            {
                if (order.GetConductorAssignedId() == id.GetId())
                    ordersOfConductor.Add(order);
            });
            return ordersOfConductor.Count == 0 ? _Optional<List<Order>>.Empty() : _Optional<List<Order>>.Of(ordersOfConductor);
        }

        public async Task<_Optional<Order>> GetOrderById(OrderId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonOrder = await _orderCollection.Find(filter).FirstOrDefaultAsync();
                if (bsonOrder == null)
                {
                    return _Optional<Order>.Empty();
                }
                var order = Order.Create(
                        new OrderId(bsonOrder["_id"].AsString),
                        new OrderNumber(bsonOrder["orderNumber"].AsInt32),
                        new OrderDate((DateTime)bsonOrder["date"]),
                        new OrderStatus(bsonOrder["orderStatus"].AsString),
                        new IncidentType(bsonOrder["incidentType"].AsString),
                        new OrderDestination(bsonOrder["destination"].AsString),
                        new OrderLocation(bsonOrder["location"].AsString),
                        new OrderDispatcherId(bsonOrder["dispatcherId"].AsString),
                        new OrderCost(bsonOrder["cost"].AsDecimal),
                        new ContractId(bsonOrder["contractId"].AsString)
                    );

                if (bsonOrder["conductorAssignedId"] != BsonNull.Value)
                {
                    order.AssignConductor(new ConductorAssignedId(bsonOrder["conductorAssignedId"].AsString));
                }

                if (bsonOrder["extraCosts"].AsBsonArray.Count > 0)
                {
                    var extraCosts = new List<ExtraCost>();
                    foreach (var extraCost in bsonOrder["extraCosts"].AsBsonArray)
                    {
                        extraCosts.Add(new ExtraCost(
                            new ExtraCostId(extraCost["id"].AsString),
                            new ExtraCostDescription(extraCost["description"].AsString),
                            new ExtraCostPrice(extraCost["price"].AsDecimal)
                        ));
                    }
                    order.AddExtraCosts(extraCosts);
                }
                if (!bsonOrder["isCostCoveredByPolicy"].AsBoolean)
                {
                    order.CostNotCoveredByPolicy();
                }
                if (bsonOrder["payed"].AsBoolean)
                {
                    order.Pay();
                }
                if (!bsonOrder["isActive"].AsBoolean)
                {
                    order.ChangeStatus();
                }
                return _Optional<Order>.Of(order);
            }
            catch (Exception)
            {
                return _Optional<Order>.Empty();
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var extraCosts = order.GetExtraCosts();
            var extraCostsBson = new BsonArray();
            extraCosts.ForEach(extracost =>
            {
                extraCostsBson.Add(new BsonDocument
                {
                    {"id", extracost.GetId()},
                    {"description", extracost.GetDescription()},
                    {"price", extracost.GetPrice()}
                });
            });

            var filter = Builders<BsonDocument>.Filter.Eq("_id", order.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("orderStatus", order.GetStatus())
                .Set("conductorAssignedId", order.GetConductorAssignedId())
                .Set("contractId", order.GetContractId())
                .Set("cost", order.GetCost())
                .Set("isCostCoveredByPolicy", order.IsCostCoveredByPolicy())
                .Set("extraCosts", extraCostsBson)
                .Set("payed", order.IsPayed())
                .Set("updatedAt", DateTime.Now);
            await _orderCollection.UpdateOneAsync(filter, update);
            return order;
        }

        public async Task<_Optional<List<Order>>> GetAllAssignedOrdersToUnnassign()
        {
            var ordersFind = await GetAllOrders(new GetAllOrdersDto());
            if (!ordersFind.HasValue())
            {
                throw new NoOrdersFoundException();
            }
            var orders = ordersFind.Unwrap();
            var ordersList = new List<Order>();
            foreach (var order in orders) {
                if (string.Equals(order.GetStatus(), "por aceptar", StringComparison.OrdinalIgnoreCase))
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", order.GetId());
                    var bsonOrder = await _orderCollection.Find(filter).FirstOrDefaultAsync();
                    if (bsonOrder["updatedAt"].ToUniversalTime() > DateTime.Now.AddMinutes(-6).ToUniversalTime())
                    {
                        ordersList.Add(order);
                    }
                }
            }
            if (ordersList.Count == 0)
            {
                return _Optional<List<Order>>.Empty();
            }
            return _Optional<List<Order>>.Of(ordersList);
        }

        public async Task<_Optional<Order>> GetCurrentOrderByConductorId(ConductorAssignedId id)
        {
            var ordersFind = await GetAllOrders(new GetAllOrdersDto());
            if (!ordersFind.HasValue())
            {
                return _Optional<Order>.Empty();
            }
            var orders = ordersFind.Unwrap();
            var currentOrder = orders.FirstOrDefault(o => o.GetConductorAssignedId() == id.GetId()
            && !string.Equals(o.GetStatus(), "finalizado", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(o.GetStatus(), "pagado", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(o.GetStatus(), "cancelado", StringComparison.OrdinalIgnoreCase));
            if (currentOrder == null)
            {
                return _Optional<Order>.Empty();
            }
            return _Optional<Order>.Of(currentOrder);
        }
    }
}
