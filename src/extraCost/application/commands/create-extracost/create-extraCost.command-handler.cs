using MongoDB.Driver;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;

using OrdersMicroservice.src.extracost.application.commands.create_extracost.types;
using OrdersMicroservice.src.extracost.application.repositories;
using OrdersMicroservice.src.extracost.domain;

using OrdersMicroservice.src.extracost.domain.value_objects;

namespace OrdersMicroservice.src.extracost.application.commands.extracost
{
    public class CreateExtraCostCommandHandler(IIdGenerator<string> idGenerator, IExtraCostRepository extraCostRepository) : IApplicationService<CreateExtraCostCommand, CreateExtraCostResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IExtraCostRepository _extraCostRepository = extraCostRepository;
    

        public async Task<Result<CreateExtraCostResponse>> Execute(CreateExtraCostCommand data)
        {
            try
            {

                var id = _idGenerator.GenerateId();
        
                var description = data.Description;
                var price = data.Price;
                

                var extacost = ExtraCost.Create(
                    new ExtraCostId(id),
                    new ExtraCostDescription(description),
                    new ExtraCostPrice(price)

                    
                    );
                var events = extacost.PullEvents();
                await _extraCostRepository.SaveExtraCost(extacost);
                return Result<CreateExtraCostResponse>.Success(new CreateExtraCostResponse(id));
            } catch (Exception e)
            {
                return Result<CreateExtraCostResponse>.Failure(e);
            }
        }
    }
}
