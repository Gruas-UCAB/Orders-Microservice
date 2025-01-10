using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrdersMicroservice.src.order.domain.entities.extraCost;

namespace OrdersMicroservice.src.order.infrastructure.models
{
    public class MongoOrder
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("orderNumber"), BsonRepresentation(BsonType.Int32)]
        public required int OrderNumber { get; set; }

        [BsonElement("date"), BsonRepresentation(BsonType.String)]
        public required DateTime Date { get; set; }

        [BsonElement("orderStatus"), BsonRepresentation(BsonType.String)]
        public required string OrderStatus { get; set; }

        [BsonElement("incidentType"), BsonRepresentation(BsonType.String)]
        public required string IncidentType { get; set; }

        [BsonElement("destination"), BsonRepresentation(BsonType.String)]
        public required string Destination { get; set; }

        [BsonElement("location"), BsonRepresentation(BsonType.String)]
        public required string Location { get; set; }

        [BsonElement("orderDispatcherId"), BsonRepresentation(BsonType.String)]
        public required string DispatcherId { get; set; }

        [BsonElement("conductorAssignedId"), BsonRepresentation(BsonType.String)]
        public string? ConductorAssignedId { get; set; }

        [BsonElement("contractId"), BsonRepresentation(BsonType.String)]
        public required string ContractId { get; set; }

        [BsonElement("cost"), BsonRepresentation(BsonType.Decimal128)]
        public required decimal Cost { get; set; }

        [BsonElement("isCostCoveredByPolicy"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsCostCoveredByPolicy { get; set; }

        [BsonElement("extraCosts"), BsonRepresentation(BsonType.Array)]
        public required List<ExtraCost> ExtraCosts { get; set; }

        [BsonElement("payed"), BsonRepresentation(BsonType.Boolean)]
        public required bool Payed { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
