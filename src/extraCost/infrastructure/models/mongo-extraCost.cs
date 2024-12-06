using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrdersMicroservice.src.extracost.infrastructure.models
{
    public class MongoExtraCost
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public required string Description { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.String)]
        public required decimal Price { get; set; }



        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
