using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.order.application.commands.accept_order.types;
using OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order;
using OrdersMicroservice.src.order.application.commands.add_extra_costs_to_order.types;
using OrdersMicroservice.src.order.application.commands.assign_conductor;
using OrdersMicroservice.src.order.application.commands.assign_conductor.types;
using OrdersMicroservice.src.order.application.commands.cancel_order;
using OrdersMicroservice.src.order.application.commands.cancel_order.types;
using OrdersMicroservice.src.order.application.commands.create_order;
using OrdersMicroservice.src.order.application.commands.create_order.types;
using OrdersMicroservice.src.order.application.commands.finish_order;
using OrdersMicroservice.src.order.application.commands.finish_order.types;
using OrdersMicroservice.src.order.application.commands.locate_order;
using OrdersMicroservice.src.order.application.commands.locate_order.types;
using OrdersMicroservice.src.order.application.commands.pay_order;
using OrdersMicroservice.src.order.application.commands.pay_order.types;
using OrdersMicroservice.src.order.application.commands.start_order;
using OrdersMicroservice.src.order.application.commands.start_order.types;
using OrdersMicroservice.src.order.application.commands.toggle_accept_order;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.application.repositories.exceptions;
using OrdersMicroservice.src.order.domain.value_objects;
using OrdersMicroservice.src.order.infrastructure.dto;
using RestSharp;

