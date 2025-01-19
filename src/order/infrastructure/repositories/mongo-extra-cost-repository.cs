using MongoDB.Bson;
using MongoDB.Driver;
using OrdersMicroservice.core.Common;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.extracost.domain.value_objects;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.application.repositories.dto;
using OrdersMicroservice.src.order.domain.entities.extraCost;
using OrdersMicroservice.src.order.infrastructure.models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrdersMicroservice.src.order.infrastructure.repositories
{
    public class MongoExtraCostRepository : IExtraCostRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> _extraCostCollection;

        public MongoExtraCostRepository()
        {
            _extraCostCollection = _config.db.GetCollection<BsonDocument>("extraCosts");
        }
        public async Task<ExtraCost> SaveExtraCost(ExtraCost extraCost)
        {
            var mongoExtraCost = new MongoExtraCost
            {
                Id = extraCost.GetId(),
                Description = extraCost.GetDescription(),
                Price = extraCost.GetPrice(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoExtraCost.Id},
                {"description", mongoExtraCost.Description},
                {"defaultPrice", mongoExtraCost.Price},
                {"createdAt", mongoExtraCost.CreatedAt },
                {"updatedAt", mongoExtraCost.UpdatedAt }
            };

            await _extraCostCollection.InsertOneAsync(bsonDocument);

            var savedExtraCost = new ExtraCost(
                new ExtraCostId(mongoExtraCost.Id),
                new ExtraCostDescription(mongoExtraCost.Description),
                new ExtraCostPrice(mongoExtraCost.Price)
                );

            return savedExtraCost;
        }
        public async Task<_Optional<List<ExtraCost>>> GetAllExtraCosts(GetAllExtraCostsDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var extraCosts = await _extraCostCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();
            var extraCostList = new List<ExtraCost>();
            foreach (var extraCost in extraCosts)
            {
                extraCostList.Add(new ExtraCost(
                    new ExtraCostId(extraCost["_id"].AsString),
                    new ExtraCostDescription(extraCost["description"].AsString),
                    new ExtraCostPrice(extraCost["defaultPrice"].AsDecimal)
                ));
            }
            if (extraCostList.Count == 0)
            {
                return _Optional<List<ExtraCost>>.Empty();
            }
            else
            {
                return _Optional<List<ExtraCost>>.Of(extraCostList);
            }
        }
        public async  Task<_Optional<ExtraCost>> GetExtraCostById(ExtraCostId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetExtraCostId());
                var bsonExtraCost = await _extraCostCollection.Find(filter).FirstOrDefaultAsync();
                if (bsonExtraCost == null)
                {
                    return _Optional<ExtraCost>.Empty();
                }
                return _Optional<ExtraCost>.Of(new ExtraCost(
                    new ExtraCostId(bsonExtraCost["_id"].AsString),
                    new ExtraCostDescription(bsonExtraCost["description"].AsString),
                    new ExtraCostPrice(bsonExtraCost["defaultPrice"].AsDecimal)
                ));
            }
            catch (Exception)
            {
                return _Optional<ExtraCost>.Empty();
            }
        }
        public async Task<ExtraCost> UpdateExtraCost(ExtraCost extraCost)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", extraCost.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("description", extraCost.GetDescription())
                .Set("defaultPrice", extraCost.GetPrice())
                .Set("updatedAt", DateTime.Now);
            await _extraCostCollection.UpdateOneAsync(filter, update);
            return extraCost;
        }
    }
}
