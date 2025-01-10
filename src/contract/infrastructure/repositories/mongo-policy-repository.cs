using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.infrastructure.models;
using OrdersMicroservice.src.contract.domain.entities.policy.value_objects;
using OrdersMicroservice.src.contract.application.repositories.exceptions;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.application.commands.update_contract.types;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OrdersMicroservice.src.contract.application.repositories.dto;

namespace OrdersMicroservice.src.contract.infrastructure.repositories
{
    public class MongoPolicyRepository : IPolicyRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> _policyCollection;

        public MongoPolicyRepository()
        {
            _policyCollection = _config.db.GetCollection<BsonDocument>("policies");
        }

        public async Task<Policy> SavePolicy(Policy policy)
        {
            var mongoPolicy = new MongoPolicy
            {
                Id = policy.GetId(),
                Name = policy.GetName(),
                MonetaryCoverage = policy.GetMonetaryCoverage(),
                KmCoverage = policy.GetkmCoverage(),
                BaseKmPrice = policy.GetBaseKmPrice(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoPolicy.Id},
                {"name", mongoPolicy.Name},
                {"monetaryCoverage", mongoPolicy.MonetaryCoverage},
                {"kmCoverage", mongoPolicy.KmCoverage},
                {"baseKmPrice", mongoPolicy.BaseKmPrice},
                {"createdAt", mongoPolicy.CreatedAt },
                {"updatedAt", mongoPolicy.UpdatedAt }
            };

            await _policyCollection.InsertOneAsync(bsonDocument);

            var savedPolicy = new Policy(
                new PolicyId(mongoPolicy.Id),
                new PolicyName(mongoPolicy.Name),
                new PolicyMonetaryCoverage(mongoPolicy.MonetaryCoverage),
                new PolicyKmCoverage(mongoPolicy.KmCoverage),
                new PolicyBaseKmPrice(mongoPolicy.BaseKmPrice)
                );

            return savedPolicy;
        }
        public async Task<_Optional<List<Policy>>> GetAllPolicies(GetAllPolicesDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;

            var policies = await _policyCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
            var policiesList = new List<Policy>();
            foreach (var bsonPolicy in policies)
            {
                var policy = new Policy(
                        new PolicyId(bsonPolicy["_id"].AsString),
                        new PolicyName(bsonPolicy["name"].AsString),
                        new PolicyMonetaryCoverage(bsonPolicy["monetaryCoverage"].AsDecimal),
                        new PolicyKmCoverage(bsonPolicy["kmCoverage"].AsDecimal),
                        new PolicyBaseKmPrice(bsonPolicy["baseKmPrice"].AsDecimal)
                    );
                policiesList.Add(policy);
            }
            if (policiesList.Count == 0)
            {
                return _Optional<List<Policy>>.Empty();
            }
            return _Optional<List<Policy>>.Of(policiesList);
        }
        public async Task<_Optional<Policy>> GetPolicyById(PolicyId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonPolicy = await _policyCollection.Find(filter).FirstOrDefaultAsync();
                if (bsonPolicy == null)
                {
                    return _Optional<Policy>.Empty();
                }
                var policy = new Policy(
                    new PolicyId(bsonPolicy["_id"].AsString),
                    new PolicyName(bsonPolicy["name"].AsString),
                    new PolicyMonetaryCoverage(bsonPolicy["monetaryCoverage"].AsDecimal),
                    new PolicyKmCoverage(bsonPolicy["kmCoverage"].AsDecimal),
                    new PolicyBaseKmPrice(bsonPolicy["baseKmPrice"].AsDecimal)
                    );
                return _Optional<Policy>.Of(policy);
            }
            catch (Exception)
            {
                return _Optional<Policy>.Empty();
            }
        }
        public async Task<PolicyId> UpdatePolicy(Policy policy)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", policy.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("name", policy.GetName())
                .Set("monetaryCoverage", policy.GetMonetaryCoverage())
                .Set("kmCoverage", policy.GetkmCoverage())
                .Set("baseKmPrice", policy.GetBaseKmPrice())
                .Set("updatedAt", DateTime.Now);
            await _policyCollection.UpdateOneAsync(filter, update);
            return new PolicyId(policy.GetId());
        }
    }
}
