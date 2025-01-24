using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using OrdersMicroservice.core.Application;

namespace OrdersMicroservice.core.Infrastructure
{
    public class FirebaseNotificationSender : IMessagingService
    {
        private readonly FirebaseMessagingSettings _firebaseMessagingSettings;
        private readonly IMessagingClient _messagingClient;
        private readonly INotificationAppClient _appClient;

        public FirebaseNotificationSender(
        IOptions<FirebaseMessagingSettings> firebaseMessagingSettings,
        IMessagingClient messagingClient,
        INotificationAppClient appClient
        )
        {
            _firebaseMessagingSettings = firebaseMessagingSettings.Value;
            _messagingClient = messagingClient;
            _appClient = appClient;
            _appClient.InitializeApp(new FirebaseAdmin.AppOptions()
            {
                Credential = GoogleCredential.FromFile(_firebaseMessagingSettings.CredentialPath)
            });
        }

        public async Task SendPushNotificationAsync(string deviceToken, string? messageTitle, string? messageBody)
        {
            try
            {
                var message = new Message()
                {
                    Token = deviceToken,
                    Notification = new Notification()
                    {
                        Title = messageTitle,
                        Body = messageBody
                    },
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High,
                        Notification = new AndroidNotification()
                        {
                            Title = messageTitle,
                            Body = messageBody,
                            Priority = NotificationPriority.HIGH,
                            Sound = _firebaseMessagingSettings.MessageSound,
                            DefaultSound = false,
                            ChannelId = _firebaseMessagingSettings.ChannelId

                        }
                    }
                };
                await _messagingClient.SendAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has ocurred during the notification send.");
            }
        }
    }
}
