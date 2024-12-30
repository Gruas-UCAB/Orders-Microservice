
using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.policy;
using ProvidersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;


namespace OrdersMicroservice.src.contract.application.repositories
{
    public interface IContractRepository
    {    
        //contract 
        Task<Contract> SaveContract(Contract contract);
         Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data);
         Task<_Optional<Contract>> GetContractById(ContractId id);
         Task<ContractId> UpdateContractById(UpdateContractByIdCommand command);
         Task<ContractId> ToggleActivityContractById(ContractId id);

         //vehicle
         Task<Vehicle> SaveVehicle(SaveVehicleDto data);
         Task<_Optional<List<Vehicle>>> GetAllVehicles(GetAllVehiclesDto data, ContractId contractId);
         Task<_Optional<Vehicle>> GetVehicleById(GetVehicleByIdDto data);
         Task<VehicleId> ToggleActivityVehicleById(ToggleActivityVehicleByIdDto data);

         //policy
         Task<Policy> SavePolicy(SavePolicyDto data);
         Task<_Optional<List<Policy>>> GetAllPolices(GetAllPolicesDto data, ContractId contractId);
         Task<_Optional<Policy>> GetPolicyById(GetPolicyByIdDto data);
         Task<PolicyId> ToggleActivityPolicyById(ToggleActivityPolicyByIdDto data);
    }
}
