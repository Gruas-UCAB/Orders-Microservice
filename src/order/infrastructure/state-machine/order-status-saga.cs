using MassTransit;
using OrdersMicroservice.src.order.application.state_machine.events;

namespace OrdersMicroservice.src.order.infrastructure.state_machine
{
    public class OrderStatusSaga : MassTransitStateMachine<OrderStatusSagaData>
    {
        public State PorAsignar { get; set; }
        public State PorAceptar { get; set; }
        public State Aceptado { get; set; }
        public State Localizado { get; set; }
        public State Cancelado { get; set; }
        public State EnProceso { get; set; }
        public State Finalizado { get; set; }
        public State Pagado { get; set; }

        public Event<OrderCreated> OrderCreated { get; set; }
        public Event<OrderAssignedToConductor> OrderAssignedToConductor { get; set; }
        public Event<OrderFinished> OrderFinished { get; set; }
        public Event<OrderCancelled> OrderCancelled { get; set; }
        public Event<OrderPayed> OrderPayed { get; set; }
        public Event<ConductorAcceptedOrder> ConductorAcceptedOrder { get; set; }
        public Event<ConductorRejectedOrder> ConductorRejectedOrder { get; set; }
        public Event<OrderLocatedByConductor> OrderLocatedByConductor { get; set; }
        public Event<OrderStartedByConductor> OrderStartedByConductor { get; set; }

        public OrderStatusSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderAssignedToConductor, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderFinished, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderCancelled, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderPayed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => ConductorAcceptedOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => ConductorRejectedOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderLocatedByConductor, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderStartedByConductor, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(OrderCreated)
                    .Then(context =>
                    {
                        context.Saga.CorrelationId = context.Message.OrderId;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                    })
                .TransitionTo(PorAsignar)
            );

            During(PorAsignar,
                When(OrderAssignedToConductor)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(PorAceptar)
            );

            During(PorAceptar,
                When(ConductorAcceptedOrder)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(Aceptado)
            );

            During(PorAceptar,
                When(ConductorRejectedOrder)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(PorAsignar)
            );

            During(Aceptado,
               When(OrderLocatedByConductor)
               .Then(
                   context => context.Saga.UpdatedAt = DateTime.UtcNow
                   )
               .TransitionTo(Localizado)
           );

            During(Localizado,
                When(OrderStartedByConductor)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(EnProceso)
            );

            During(Localizado,
                When(OrderCancelled)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(Cancelado)
            );

            During(EnProceso,
                When(OrderFinished)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(Finalizado)
            );

            During(EnProceso,
                When(OrderCancelled)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(Cancelado)
            );

            During(Finalizado,
                When(OrderPayed)
                .Then(
                    context => context.Saga.UpdatedAt = DateTime.UtcNow
                    )
                .TransitionTo(Pagado)
            );

        }
    }
}
