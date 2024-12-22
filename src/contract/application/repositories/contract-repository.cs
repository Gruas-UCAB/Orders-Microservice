
using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.repositories.dto;

namespace OrdersMicroservice.src.contract.application.repositories
{
    public interface IContractRepository
    {
        public Task<Contract> SaveContract(Contract contract);
        public Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data);
        public Task<_Optional<Contract>> GetContractById(ContractId id);
        public Task<ContractId> UpdateContractById(UpdateContractByIdCommand command);

        public Task<ContractId> ToggleActivityContractById(ContractId id);

    }
}
