/*using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;
using OrdersMicroservice.src.contract.application.commands.create_department.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace OrdersMicroservice.src.contract.application.commands.create_user
{
    public class CreateContractCommandHandler(IIdGenerator<string> idGenerator, IContractRepository contractRepository) : IApplicationService<CreateContractCommand, CreateContractResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IContractRepository _contractRepository = contractRepository;
        public async Task<Result<CreateContractResponse>> Execute(CreateContractCommand data)
        {
            try
            {
                var id = _idGenerator.GenerateId();
                //atributes

                var contract = Contract.Create(
                    new ContractId(id)
                    
                    );
                var events = contract.PullEvents();
                await _contractRepository.SaveContract(contract);
                return Result<CreateContractResponse>.Success(new CreateContractResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateContractResponse>.Failure(e);
            }
        }
    }
}
*/