
using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.policy;

namespace OrdersMicroservice.src.contract.application.repositories
{
    public interface IContractRepository
    {    
        Task<Contract> SaveContract(Contract contract);
        Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data);
        Task<_Optional<Contract>> GetContractById(ContractId id);
        Task<_Optional<Contract>> GetContractByContractNumber(NumberContract contractNumber);
        Task<ContractId> UpdateContract(Contract contract);
        Task<ContractId> ToggleActivityContractById(ContractId id);
        Task<_Optional<Vehicle>> GetContractVehicle(ContractId id);
        Task<_Optional<Policy>> GetContractPolicy(ContractId id);
    }
}
