﻿using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.domain;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.repositories
{
    public interface IOrderRepository
    {
        Task<Order> SaveOrder(Order order);
        Task<_Optional<List<Order>>> GetAllOrders(GetAllOrdersDto data);
        Task<_Optional<List<Order>>> GetAllAssignedOrdersToUnnassign();
        Task<_Optional<Order>> GetCurrentOrderByConductorId(ConductorAssignedId id);
        Task<_Optional<List<Order>>> GetAllOrdersByConductorId(ConductorAssignedId id);
        Task<_Optional<Order>> GetOrderById(OrderId id);
        Task<Order> UpdateOrder(Order order);
    }
}
