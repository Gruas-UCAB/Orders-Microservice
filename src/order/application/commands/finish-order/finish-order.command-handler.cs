using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.finish_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.finish_order
{
    public class FinishOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<FinishOrderCommand, FinishOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<FinishOrderResponse>> Execute(FinishOrderCommand data)
        {
            var orderFInd = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFInd.HasValue())
            {
                return Result<FinishOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFInd.Unwrap();
            if (!string.Equals(order.GetStatus(), "en proceso", StringComparison.OrdinalIgnoreCase))
            {
                return Result<FinishOrderResponse>.Failure(new OrderNotStartedException());
            }
            order.SetStatus(new OrderStatus("finalizado"));
            await _orderRepository.UpdateOrder(order);
            await _publishEndpoint.Publish(new OrderFinished(Guid.Parse(order.GetId())));
            return Result<FinishOrderResponse>.Success(new FinishOrderResponse(order.GetId()));
        }
    }
}
