using FirebaseAdmin;
using OrdersMicroservice.core.Application;

namespace OrdersMicroservice.core.Infrastructure
{
    public class FirebaseAppClient : INotificationAppClient
    {
        public void InitializeApp(AppOptions options)
        {
            FirebaseApp.Create(options);
        }
    }
}
