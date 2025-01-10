using MongoDB.Bson.Serialization.Conventions;

namespace OrdersMicroservice.src.order.application.commands.start_order.types
{
    public class StartOrderResponse(string id)
    {
        public readonly string Id = id;
    }
}