namespace OrdersMicroservice.src.order.infrastructure
{
    [Route("order")]
    [ApiController]
    [Authorize(Policy = "CreationalOrderUser")]
    public class OrderController(IIdGenerator<string> idGenerator, IOrderRepository orderRepository, 
        IContractRepository contractRepository, IExtraCostRepository extraCostRepository, 
        IRestClient restClient, IBus bus, IPublishEndpoint publishEndpoint) : Controller
    { 
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        private readonly IBus _bus= bus;
        private readonly IRestClient _restClient = restClient;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommand command, [FromHeader(Name = "Authorization")] string token)
        {
            var dispartcherExistRequest = new RestRequest($"https://localhost:5350/user/{command.OrderDispatcherId}", Method.Get);
            dispartcherExistRequest.AddHeader("Authorization", token);
            var dispatcherFind = await _restClient.ExecuteAsync(dispartcherExistRequest);
            if (!dispatcherFind.IsSuccessful)
            {
                return NotFound(new { errorMessage = "Dispatcher not found" });
            }
            var service = new CreateOrderCommandHandler(_idGenerator, _orderRepository, _contractRepository, _publishEndpoint);
            var response = await service.Execute(command);
            await _bus.Publish(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Created("Created", new { id = response.Unwrap().Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersDto data)
        {
            var orders = await _orderRepository.GetAllOrders(data);
            if (!orders.HasValue())
            {
                return BadRequest(new { errorMessage = new NoOrdersFoundException().Message });
            }
            var ordersList = orders.Unwrap()
                .Select(
                    o => new
                    {
                        Id = o.GetId(),
                        OrderNumber = o.GetOrderNumber(),
                        OrderDate = o.GetDate(),
                        OrderStatus = o.GetStatus(),
                        IncidentType = o.GetIncidentType(),
                        Destination = o.GetDestination(),
                        Location = o.GetLocation(),
                        DispatcherId = o.GetDispatcherId(),
                        ConductorAssignedId = o.GetConductorAssignedId(),
                        Cost = o.GetCost(),
                        IsCostCoveredByPolicy = o.IsCostCoveredByPolicy(),
                        ExtraCosts = o.GetExtraCosts()
                            .Select(
                                ec => new
                                {
                                    Id = ec.GetId(),
                                    Description = ec.GetDescription(),
                                    Price = ec.GetPrice()
                                }).ToList(),
                        ContractId = o.GetContractId(),
                        Payed = o.IsPayed(),
                    }
                ).ToList();
            return Ok(ordersList);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderById(new OrderId(id));
            if (!order.HasValue())
            {
                return BadRequest(new { errorMessage = new OrderNotFoundExcepion().Message });
            }
            var orderData = order.Unwrap();
            var orderResponse = new
            {
                Id = orderData.GetId(),
                OrderNumber = orderData.GetOrderNumber(),
                OrderDate = orderData.GetDate(),
                OrderStatus = orderData.GetStatus(),
                IncidentType = orderData.GetIncidentType(),
                Destination = orderData.GetDestination(),
                Location = orderData.GetLocation(),
                DispatcherId = orderData.GetDispatcherId(),
                ConductorAssignedId = orderData.GetConductorAssignedId(),
                Cost = orderData.GetCost(),
                IsCostCoveredByPolicy = orderData.IsCostCoveredByPolicy(),
                ExtraCosts = orderData.GetExtraCosts()
                    .Select(
                        ec => new
                        {
                            Id = ec.GetId(),
                            Description = ec.GetDescription(),
                            Price = ec.GetPrice()
                        }).ToList(),
                ContractId = orderData.GetContractId(),
                Payed = orderData.IsPayed(),
            };
            return Ok(orderResponse);
        }

        [HttpPatch("assign-conductor/{orderId}")]
        public async Task<IActionResult> AssignConductor([FromBody] UpdateOrderDto data, [FromHeader(Name = "Authorization")] string token, string orderId)
        {
            if (data.ConductorAssignedId == null || data.TotalDistance == null)
            {
                return BadRequest(new { errorMessage = "ConductorAssignedId and TotalDistance are required" });
            }
            var conductorFindRequest = new RestRequest($"https://localhost:5250/provider/conductors/conductor/{data.ConductorAssignedId}", Method.Get);
            conductorFindRequest.AddHeader("Authorization", token);
            var conductorFind = await _restClient.ExecuteAsync(conductorFindRequest);
            if (!conductorFind.IsSuccessful)
            {
                return NotFound(new { errorMessage = conductorFind.Content});
            }
            var service = new AssignConductorCommandHandler(_orderRepository, _contractRepository, _publishEndpoint);
            var response = await service.Execute(new AssignConductorCommand(orderId, data.ConductorAssignedId, (decimal)data.TotalDistance));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Ok("The conductor has been assigned");
        }

        [HttpPatch("toggle-accept/{orderId}")]
        public async Task<IActionResult> ToggleAcceptOrder([FromBody] AcceptOrderDto data, [FromHeader(Name = "Authorization")] string token, string orderId)
        {
            var service = new ToggleAcceptOrderCommandHandler(_orderRepository, _publishEndpoint);
            var response = await service.Execute(new ToggleAcceptOrderCommand(orderId, data.Accepted));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var order = await _orderRepository.GetOrderById(new OrderId(orderId));
            if (!order.HasValue())
            {
                return NotFound(new { errorMessage = new OrderNotFoundExcepion().Message });
            }
            var UpdateConductorStatusRequest = new RestRequest($"https://localhost:5250/provider/conductors/conductor/toggle-activity/{order.Unwrap().GetConductorAssignedId()}", Method.Patch);
            UpdateConductorStatusRequest.AddHeader("Authorization", token);
            UpdateConductorStatusRequest.AddJsonBody(new { });
            var conductorStatusUpdated = await _restClient.ExecuteAsync(UpdateConductorStatusRequest);
            if (!conductorStatusUpdated.IsSuccessful)
            {
                return NotFound(new { errorMessage = conductorStatusUpdated.Content });
            }
            return data.Accepted ? Ok("The order has been accepted") : Ok("The order has been rejected");
        }

        [HttpPatch("locate/{orderId}")]
        public async Task<IActionResult> LocateOrder([FromHeader(Name = "Authorization")] string token, string orderId)
        {
            var service = new LocateOrderCommandHandler(_orderRepository, _publishEndpoint);
            var response = await service.Execute(new LocateOrderCommand(orderId));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var order = await _orderRepository.GetOrderById(new OrderId(orderId));
            if (!order.HasValue())
            {
                return NotFound(new { errorMessage = new OrderNotFoundExcepion().Message });
            }
            var UpdateConductorLocationRequest = new RestRequest($"https://localhost:5250/provider/conductors/conductor/location/{order.Unwrap().GetConductorAssignedId()}", Method.Patch);
            UpdateConductorLocationRequest.AddHeader("Authorization", token);
            UpdateConductorLocationRequest.AddJsonBody(new { location = order.Unwrap().GetLocation() });
            var conductorLocationUpdated = await _restClient.ExecuteAsync(UpdateConductorLocationRequest);
            if (!conductorLocationUpdated.IsSuccessful)
            {
                return NotFound(new { errorMessage = conductorLocationUpdated.Content });
            }
            return Ok("The order has been located");
        }

        [HttpPatch("start/{orderId}")]
        public async Task<IActionResult> StartOrder(string orderId)
        {
            var service = new StartOrderCommandHandler(_orderRepository, _publishEndpoint);
            var response = await service.Execute(new StartOrderCommand(orderId));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Ok("The order has been started");
        }

        [HttpPatch("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var service = new CancelOrderCommandHandler(_orderRepository, _publishEndpoint);
            var response = await service.Execute(new CancelOrderCommand(orderId));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Ok("The order has been cancelled");
        }

        [HttpPatch("finish/{orderId}")]
        public async Task<IActionResult> FinishOrder([FromHeader(Name = "Authorization")] string token, string orderId)
        {
            var service = new FinishOrderCommandHandler(_orderRepository, _publishEndpoint);
            var response = await service.Execute(new FinishOrderCommand(orderId));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            var order = await _orderRepository.GetOrderById(new OrderId(orderId));
            if (!order.HasValue())
            {
                return NotFound(new { errorMessage = new OrderNotFoundExcepion().Message });
            }
            var UpdateConductorLocationRequest = new RestRequest($"https://localhost:5250/provider/conductors/conductor/location/{order.Unwrap().GetConductorAssignedId()}", Method.Patch);
            UpdateConductorLocationRequest.AddHeader("Authorization", token);
            UpdateConductorLocationRequest.AddJsonBody(new { location = order.Unwrap().GetLocation() });
            var conductorLocationUpdated = await _restClient.ExecuteAsync(UpdateConductorLocationRequest);
            if (!conductorLocationUpdated.IsSuccessful)
            {
                return NotFound(new { errorMessage = conductorLocationUpdated.Content });
            }
            var UpdateConductorStatusRequest = new RestRequest($"https://localhost:5250/provider/conductors/conductor/toggle-activity/{order.Unwrap().GetConductorAssignedId()}", Method.Patch);
            UpdateConductorStatusRequest.AddHeader("Authorization", token);
            UpdateConductorStatusRequest.AddJsonBody(new { });
            var conductorStatusUpdated = await _restClient.ExecuteAsync(UpdateConductorStatusRequest);
            if (!conductorStatusUpdated.IsSuccessful)
            {
                return NotFound(new { errorMessage = conductorStatusUpdated.Content });
            }
            return Ok("The order has been finished");
        }

        [HttpPatch("pay/{orderId}")]
        public async Task<IActionResult> PayOrder(string orderId)
        {
            var service = new PayOrderCommandHandler(_orderRepository, publishEndpoint);
            var response = await service.Execute(new PayOrderCommand(orderId));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Ok("The order has been payed");
        }

        [HttpPatch("add-extra-costs/{orderId}")]
        public async Task<IActionResult> AddExtraCosts([FromBody] UpdateOrderDto data, string orderId)
        {
            var service = new AddExtraCostsToOrderCommandHandler(_orderRepository, _contractRepository, _extraCostRepository);
            var response = await service.Execute(new AddExtraCostsToOrderCommand(orderId, data.ExtraCosts!));
            if (response.IsFailure)
            {
                return BadRequest(new { errorMessage = response.ErrorMessage() });
            }
            return Ok("The extra costs have been added");
        }

    }
}
