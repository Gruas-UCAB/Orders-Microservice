using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.value_objects;


namespace ProvidersMicroservice.src.contract.application.repositories.dto
{
    public record SaveVehicleDto
    (
        ContractId contractId,
        Vehicle  vehicle
    );
}
