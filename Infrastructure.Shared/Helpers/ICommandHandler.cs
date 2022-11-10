using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Helpers
{
    public interface ICommandHandler<TCommand, TResponse>
    {
        TResponse Handle(TCommand command);

        Task<TResponse> HandleAsync(TCommand command);
    }
}
