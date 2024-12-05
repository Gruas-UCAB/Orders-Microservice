using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.extracost.domain;

using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;

using OrdersMicroservice.src.policy.application.commands.update_policy.types;

using OrdersMicroservice.src.policy.application.repositories.dto;
using OrdersMicroservice.src.policy.application.repositories.exceptions;

using OrdersMicroservice.src.policy.domain.value_objects;
using OrdersMicroservice.src.policy.infrastructure.models;
using OrdersMicroservice.src.policy.domain;

namespace OrdersMicroservice.src.policy.infrastructure.repositories
{
    public class MongoPolicyRepository : IPolicyRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> policyCollection;

        public MongoPolicyRepository()
        {
            policyCollection = _config.db.GetCollection<BsonDocument>("policy");
        }

        public async Task<Policy> SavePolicy(Policy policy)
        {
            var mongoPolicy = new MongoPolicy
            {
                Id = policy.GetId(),
                Name = policy.GetName(),
                MonetaryCoverage = policy.GetMonetaryCoverage(),
                KmCoverage = policy.GetkmCoverage(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoPolicy.Id},
                {"name", mongoPolicy.Name},
                {"monetaryCoverage", mongoPolicy.MonetaryCoverage},
                {"kmCoverage", mongoPolicy.KmCoverage},

                {"createdAt", mongoPolicy.CreatedAt },
                {"updatedAt", mongoPolicy.UpdatedAt }
            };

            await policyCollection.InsertOneAsync(bsonDocument);

            var savedPolicy = Policy.Create(
                new PolicyId(mongoPolicy.Id), 
                new PolicyName(mongoPolicy.Name), 
                new PolicyMonetaryCoverage(mongoPolicy.MonetaryCoverage), 
                new PolicyKmCoverage(mongoPolicy.KmCoverage));


            return savedPolicy;
        }
        public async Task<_Optional<List<Policy>>> GetAllPolicies()
        {
            
            var policies = await policyCollection.Find(new BsonDocument()).ToListAsync();
            var policyList = policies.Select(policy => Policy.Create(
                new PolicyId(policy["_id"].AsString),
                new PolicyName(policy["name"].AsString),
                new PolicyMonetaryCoverage(policy["monetaryCoverage"].AsDecimal),
                new PolicyKmCoverage(policy["kmCoverage"].AsDecimal)

            )).ToList();

            if (policyList.Count == 0)
            {
                return _Optional<List<Policy>>.Empty();
            }

            return _Optional<List<Policy>>.Of(policyList);
        }

        public async Task<_Optional<Policy>> GetPolicyById(PolicyId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonUser = await policyCollection.Find(filter).FirstOrDefaultAsync();
        
                if (bsonUser == null)
                {
                    return _Optional<Policy>.Empty();
                }

                var policy = Policy.Create(
                    new PolicyId(bsonUser["_id"].AsString),
                    new PolicyName(bsonUser["name"].AsString),
                    new PolicyMonetaryCoverage(bsonUser["monetaryCoverage"].AsDecimal),
                    new PolicyKmCoverage(bsonUser["kmCoverage"].AsDecimal)
 
                );



                return _Optional<Policy>.Of(policy);
            }
            catch (Exception ex)
            {
        
                return _Optional<Policy>.Empty();
            }
        }

        public async Task<PolicyId> UpdatePolicyById(UpdatePolicyByIdCommand command)
        {
            var user = await GetPolicyById(new PolicyId(command.Id));
            if (!user.HasValue())
            {
                throw new PolicyNotFoundException();
            }
            
            var filter = Builders<BsonDocument>.Filter.Eq("_id", command.Id);

            var update = Builders<BsonDocument>.Update
                .Set("updatedAt", DateTime.Now);

            if (command.Name != null)
            {
                update = update.Set("name", command.Name);
            }
            if (command.MonetaryCoverage != null)
            {
                update = update.Set("monetaryCoverage", command.MonetaryCoverage);
            }
            if (command.KmCoverage != null)
            {
                update = update.Set("kmCoverage", command.KmCoverage);
            }

            await policyCollection.UpdateOneAsync(filter, update);

            return new PolicyId(command.Id);
        }

        public async Task<PolicyId> ToggleActivityPolicyById(PolicyId id)
        {
            var policy = await GetPolicyById(id);
            if (!policy.HasValue())
            {
                throw new PolicyNotFoundException();
            }

            

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("updatedAt", DateTime.Now);

            await policyCollection.UpdateOneAsync(filter, update);

            return id;
        }

    }
}
