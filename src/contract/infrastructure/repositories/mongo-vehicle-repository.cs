using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.infrastructure.models;


namespace OrdersMicroservice.src.contract.infrastructure.repositories
{
    public class MongoVehicleRepository : IVehicleRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> vehicleCollection;

        public MongoVehicleRepository()
        {
            vehicleCollection = _config.db.GetCollection<BsonDocument>("vehicles");
        }

        public async Task<_Optional<List<Vehicle>>> GetAllVehicles()
        {
            var vehicles = await vehicleCollection.Find(new BsonDocument()).ToListAsync();
            var vehicleList = vehicles.Select(vehicle => new Vehicle(
                new VehicleId(vehicle["_id"].AsString),
                new VehicleLicensePlate(vehicle["licensePlate"].AsString),
                new VehicleBrand(vehicle["brand"].AsString),
                new VehicleModel(vehicle["model"].AsString),
                new VehicleYear(vehicle["year"].AsInt32),
                new VehicleColor(vehicle["color"].AsString),
                new VehicleKm(vehicle["km"].AsDouble)
            )).ToList();

            if (vehicleList.Count == 0)
            {
                return _Optional<List<Vehicle>>.Empty();
            }

            return _Optional<List<Vehicle>>.Of(vehicleList);
        }

        public async Task<_Optional<Vehicle>> GetVehicleById(VehicleId id)
        {
            var bsonVehicle = await vehicleCollection.FindAsync(new BsonDocument { { "_id", id.GetId() } }).Result.FirstOrDefaultAsync();
            if (bsonVehicle == null)
            {
                return _Optional<Vehicle>.Empty();
            }
            var vehicle = new Vehicle(
                    new VehicleId(bsonVehicle["_id"].AsString),
                    new VehicleLicensePlate(bsonVehicle["licensePlate"].AsString),
                    new VehicleBrand(bsonVehicle["brand"].AsString),
                    new VehicleModel(bsonVehicle["model"].AsString),
                    new VehicleYear(bsonVehicle["year"].AsInt32),
                    new VehicleColor(bsonVehicle["color"].AsString),
                    new VehicleKm(bsonVehicle["km"].AsDouble)
                    );
            return _Optional<Vehicle>.Of(vehicle);
        }

        public async Task<Vehicle> SaveVehicle(Vehicle vehicle)
        {

            var mongoVehicle = new MongoVehicle
            {
                Id = vehicle.GetId(),
                LicensePlate = vehicle.GetLicensePlate(),
                Brand = vehicle.GetBrand(),
                Model = vehicle.GetModel(),
                Year = vehicle.GetYear(),
                Color = vehicle.GetColor(),
                Km = vehicle.GetKm(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoVehicle.Id},
                {"licensePlate", mongoVehicle.LicensePlate },
                {"brand", mongoVehicle.Brand },
                {"model", mongoVehicle.Model },
                {"year", mongoVehicle.Year },
                {"color", mongoVehicle.Color },
                {"km", mongoVehicle.Km },
                {"createdAt", mongoVehicle.CreatedAt },
                {"updatedAt", mongoVehicle.UpdatedAt }
            };

            await vehicleCollection.InsertOneAsync(bsonDocument);

            var savedVehicle = new Vehicle(
                new VehicleId(mongoVehicle.Id), 
                new VehicleLicensePlate(mongoVehicle.LicensePlate), 
                new VehicleBrand(mongoVehicle.Brand), 
                new VehicleModel(mongoVehicle.Model), 
                new VehicleYear(mongoVehicle.Year), 
                new VehicleColor(mongoVehicle.Color),
                new VehicleKm(mongoVehicle.Km));
            return savedVehicle;
        }
    }
}
