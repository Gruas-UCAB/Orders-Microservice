using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrdersMicroservice.src.contract.domain.entities.policy;
using OrdersMicroservice.src.contract.domain.entities.vehicle;


namespace OrdersMicroservice.src.contract.infrastructure.models
{
    public class MongoContract
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("numberContract"), BsonRepresentation(BsonType.String)]
        public required decimal NumberContract { get; set; }

        [BsonElement("expirationDate"), BsonRepresentation(BsonType.String)]
        public required DateTime ExpirationDate { get; set; }

        [BsonElement("policy") /*, BsonRepresentation(BsonType.String)*/]
        public required Policy Policy { get; set; }

        [BsonElement("vehicle")/*, BsonRepresentation(BsonType.String)*/]
        public required Vehicle Vehicle { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
