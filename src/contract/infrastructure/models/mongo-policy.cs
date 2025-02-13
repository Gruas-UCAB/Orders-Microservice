﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrdersMicroservice.src.contract.infrastructure.models
{
    public class MongoPolicy
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public required string Name { get; set; }

        [BsonElement("monetaryCoverage"), BsonRepresentation(BsonType.String)]
        public required decimal MonetaryCoverage { get; set; }

        [BsonElement("kmCoverage"), BsonRepresentation(BsonType.String)]
        public required decimal KmCoverage { get; set; }

        [BsonElement("baseKmPrice"), BsonRepresentation(BsonType.String)]
        public required decimal BaseKmPrice{ get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public DateTime? UpdatedAt { get; set; }
    }
}
