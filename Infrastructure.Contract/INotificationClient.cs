using Infrastructure.Shared.Models;

namespace Infrastructure.Contract
{
    public interface INotificationClient
    {
        Task SendNotificationAsync(NotificationMessagePayload payload);
        void SendNotification(NotificationMessagePayload payload);
        Task SendNotificationAsync<T>(T payload, string socketId, bool isSuccessfull);
        void SendNotification<T>(T payload, string socketId, bool isSuccessfull);
    }
}