using FirebaseAdmin.Messaging;
using OrdersMicroservice.core.Application;

namespace OrdersMicroservice.core.Infrastructure
{
    public class FirebaseMessagingClient : IMessagingClient
    {
        public async Task<string> SendAsync(Message message)
        {
            return await FirebaseMessaging.DefaultInstance.SendAsync(message);

        }
    }
}
