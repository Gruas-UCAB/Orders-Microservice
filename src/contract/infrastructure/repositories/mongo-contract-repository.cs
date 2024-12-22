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
using OrdersMicroservice.src.contract.infrastructure.models;
using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.vehicle.domain.value_objects;



namespace contractsMicroservice.src.contract.infrastructure.repositories
{
    public class MongoContractRepository : IContractRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> contractCollection;

        public MongoContractRepository()
        {
            contractCollection = _config.db.GetCollection<BsonDocument>("contracts");
        }

        public async Task<Contract> SaveContract(Contract contract)
        {
            var mongoContract = new MongoContract
            {
                Id = contract.GetContractId(),
                NumberContract = contract.GetContractNumber(),
                ExpirationDate = contract.GetContractExpirationDate(),
                

                VehicleId = contract.GetVehicleId(),
                PolizaId = contract.GetPolicyId(),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoContract.Id},
                {"numberContract", mongoContract.NumberContract},
                {"expirationDate", mongoContract.ExpirationDate},
                {"vehicleId", mongoContract.VehicleId},
                {"polizaId", mongoContract.PolizaId},
                {"isActive", mongoContract.IsActive},
                {"createdAt", mongoContract.CreatedAt },
                {"updatedAt", mongoContract.UpdatedAt }
            };
            await contractCollection.InsertOneAsync(bsonDocument);

            var savedcontract = Contract.Create(
                new ContractId(mongoContract.Id), 
                new NumberContract(mongoContract.NumberContract), 
                new ContractExpitionDate(mongoContract.ExpirationDate), 
                new VehicleId(mongoContract.VehicleId),
                new PolicyId(mongoContract.PolizaId)
                );
               

            return savedcontract;
        }
        public async Task<_Optional<List<Contract>>> GetAllContracts(GetAllContractsDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var contracts = await contractCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
          
            var contractList = new List<Contract>();

            foreach (var bsonContract in contracts)
            {
                var contract = Contract.Create(
                new ContractId(bsonContract["_id"].AsString),
                new NumberContract(bsonContract["numberContract"].AsDecimal),
                new ContractExpitionDate(bsonContract["expirationDate"].AsBsonDateTime.AsLocalTime),
                new VehicleId(bsonContract["vehicleId"].AsString),
                new PolicyId(bsonContract["polizaId"].AsString)
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
                var bsonContract = await contractCollection.Find(filter).FirstOrDefaultAsync();
        
                if (bsonContract == null)
                {
                    return _Optional<Contract>.Empty();
                }

                var contract = Contract.Create(
                new ContractId(bsonContract["_id"].AsString),
                new NumberContract(bsonContract["numberContract"].AsDecimal),
                new ContractExpitionDate(bsonContract["expirationDate"].AsBsonDateTime.AsLocalTime),
                new VehicleId(bsonContract["vehicleId"].AsString),
                new PolicyId(bsonContract["polizaId"].AsString)
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
                update = update.Set("phone", command.ExpirationDate);
            }

            await contractCollection.UpdateOneAsync(filter, update);

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

            await contractCollection.UpdateOneAsync(filter, update);

            return id;
        }

    }
}
