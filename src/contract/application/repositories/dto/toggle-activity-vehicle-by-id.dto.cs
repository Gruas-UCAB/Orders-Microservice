

using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.domain.value_objects;

namespace ProvidersMicroservice.src.contract.application.repositories.dto
{
    public record ToggleActivityVehicleByIdDto
    (
        ContractId contractId,
        VehicleId vehicleId
    );
}
