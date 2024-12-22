
using OrdersMicroservice.src.vehicle.domain;
using OrdersMicroservice.src.vehicle.domain.value_objects;
using OrdersMicroservice.core.Common;

namespace OrdersMicroservice.src.vehicle.application.repositories
{
    public interface IVehicleRepository
    {
        public Task<Vehicle> SaveVehicle(Vehicle vehicle);
        public Task<_Optional<List<Vehicle>>> GetAllVehicles();
        public Task<_Optional<Vehicle>> GetVehicleById(VehicleId id);
    }
}
