
using OrdersMicroservice.core.Common;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;



namespace OrdersMicroservice.src.contract.application.repositories
{
    public interface IVehicleRepository
    {
        public Task<Vehicle> SaveVehicle(Vehicle vehicle);
        public Task<_Optional<List<Vehicle>>> GetAllVehicles();
        public Task<_Optional<Vehicle>> GetVehicleById(VehicleId id);
    }
}
