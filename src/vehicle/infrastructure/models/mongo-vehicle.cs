using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrdersMicroservice.src.vehicle.infrastructure.models
{
    public class MongoVehicle
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("licensePlate")]
        public required string LicensePlate { get; set; }

        [BsonElement("brand")]
        public required string Brand { get; set; }

        [BsonElement("model")]
        public required string Model { get; set; }

        [BsonElement("year")]
        public required int Year { get; set; }

        [BsonElement("color")]
        public required string Color { get; set; }

        [BsonElement("km")]
        public required double Km { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.DateTime)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updateAt"), BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
