using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.create_extra_cost.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.application.commands.create_extra_cost
{
    public class CreateExtraCostCommandHandler(IExtraCostRepository extraCostRepository, IIdGenerator<string> idGenerator) : IApplicationService<CreateExtraCostCommand, CreateExtraCostResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        public async Task<Result<CreateExtraCostResponse>> Execute(CreateExtraCostCommand data)
        {
            try
            {
                var extraCost = new ExtraCost(
                    new ExtraCostId(_idGenerator.GenerateId()),
                    new ExtraCostDescription(data.Description),
                    new ExtraCostPrice(data.DefaultPrice)
                );
                await _extraCostRepository.SaveExtraCost(extraCost);
                return Result<CreateExtraCostResponse>.Success(new CreateExtraCostResponse(extraCost.GetId()));
            }catch (Exception e)
            {
                return Result<CreateExtraCostResponse>.Failure(e);
            }
        }
    }
}
