using Infrastructure.Contract;
using Infrastructure.Shared.Helpers;
using Infrastructure.Shared.ResponseModels;
using Notification.Command;

namespace Notification.CommandHandler
{
    public class NotificationCommandHandler : EcapAbstractCommandHandler<NotificationCommand>
    {
        private readonly INotificationClient _notificationClient;
        public NotificationCommandHandler(INotificationClient notificationService)
        {
            _notificationClient = notificationService;
        }
        public override async Task<CommandResponse> HandleAsync(NotificationCommand command)
        {
            await _notificationClient.SendNotificationAsync(command);
            return new CommandResponse();

        }
    }
}