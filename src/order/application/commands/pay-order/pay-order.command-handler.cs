using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.pay_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.pay_order
{
    public class PayOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<PayOrderCommand, PayOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<PayOrderResponse>> Execute(PayOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFind.HasValue())
            {
                return Result<PayOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "finalizado", StringComparison.OrdinalIgnoreCase))
            {
                return Result<PayOrderResponse>.Failure(new OrderCantBePaidException());
            }
            order.Pay();
            order.SetStatus(new OrderStatus("pagado"));
            await _orderRepository.UpdateOrder(order);
            await _publishEndpoint.Publish(new OrderPayed(Guid.Parse(order.GetId())));
            return Result<PayOrderResponse>.Success(new PayOrderResponse(order.GetId()));
        }
    }
}
