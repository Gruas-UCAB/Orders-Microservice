using MassTransit.Transports;
using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.cancel_order.types;
using OrdersMicroservice.src.order.application.commands.start_order.types;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.cancel_order
{
    public class CancelOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<CancelOrderCommand, CancelOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<CancelOrderResponse>> Execute(CancelOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.Id));
            if (!orderFind.HasValue())
            {
                return Result<CancelOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "localizado", StringComparison.OrdinalIgnoreCase) && !string.Equals(order.GetStatus(), "en proceso", StringComparison.OrdinalIgnoreCase))
            {
                return Result<CancelOrderResponse>.Failure(new OrderCantBeCancelledException());
            }
            order.SetStatus(new OrderStatus("cancelado"));
            await _orderRepository.UpdateOrder(order);
            await _publishEndpoint.Publish(new OrderStartedByConductor(Guid.Parse(order.GetId())));
            return Result<CancelOrderResponse>.Success(new CancelOrderResponse(order.GetId()));
        }
    }
}
