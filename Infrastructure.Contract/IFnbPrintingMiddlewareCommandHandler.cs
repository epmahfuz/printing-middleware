using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract
{
    public interface IFnbPrintingMiddlewareCommandHandler
    {
        TResponse Submit<TCommand, TResponse>(TCommand command);
        Task<TResponse> SubmitAsync<TCommand, TResponse>(TCommand command);
    }
}
