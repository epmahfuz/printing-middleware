using Infrastructure.Domain.ServiceExtensions;
using Infrastructure.Shared.Helpers;
using Notification.CommandHandler;

namespace KioskPaymentMiddleware.ServiceExtensions
{
    public static class ApplicationServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddCommandHandlers();
            serviceCollection.AddInfrastructureServices();
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection container)
        {
            container.RegisterCollection(typeof(ICommandHandler<,>), new[]
            {
                typeof(NotificationCommandHandler).Assembly
            });

            return container;
        }
    }
}