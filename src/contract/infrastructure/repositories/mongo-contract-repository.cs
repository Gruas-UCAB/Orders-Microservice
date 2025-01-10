using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.application.repositories.dto;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain;
using OrdersMicroservice.src.contract.domain.value_objects;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.domain.entities.vehicle;
using OrdersMicroservice.src.contract.domain.entities.vehicle.value_objects;
using OrdersMicroservice.src.contract.infrastructure.models;

namespace contractsMicroservice.src.contract.infrastructure.repositories
{
    public class MongoContractRepository : IContractRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> _contractCollection;

        public MongoContractRepository()
        {
            _contractCollection = _config.db.GetCollection<BsonDocument>("contracts");
        }

        public async Task<Contract> SaveContract(Contract contract)
        {
            var mongoContract = new MongoContract
            {
                Id = contract.GetId(),
                NumberContract = contract.GetContractNumber(),
                ExpirationDate = contract.GetContractExpirationDate(),
                Vehicle = contract.GetVehicle(),
                Policy = contract.GetPolicy(),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var mongoVehicle = new MongoVehicle 
            {
                Id = contract.GetVehicle().GetId(),
                LicensePlate = contract.GetVehicle().GetLicensePlate(),
                Brand = contract.GetVehicle().GetBrand(),
                Model = contract.GetVehicle().GetModel(),
                Year = contract.GetVehicle().GetYear(),
                Color = contract.GetVehicle().GetColor(),
                Km = contract.GetVehicle().GetKm(),
                OwnerDni = contract.GetVehicle().GetOwnerDni(),
                OwnerName = contract.GetVehicle().GetOwnerName()
            };

            var bsonVehicle = new BsonDocument
            {
                {"_id", mongoVehicle.Id},
                {"licensePlate", mongoVehicle.LicensePlate},
                {"brand", mongoVehicle.Brand},
                {"model", mongoVehicle.Model},
                {"year", mongoVehicle.Year},
                {"color", mongoVehicle.Color},
                {"km", mongoVehicle.Km},
                {"ownerDni", mongoVehicle.OwnerDni},
                {"ownerName", mongoVehicle.OwnerName }
            };

            var mongoPolicy = new MongoPolicy
            {
                Id = contract.GetPolicy().GetId(),
                Name = contract.GetPolicy().GetName(),
                KmCoverage = contract.GetPolicy().GetkmCoverage(),
                MonetaryCoverage = contract.GetPolicy().GetMonetaryCoverage(),
                BaseKmPrice = contract.GetPolicy().GetBaseKmPrice(),
            };

            var bsonPolicy = new BsonDocument
            {
                {"_id", mongoPolicy.Id},
                {"name", mongoPolicy.Name},
                {"kmCoverage", mongoPolicy.KmCoverage},
                {"monetaryCoverage", mongoPolicy.MonetaryCoverage},
                {"baseKmPrice", mongoPolicy.BaseKmPrice}
            };

            var bsonContract = new BsonDocument
            {
                {"_id", mongoContract.Id},
                {"numberContract", mongoContract.NumberContract},
                {"expirationDate", mongoContract.ExpirationDate},
                {"vehicle", bsonVehicle},
                {"policy", bsonPolicy},
                {"isActive", mongoContract.IsActive},
                {"createdAt", mongoContract.CreatedAt },
                {"updatedAt", mongoContract.UpdatedAt }
            };
            await _contractCollection.InsertOneAsync(bsonContract);
            return contract;
        }
        public async Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var contracts = await _contractCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
            var contractsList = new List<Contract>();
            foreach(var bsonContract in contracts)
            {
                var vehicle = bsonContract["vehicle"].AsBsonDocument;
                var policy = bsonContract["policy"].AsBsonDocument;
                var contract = Contract.Create(
                    new ContractId(bsonContract["_id"].AsString),
                    new NumberContract(bsonContract["numberContract"].AsInt32),
                    new ContractExpitionDate(((DateTime)bsonContract["expirationDate"])),
                    new Vehicle(
                        new VehicleId(vehicle["_id"].AsString),
                        new VehicleLicensePlate(vehicle["licensePlate"].AsString),
                        new VehicleBrand(vehicle["brand"].AsString),
                        new VehicleModel(vehicle["model"].AsString),
                        new VehicleYear(vehicle["year"].AsInt32),
                        new VehicleColor(vehicle["color"].AsString),
                        new VehicleKm(vehicle["km"].AsInt32),
                        new VehicleOwnerDni(vehicle["ownerDni"].AsInt32),
                        new VehicleOwnerName(vehicle["ownerName"].AsString)
                    ),
                    new Policy(
                        new PolicyId(policy["_id"].AsString),
                        new PolicyName(policy["name"].AsString),
                        new PolicyMonetaryCoverage(policy["monetaryCoverage"].AsDecimal),
                        new PolicyKmCoverage(policy["kmCoverage"].AsDecimal),
                        new PolicyBaseKmPrice(policy["baseKmPrice"].AsDecimal)
                    )
                );
                if (bsonContract["isActive"].AsBoolean)
                {
                    contract.ChangeStatus();
                }
                contractsList.Add(contract);
            }
            return (contractsList.Count == 0) ? _Optional<List<Contract>>.Empty() : _Optional<List<Contract>>.Of(contractsList);
        }
        public async Task<_Optional<Contract>> GetContractByContractNumber(NumberContract contractNumber)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("numberContract", contractNumber.GetNumberContract());
                var bsonContract = await _contractCollection.Find(filter).FirstOrDefaultAsync();
                if (bsonContract == null)
                {
                    return _Optional<Contract>.Empty();
                }
                var vehicle = bsonContract["vehicle"].AsBsonDocument;
                var policy = bsonContract["policy"].AsBsonDocument;
                var contract = Contract.Create(
                    new ContractId(bsonContract["_id"].AsString),
                    new NumberContract(bsonContract["numberContract"].AsInt32),
                    new ContractExpitionDate(((DateTime)bsonContract["expirationDate"])),
                    new Vehicle(
                        new VehicleId(vehicle["_id"].AsString),
                        new VehicleLicensePlate(vehicle["licensePlate"].AsString),
                        new VehicleBrand(vehicle["brand"].AsString),
                        new VehicleModel(vehicle["model"].AsString),
                        new VehicleYear(vehicle["year"].AsInt32),
                        new VehicleColor(vehicle["color"].AsString),
                        new VehicleKm(vehicle["km"].AsInt32),
                        new VehicleOwnerDni(vehicle["ownerDni"].AsInt32),
                        new VehicleOwnerName(vehicle["ownerName"].AsString)
                    ),
                    new Policy(
                        new PolicyId(policy["_id"].AsString),
                        new PolicyName(policy["name"].AsString),
                        new PolicyMonetaryCoverage(policy["monetaryCoverage"].AsDecimal),
                        new PolicyKmCoverage(policy["kmCoverage"].AsDecimal),
                        new PolicyBaseKmPrice(policy["baseKmPrice"].AsDecimal)
                    )
                );
                if (bsonContract["isActive"].AsBoolean)
                {
                    contract.ChangeStatus();
                }
                return _Optional<Contract>.Of(contract);
            }
            catch (Exception)
            {
                return _Optional<Contract>.Empty();
            }
        }
        public async Task<_Optional<Contract>> GetContractById(ContractId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonContract = await _contractCollection.Find(filter).FirstOrDefaultAsync();
                if (bsonContract == null)
                {
                    return _Optional<Contract>.Empty();
                }
                var vehicle = bsonContract["vehicle"].AsBsonDocument;
                var policy = bsonContract["policy"].AsBsonDocument;
                var contract = Contract.Create(
                    new ContractId(bsonContract["_id"].AsString),
                    new NumberContract(bsonContract["numberContract"].AsInt32),
                    new ContractExpitionDate(((DateTime)bsonContract["expirationDate"])),
                    new Vehicle(
                        new VehicleId(vehicle["_id"].AsString),
                        new VehicleLicensePlate(vehicle["licensePlate"].AsString),
                        new VehicleBrand(vehicle["brand"].AsString),
                        new VehicleModel(vehicle["model"].AsString),
                        new VehicleYear(vehicle["year"].AsInt32),
                        new VehicleColor(vehicle["color"].AsString),
                        new VehicleKm(vehicle["km"].AsInt32),
                        new VehicleOwnerDni(vehicle["ownerDni"].AsInt32),
                        new VehicleOwnerName(vehicle["ownerName"].AsString)
                    ),
                    new Policy(
                        new PolicyId(policy["_id"].AsString),
                        new PolicyName(policy["name"].AsString),
                        new PolicyMonetaryCoverage(policy["monetaryCoverage"].AsDecimal),
                        new PolicyKmCoverage(policy["kmCoverage"].AsDecimal),
                        new PolicyBaseKmPrice(policy["baseKmPrice"].AsDecimal)
                    )
                );
                if (bsonContract["isActive"].AsBoolean)
                {
                    contract.ChangeStatus();
                }
                return _Optional<Contract>.Of(contract);
            }
            catch (Exception)
            {
                return _Optional<Contract>.Empty();
            }
        }

        public async Task<ContractId> UpdateContract(Contract contract)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", contract.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("expirationDate", contract.GetContractExpirationDate())
                .Set("policy", new BsonDocument
                {
                    {"_id", contract.GetPolicy().GetId()},
                    {"name", contract.GetPolicy().GetName()},
                    {"monetaryCoverage", contract.GetPolicy().GetMonetaryCoverage()},
                    {"kmCoverage", contract.GetPolicy().GetkmCoverage()},
                    {"baseKmPrice", contract.GetPolicy().GetBaseKmPrice()}
                })
                .Set("updatedAt", DateTime.Now);
            await _contractCollection.UpdateOneAsync(filter, update);
            return new ContractId(contract.GetId());
        }
        public async Task<ContractId> ToggleActivityContractById(ContractId id)
        {
            var contract = await GetContractById(id);
            if (!contract.HasValue())
            {
                throw new ContractNotFoundException();
            }
            var contractToUpdate = contract.Unwrap();
            contractToUpdate.ChangeStatus();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
            var update = Builders<BsonDocument>.Update.
                Set("isActive", contractToUpdate.IsActive())
                .Set("updatedAt", DateTime.Now);
            await _contractCollection.UpdateOneAsync(filter, update);
            return id;
        }

        public async Task<_Optional<Vehicle>> GetContractVehicle(ContractId id)
        {
            var contract = await GetContractById(id);
            if (!contract.HasValue())
            {
                return _Optional<Vehicle>.Empty();
            }
            return _Optional<Vehicle>.Of(contract.Unwrap().GetVehicle());
        }

        public async Task<_Optional<Policy>> GetContractPolicy(ContractId id)
        {
            var contract = await GetContractById(id);
            if (!contract.HasValue())
            {
                return _Optional<Policy>.Empty();
            }
            return _Optional<Policy>.Of(contract.Unwrap().GetPolicy());
        }

    }
}
