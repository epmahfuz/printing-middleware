using Infrastructure.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain.ServiceExtensions
{
    public static class InfrastructureServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            services.AddSingleton<NotificationMessageHandler>();

            services.AddTransient<INotificationClient, NotificationClient>();
            //services.AddTransient<IKioskPaymentMiddlewareCommandHandler, KioskPaymentMiddlewareCommandHandler>();

            //services.AddSingleton<INotificationServiceClient, NotificationServiceClient>();
        }
    }
}
