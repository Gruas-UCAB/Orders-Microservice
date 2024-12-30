using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;

using OrdersMicroservice.src.contract.application.commands.update_contract.types;
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
using ProvidersMicroservice.src.contract.application.repositories.dto;
using Microsoft.AspNetCore.Mvc;




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
                Id = contract.GetContractId(),
                NumberContract = contract.GetContractNumber(),
                ExpirationDate = contract.GetContractExpirationDate(),
                Vehicle = contract.GetVehicle(),/*?*/
                Policy = contract.GetPolicy(),/*?*/
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoContract.Id},
                {"numberContract", mongoContract.NumberContract},
                {"expirationDate", mongoContract.ExpirationDate},
                {"vehicle", mongoContract.Vehicle.ToBsonDocument()},
                {"policy", mongoContract.Policy.ToBsonDocument()},
                {"isActive", mongoContract.IsActive},
                {"createdAt", mongoContract.CreatedAt },
                {"updatedAt", mongoContract.UpdatedAt }
            };
            await _contractCollection.InsertOneAsync(bsonDocument);

            var savedcontract = Contract.Create(
                new ContractId(mongoContract.Id), 
                new NumberContract(mongoContract.NumberContract), 
                new ContractExpitionDate(mongoContract.ExpirationDate),
                mongoContract.Vehicle/*?*/
                /*mongoContract.Policy/*?*/
                );
               

            return savedcontract;
        }
        public async Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var contracts = await _contractCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
          
            var contractList = new List<Contract>();

            foreach (var bsonContract in contracts)
            {
                var bsonVehicles = bsonContract["vehicle"].AsBsonArray;

                var vehicle = new Vehicle(
                    new VehicleId(bsonContract["vehicleId"].AsString),
                    new VehicleLicensePlate(bsonContract["licensePlate"].AsString),
                    new VehicleBrand(bsonContract["brand"].AsString),
                    new VehicleModel(bsonContract["model"].AsString),
                    new VehicleYear(bsonContract["year"].AsInt32),
                    new VehicleColor(bsonContract["color"].AsString),
                    new VehicleKm(bsonContract["km"].AsDouble)
                );


               /*
                var bsonPolices = bsonContract["policy"].AsBsonArray;
                var policy = new Policy(
                    new PolicyId(bsonContract["policyId"].AsString),
                    new PolicyName(bsonContract["name"].AsString),
                    new PolicyMonetaryCoverage(bsonContract["monetaryCoverage"].AsDecimal),
                    new PolicyKmCoverage(bsonContract["kmCoverage"].AsDecimal)
                );
                */
                var contract = Contract.Create(
                new ContractId(bsonContract["_id"].AsString),
                new NumberContract(bsonContract["numberContract"].AsDecimal),
                new ContractExpitionDate(bsonContract["expirationDate"].AsBsonDateTime.AsLocalTime),
                vehicle
                /*policy*/
            );

                if (!bsonContract["isActive"].AsBoolean)
                {
                    contract.ChangeStatus();
                }
                contractList.Add(contract);
                
            }

            if (contractList.Count == 0)
            {
                return _Optional<List<Contract>>.Empty();
            }
            return _Optional<List<Contract>>.Of(contractList);
        }

        public async Task<_Optional<Contract>> GetContractById(ContractId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetContractId());
                var bsonContract = await _contractCollection.Find(filter).FirstOrDefaultAsync();
        
                if (bsonContract == null)
                {
                    return _Optional<Contract>.Empty();
                }

                var bsonVehicles = bsonContract["vehicle"].AsBsonArray;/*?*/
                var vehicle = new Vehicle(
                    new VehicleId(bsonVehicles["vehicleId"].AsString),
                    new VehicleLicensePlate(bsonVehicles["licensePlate"].AsString),
                    new VehicleBrand(bsonVehicles["brand"].AsString),
                    new VehicleModel(bsonVehicles["model"].AsString),
                    new VehicleYear(bsonVehicles["year"].AsInt32),
                    new VehicleColor(bsonVehicles["color"].AsString),
                    new VehicleKm(bsonVehicles["km"].AsDouble)
                );

                var bsonPolices = bsonContract["policy"].AsBsonArray;/*?*/
                var policy = new Policy(
                    new PolicyId(bsonContract["policyId"].AsString),
                    new PolicyName(bsonContract["name"].AsString),
                    new PolicyMonetaryCoverage(bsonContract["monetaryCoverage"].AsDecimal),
                    new PolicyKmCoverage(bsonContract["kmCoverage"].AsDecimal)
                );

                var contract = Contract.Create(
                new ContractId(bsonContract["_id"].AsString),
                new NumberContract(bsonContract["numberContract"].AsDecimal),
                new ContractExpitionDate(bsonContract["expirationDate"].AsBsonDateTime.AsLocalTime),
                vehicle
                /*policy*/
            );

                if (!bsonContract["isActive"].AsBoolean)
                {
                    contract.ChangeStatus();
                }

                return _Optional<Contract>.Of(contract);
            }
            catch (Exception ex)
            {
        
                return _Optional<Contract>.Empty();
            }
        }

        public async Task<ContractId> UpdateContractById(UpdateContractByIdCommand command)
        {
            var contract = await GetContractById(new ContractId(command.Id));
            if (!contract.HasValue())
            {
                throw new ContractNotFoundException();
            }
            
            var filter = Builders<BsonDocument>.Filter.Eq("_id", command.Id);

            var update = Builders<BsonDocument>.Update
                .Set("updatedAt", DateTime.Now);

            if (command.NumberContract != null)
            {
                update = update.Set("numberContract", command.NumberContract);
            }
            if (command.ExpirationDate != null)
            {
                update = update.Set("expirationDate", command.ExpirationDate);
            }

            await _contractCollection.UpdateOneAsync(filter, update);

            return new ContractId(command.Id);
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

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetContractId());
            var update = Builders<BsonDocument>.Update.
                Set("isActive", contractToUpdate.IsActive())
                .Set("updatedAt", DateTime.Now);

            await _contractCollection.UpdateOneAsync(filter, update);

            return id;
        }
        //----------------------------------------------------------------------------------------------------------------//

        //vehicle
        public async Task<Vehicle> SaveVehicle(SaveVehicleDto data)
        {
            var contractFind = await GetContractById(data.contractId);
            if (!contractFind.HasValue())
            {
                throw new ContractNotFoundException();
            }

            var contract = contractFind.Unwrap();
            var vehicleAdded = contract.AddVehicle(
                    new VehicleId(data.vehicle.GetId()),
                    new VehicleLicensePlate(data.vehicle.GetLicensePlate()),
                    new VehicleBrand(data.vehicle.GetBrand()),
                    new VehicleModel(data.vehicle.GetModel()),
                    new VehicleYear(data.vehicle.GetYear()),
                    new VehicleColor(data.vehicle.GetColor()),
                    new VehicleKm(data.vehicle.GetKm())
                );

            var vehicleBsonArray = new BsonArray();/*?*/
           /* foreach (var c in contract.GetVehicle())
            {*/
                var vehicle = contract.GetVehicle();
                var vehicleBson = new BsonDocument
                {
                    { "id", vehicle.GetId() },
                    { "licensePlate", vehicle.GetLicensePlate() },
                    { "brand", vehicle.GetBrand() },
                    { "model", vehicle.GetModel() },
                    { "year", vehicle.GetYear() },
                    { "color", vehicle.GetColor() },
                    { "km", vehicle.GetKm() }
                 
                };
                vehicleBsonArray.Add(vehicleBson);
           /* }*/

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.contractId.GetContractId());
            var update = Builders<BsonDocument>.Update
                .Set("vehicle", vehicleBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _contractCollection.UpdateOneAsync(filter, update);
            return vehicleAdded;
        }

        public async Task<_Optional<Vehicle>> GetVehicleById(GetVehicleByIdDto data)
        {
            var contractFind = await GetContractById(new ContractId(data.contractId));
            if (!contractFind.HasValue())
            {
                throw new ContractNotFoundException();
            }
            var contract = contractFind.Unwrap();
            var vehicle = contract.GetVehicle();/*.Find(c => c.Equals(new VehicleId(data.vehicleId))); no ?*/
            if (vehicle == null)
            {
                return _Optional<Vehicle>.Empty();
            }
            return _Optional<Vehicle>.Of(vehicle);
        }


        public async Task<_Optional<List<Vehicle>>> GetAllVehicles(GetAllVehiclesDto data, ContractId contractId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", contractId.GetContractId());
            var bsonContract = await _contractCollection.Find(filter).FirstOrDefaultAsync();

            if (bsonContract == null)
            {
                return _Optional<List<Vehicle>>.Empty();
            }

            var bsonVehicles = bsonContract["vehicle"].AsBsonArray;
            var vehicles = new List<Vehicle>();
            foreach (var bsonVehicle in bsonVehicles)
            {
                var vehicle = new Vehicle(
                    new VehicleId(bsonVehicle["_id"].AsString),
                    new VehicleLicensePlate(bsonVehicle["licensePlate"].AsString),
                    new VehicleBrand(bsonVehicle["brand"].AsString),
                    new VehicleModel(bsonVehicle["model"].AsString),
                    new VehicleYear(bsonVehicle["year"].AsInt32),
                    new VehicleColor(bsonVehicle["color"].AsString),
                    new VehicleKm(bsonVehicle["km"].AsDouble)
                );


                vehicles.Add(vehicle);
            }
            return (vehicles.Count == 0) ? _Optional<List<Vehicle>>.Empty() : _Optional<List<Vehicle>>.Of(vehicles);
        }


        public async Task<VehicleId> ToggleActivityVehicleById(ToggleActivityVehicleByIdDto data)
        {
            var vehicle = await GetVehicleById(new GetVehicleByIdDto(data.contractId.GetContractId(), data.vehicleId.GetId()));
            if (!vehicle.HasValue())
            {
                throw new VehicleNotFoundException();
            }

            var vehicleToUpdate = vehicle.Unwrap();
           /* vehicleToUpdate.ChangeStatus();*/
            var vehiclesFind = await GetAllVehicles(new GetAllVehiclesDto(), (data.contractId));
            var vehicles = vehiclesFind.Unwrap();
            vehicles.ForEach(c =>
            {
                if (c.GetId() == vehicleToUpdate.GetId())
                {
                    /*c.ChangeStatus;*/
                }
            });

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.contractId.GetContractId());
            var update = Builders<BsonDocument>.Update
                .Set("vehicles", vehicles)
                .Set("updatedAt", DateTime.Now);

            await _contractCollection.UpdateOneAsync(filter, update);
            return data.vehicleId;
        }


        //policy
        public async Task<Policy> SavePolicy(SavePolicyDto data)
        {
            var contractFind = await GetContractById(data.contractId);
            if (!contractFind.HasValue())
            {
                throw new ContractNotFoundException();
            }

            var contract = contractFind.Unwrap();
            var policyAdded = contract.AddPolicy(
                    new PolicyId(data.policy.GetId()),
                    new PolicyName(data.policy.GetName()),
                    new PolicyMonetaryCoverage(data.policy.GetMonetaryCoverage()),
                    new PolicyKmCoverage(data.policy.GetkmCoverage())
                );

            var policysBsonArray = new BsonArray();
            /* foreach (var c in contract.GetPolicy())
             {*/
                 var policy = contract.GetPolicy();
                 var policyBson = new BsonDocument
                    {
                    { "id", policy.GetId() },
                    { "name", policy.GetName() },
                    { "monetaryCoverage", policy.GetMonetaryCoverage()},
                    { "kmCoverage", policy.GetkmCoverage()},
                };
                policysBsonArray.Add(policyBson);
           /* }*/

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.contractId.GetContractId());
            var update = Builders<BsonDocument>.Update
                .Set("policy", policysBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _contractCollection.UpdateOneAsync(filter, update);
            return policyAdded;
        }

        public async Task<_Optional<Policy>> GetPolicyById(GetPolicyByIdDto data)
        {
            var contractFind = await GetContractById(new ContractId(data.contractId));
            if (!contractFind.HasValue())
            {
                throw new PolicyNotFoundException();
            }
            var contract = contractFind.Unwrap();
            var policy = contract.GetPolicy();
            if (policy == null)
            {
                return _Optional<Policy>.Empty();
            }
            return _Optional<Policy>.Of(policy);
        }



        public async Task<_Optional<List<Policy>>> GetAllPolices(GetAllPolicesDto data, ContractId contractId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", contractId.GetContractId());
            var bsonContract = await _contractCollection.Find(filter).FirstOrDefaultAsync();

            if ( bsonContract == null)
            {
                return _Optional<List<Policy>>.Empty();
            }
            var bsonPolices = bsonContract["policy"].AsBsonArray;
            var polices = new List<Policy>();
            foreach (var bsonPolicy in bsonPolices)
            {
                var policy = new Policy(
                    new PolicyId(bsonPolicy["_id"].AsString),
                    new PolicyName(bsonPolicy["name"].AsString),
                    new PolicyMonetaryCoverage(bsonPolicy["monetaryCoverage"].AsDecimal),
                    new PolicyKmCoverage(bsonPolicy["kmCoverage"].AsDecimal)
                );

 
                polices.Add(policy);
            }

            return (polices.Count == 0) ? _Optional<List<Policy>>.Empty() : _Optional<List<Policy>>.Of(polices);
        }


        public async Task<PolicyId> ToggleActivityPolicyById(ToggleActivityPolicyByIdDto data)
        {
            var policy = await GetPolicyById(new GetPolicyByIdDto(data.contractId.GetContractId(), data.policyId.GetId()));
            if (!policy.HasValue())
            {
                throw new PolicyNotFoundException();
            }

            var policyToUpdate = policy.Unwrap();
            /*policyToUpdate.ChangeStatus();*/
            var policesFind = await GetAllPolices(new GetAllPolicesDto(), (data.contractId));
            var polices = policesFind.Unwrap();
            polices.ForEach(c =>
            {
                if (c.GetId() == policyToUpdate.GetId())
                {
                    /*c.ChangeStatus();*/
                }
            });

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.contractId.GetContractId());
            var update = Builders<BsonDocument>.Update
                .Set("polizy", polices)
                .Set("updatedAt", DateTime.Now);

            await _contractCollection.UpdateOneAsync(filter, update);
            return data.policyId;
        }

    }
}
