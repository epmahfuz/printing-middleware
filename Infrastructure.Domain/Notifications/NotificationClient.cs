
using Infrastructure.Contract;
using Infrastructure.Domain;
using Infrastructure.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class NotificationClient: INotificationClient
    {
        private readonly NotificationMessageHandler _notificationMessageHandler;
        public NotificationClient(NotificationMessageHandler notificationMessageHandler)
        {
            _notificationMessageHandler = notificationMessageHandler;
        }
        public async Task SendNotificationAsync(NotificationMessagePayload payload)
        {
            await _notificationMessageHandler.SendMessageAsync(payload);
        }
        public async Task SendNotificationAsync<T>(T payload, string socketId, bool isSuccessfull)
        {
            var serializePayload = JsonConvert.SerializeObject(payload);
            var message = new NotificationMessagePayload
            {
                KioskId = socketId,
                SerializedPayload = serializePayload,
                IsSuccessfull = isSuccessfull
            };
            await _notificationMessageHandler.SendMessageAsync(message);
        }
        public void SendNotification(NotificationMessagePayload payload)
            => SendNotificationAsync(payload).Wait();
        public void SendNotification<T>(T payload, string socketId, bool isSuccessfull)
            => SendNotificationAsync(payload, socketId, isSuccessfull).Wait();

    }
}
