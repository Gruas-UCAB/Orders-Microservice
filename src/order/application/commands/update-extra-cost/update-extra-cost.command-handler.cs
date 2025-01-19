using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.commands.update_extra_cost.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.exceptions;

namespace OrdersMicroservice.src.order.application.commands.update_extra_cost
{
    public class UpdateExtraCostCommandHandler(IExtraCostRepository extraCostRepository) : IApplicationService<UpdateExtraCostCommand, UpdateExtraCostResponse>
    {
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        public async Task<Result<UpdateExtraCostResponse>> Execute(UpdateExtraCostCommand data)
        {
            var extraCostFind = await _extraCostRepository.GetExtraCostById(new ExtraCostId(data.Id));
            if (!extraCostFind.HasValue())
                return Result<UpdateExtraCostResponse>.Failure(new ExtraCostNotFoundException());
            var extraCost = extraCostFind.Unwrap();
            if (data.Description != null)
                extraCost.SetDescription(new ExtraCostDescription(data.Description));
            if (data.DefaultPrice != null)
                extraCost.SetPrice(new ExtraCostPrice((decimal)data.DefaultPrice));
            await _extraCostRepository.UpdateExtraCost(extraCost);
            return Result<UpdateExtraCostResponse>.Success(new UpdateExtraCostResponse(extraCost.GetId()));
        }
    }
}
