using Infrastructure.Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Helpers
{
    public abstract class EcapAbstractCommandHandler<TCommand>
        : ICommandHandler<TCommand, CommandResponse> where TCommand : class
    {
        public CommandResponse Handle(TCommand command)
            => HandleAsync(command).Result;

        public abstract Task<CommandResponse> HandleAsync(TCommand command);

    }
}
