using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.locate_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.locate_order
{
    public class LocateOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<LocateOrderCommand, LocateOrderResponse>
    { 
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<LocateOrderResponse>> Execute(LocateOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.OrderId));
            if (!orderFind.HasValue())
            {
                return Result<LocateOrderResponse>.Failure(new OrderNotFoundExcepion());
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "aceptado", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(order.GetStatus());
                return Result<LocateOrderResponse>.Failure(new OrderNotAcceptedException());
            }
            order.SetStatus(new OrderStatus("localizado"));
            await _orderRepository.UpdateOrder(order);
            await _publishEndpoint.Publish(new OrderLocatedByConductor(Guid.Parse(order.GetId())));
            return Result<LocateOrderResponse>.Success(new LocateOrderResponse(order.GetId()));
        }
    }
}
