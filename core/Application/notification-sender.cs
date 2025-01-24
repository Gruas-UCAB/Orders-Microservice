using FirebaseAdmin.Messaging;
using FirebaseAdmin;

namespace OrdersMicroservice.core.Application
{
    public interface IMessagingService
    {
        Task SendPushNotificationAsync(string deviceToken, string? messageTitle, string? messageBody);
    }

    public interface IMessagingClient
    {
        Task<string> SendAsync(Message message);
    }

    public interface INotificationAppClient
    {
        void InitializeApp(AppOptions options);
    }

}
