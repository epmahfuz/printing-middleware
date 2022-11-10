

using System;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Helpers
{
    public class CommandHandler
    {
        private readonly IServiceProvider serviceProvider;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TResponse> SubmitAsync<TCommand, TResponse>(TCommand command)
        {
            return (serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>)) as ICommandHandler<TCommand, TResponse>).HandleAsync(command);
        }

        public TResponse Submit<TCommand, TResponse>(TCommand command)
        {
            return (serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>)) as ICommandHandler<TCommand, TResponse>).Handle(command);
        }
    }
}
