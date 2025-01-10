using MassTransit;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.order.application.commands.start_order.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.application.state_machine.events;
using OrdersMicroservice.src.order.domain.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;

namespace OrdersMicroservice.src.order.application.commands.start_order
{
    public class StartOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint) : IApplicationService<StartOrderCommand, StartOrderResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        public async Task<Result<StartOrderResponse>> Execute(StartOrderCommand data)
        {
            var orderFind = await _orderRepository.GetOrderById(new OrderId(data.Id));
            if (!orderFind.HasValue())
            { 
                return Result<StartOrderResponse>.Failure(new OrderNotFoundExcepion()); 
            }
            var order = orderFind.Unwrap();
            if (!string.Equals(order.GetStatus(), "localizado", StringComparison.OrdinalIgnoreCase))
            {
                return Result<StartOrderResponse>.Failure(new OrderNotLocatedException());
            }
            order.SetStatus(new OrderStatus("en proceso"));
            await _orderRepository.UpdateOrder(order);
            await _publishEndpoint.Publish(new OrderStartedByConductor(Guid.Parse(order.GetId())));
            return Result<StartOrderResponse>.Success(new StartOrderResponse(order.GetId()));
        }
    }
}
