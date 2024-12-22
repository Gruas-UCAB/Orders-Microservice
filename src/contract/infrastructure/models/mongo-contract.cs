using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        [BsonElement("polizaId"), BsonRepresentation(BsonType.String)]
        public required string PolizaId { get; set; }

        [BsonElement("vehicleId"), BsonRepresentation(BsonType.String)]
        public required string VehicleId { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
