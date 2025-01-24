using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.accept_order.types;
using OrdersMicroservice.src.order.application.commands.toggle_accept_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.toggle_accept_order
{
    public class ToggleAcceptOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<ToggleAcceptOrderCommand, ToggleAcceptOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<ToggleAcceptOrderResponse>> Execute(ToggleAcceptOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFind.HasValue())
            {
                return Result<ToggleAcceptOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "por aceptar", StringComparison.OrdinalIgnoreCase))
            {
                return Result<ToggleAcceptOrderResponse>.Failure(new OrderNotAssignendException());
            }
            if (data.Accepted) {
                order.SetStatus(new OrderStatus("aceptado"));
                await _orderRepository.UpdateOrder(order);
                await _publishEndpoint.Publish(new ConductorAcceptedOrder(Guid.Parse(order.GetId())));
            }
            else
            {
                order.SetStatus(new OrderStatus("por asignar"));
                order.RemoveConductorAssigned();
                await _orderRepository.UpdateOrder(order);
                await _publishEndpoint.Publish(new ConductorRejectedOrder(Guid.Parse(order.GetId())));
            }
            return Result<ToggleAcceptOrderResponse>.Success(new ToggleAcceptOrderResponse(order.GetId()));
        }
    }
}
