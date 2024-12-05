/*using MongoDB.Bson.Serialization;*/


using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.extracost.application.commands.update_extracos.types;
using OrdersMicroservice.src.extracost.application.commands.update_extracost.types;
using OrdersMicroservice.src.extracost.application.repositories;
using OrdersMicroservice.src.extracost.application.repositories.exceptions;

namespace OrdersMicroservice.src.extracost.application.commands.update_extracost
{
    public class UpdateExtraCostByIdCommandHandler(IExtraCostRepository extraCostRepository) : IApplicationService<UpdateExtraCostByIdCommand, UpdateExtraCostByIdResponse>
    {
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
        public async Task<Result<UpdateExtraCostByIdResponse>> Execute(UpdateExtraCostByIdCommand data)
        {
            try
            {
                var extraCost = await _extraCostRepository.GetExtraCostById(new domain.value_objects.ExtraCostId(data.Id));
                if (!extraCost.HasValue())
                {
                    return Result<UpdateExtraCostByIdResponse>.Failure(new ExtraCostNotFoundException());
                }

                var extaCostToUpdate = extraCost.Unwrap();
                if (data.Description != null)
                {
                    extaCostToUpdate.UpdateExtraCostDescription(new domain.value_objects.ExtraCostDescription(data.Description));
                    Console.WriteLine("Cambio nombre");
                }

                if (data.Price != null)
                {
                    extaCostToUpdate.UpdateExtraCostPrice(new domain.value_objects.ExtraCostPrice(data.Price));
                    
                }
                ///los otros update

                await _extraCostRepository.UpdateExtraCostById(data);

                return Result<UpdateExtraCostByIdResponse>.Success(new UpdateExtraCostByIdResponse(extaCostToUpdate.GetExtraCostId()));
            }
            catch (Exception e) 
            {
                return Result<UpdateExtraCostByIdResponse>.Failure(e);
            }
        }
    }
}
