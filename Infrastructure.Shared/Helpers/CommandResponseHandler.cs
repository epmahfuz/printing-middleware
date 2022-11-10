using FluentValidation.Results;
using Infrastructure.Shared.ResponseModels;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infrastructure.Shared.Helpers
{
    public static class CommandResponseHandler
    {
        public static CommandResponse HandleSuccess(HttpStatusCode successStatusCode)
        {
            return new CommandResponse()
            {
                HttpStatusCode = successStatusCode
            };
        }

        public static CommandResponse HandleError(ILogger log, HttpStatusCode errorStatusCode, string functionName, string errorMessage, Exception ex = null, ValidationResult validationError = null)
        {
            var exceptionDetails = ex != null ? ex.ToString() : string.Empty;
            log.LogError($"Error occurred while processing the request. Function name: {functionName}, reason: {errorMessage}, details: {exceptionDetails}");
            var response = new CommandResponse()
            {
                Errors = validationError,
                ExternalError = ex?.Message,
                HttpStatusCode = errorStatusCode
            };
            return response;
        }
    }
}
