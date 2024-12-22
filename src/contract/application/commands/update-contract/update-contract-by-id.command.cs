using MongoDB.Bson.Serialization;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.Core.Application;
using OrdersMicroservice.Core.Common;

using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.exceptions;

using OrdersMicroservice.src.user.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;




namespace OrdersMicroservice.src.contract.application.commands.update_contract
{
    public class UpdateContractByIdCommandHandler(IContractRepository contractRepository) : IApplicationService<UpdateContractByIdCommand, UpdateContractByIdResponse>
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        public async Task<Result<UpdateContractByIdResponse>> Execute(UpdateContractByIdCommand data)
        {
            try
            {
                var contract = await _contractRepository.GetContractById(new domain.value_objects.ContractId(data.Id));
                if ( contract.HasValue())
                {
                    return Result<UpdateContractByIdResponse>.Failure(new ContractNotFoundException());
                }

                var contractToUpdate = contract.Unwrap();
                if (data.NumberContract != null)
                {
                    contractToUpdate.UpdateNumberContract(new domain.value_objects.NumberContract(data.NumberContract));
                    Console.WriteLine("Cambio nombre");
                }
                if (data.ExpirationDate != null)
                {
                    Console.WriteLine("Cambio telefono");
                    contractToUpdate.UpdateExpirationDate(new domain.value_objects.ContractExpitionDate(data.ExpirationDate));
                }

                await _contractRepository.UpdateContractById(data);

                return Result<UpdateContractByIdResponse>.Success(new UpdateContractByIdResponse(contractToUpdate.GetContractId()));
            }
            catch (Exception e) 
            {
                return Result<UpdateContractByIdResponse>.Failure(e);
            }
        }
    }
}
