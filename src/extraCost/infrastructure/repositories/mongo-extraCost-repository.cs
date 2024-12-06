using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.extracost.domain;

using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;

using OrdersMicroservice.src.extracost.application.commands.update_extracost.types;

using OrdersMicroservice.src.extracost.application.repositories.dto;
using OrdersMicroservice.src.extracost.application.repositories.exceptions;

using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.extracost.infrastructure.models;
using OrdersMicroservice.src.extracost.application.commands.update_extracos.types;
using OrdersMicroservice.src.extracost.application.repositories;


namespace OrdersMicroservice.src.extracost.infrastructure.repositories
{
    public class MongoExtraCostRepository : IExtraCostRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> extraCostCollection;

        public MongoExtraCostRepository()
        {
            extraCostCollection = _config.db.GetCollection<BsonDocument>("extraCost");
        }

        public async Task<ExtraCost> SaveExtraCost(ExtraCost extraCost)
        {
            var mongoExtraCost = new MongoExtraCost
            {
                Id = extraCost.GetExtraCostId(),
                Description = extraCost.GetExtraCostDescription(),
                Price = extraCost.GetExtraCostPrice(),

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoExtraCost.Id},
                {"description", mongoExtraCost.Description},
                {"price", mongoExtraCost.Price},
             

                {"createdAt", mongoExtraCost.CreatedAt },
                {"updatedAt", mongoExtraCost.UpdatedAt }
            };

            await extraCostCollection.InsertOneAsync(bsonDocument);

            var savedExtraCost = ExtraCost.Create(
                new ExtraCostId(mongoExtraCost.Id), 
                new ExtraCostDescription(mongoExtraCost.Description), 
                new ExtraCostPrice(mongoExtraCost.Price)
            ); 
                


            return savedExtraCost;
        }
        public async Task<_Optional<List<ExtraCost>>> GetAllExtraCosts()
        {
            
            var extraCosts = await extraCostCollection.Find(new BsonDocument()).ToListAsync();
            var extraCostList = extraCosts.Select(extraCost => ExtraCost.Create(
                new ExtraCostId(extraCost["_id"].AsString),
                new ExtraCostDescription(extraCost["description"].AsString),
                new ExtraCostPrice(extraCost["price"].AsDecimal)

            )).ToList();

            if (extraCostList.Count == 0)
            {
                return _Optional<List<ExtraCost>>.Empty();
            }

            return _Optional<List<ExtraCost>>.Of(extraCostList);
        }

        public async Task<_Optional<ExtraCost>> GetExtraCostById(ExtraCostId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetExtraCostId());
                var bsonUser = await extraCostCollection.Find(filter).FirstOrDefaultAsync();
        
                if (bsonUser == null)
                {
                    return _Optional<ExtraCost>.Empty();
                }

                var extraCost = ExtraCost.Create(
                    new ExtraCostId(bsonUser["_id"].AsString),
                    new ExtraCostDescription(bsonUser["description"].AsString),
                    new ExtraCostPrice(bsonUser["price"].AsDecimal)
                  
 
                );



                return _Optional<ExtraCost>.Of(extraCost);
            }
            catch (Exception ex)
            {
        
                return _Optional<ExtraCost>.Empty();
            }
        }

        public async Task<ExtraCostId> UpdateExtraCostById(UpdateExtraCostByIdCommand command)
        {
            var user = await GetExtraCostById(new ExtraCostId(command.Id));
            if (!user.HasValue())
            {
                throw new ExtraCostNotFoundException();
            }
            
            var filter = Builders<BsonDocument>.Filter.Eq("_id", command.Id);

            var update = Builders<BsonDocument>.Update
                .Set("updatedAt", DateTime.Now);

            if (command.Description != null)
            {
                update = update.Set("description", command.Description);
            }
            if (command.Price!= null)
            {
                update = update.Set("price", command.Price);
            }


            await extraCostCollection.UpdateOneAsync(filter, update);

            return new ExtraCostId(command.Id);
        }

        public async Task<ExtraCostId> ToggleActivityExtraCostById(ExtraCostId id)
        {
            var extraCost = await GetExtraCostById(id);
            if (!extraCost.HasValue())
            {
                throw new ExtraCostNotFoundException();
            }

            

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetExtraCostId());
            var update = Builders<BsonDocument>.Update
                .Set("updatedAt", DateTime.Now);

            await extraCostCollection.UpdateOneAsync(filter, update);

            return id;
        }

    }
}
