namespace OrdersMicroservice.src.order.application.commands.cancel_order.types
{
    public class CancelOrderResponse(string id)
    {
        public readonly string Id = id;
    }
}
