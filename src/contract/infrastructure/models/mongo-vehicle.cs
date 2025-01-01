using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrdersMicroservice.src.contract.infrastructure.models
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
        public required int Km { get; set; }
        [BsonElement("ownerDni")]
        public required int OwnerDni { get; set; }
        [BsonElement("ownerName")]
        public required string OwnerName { get; set; }
    }
}
