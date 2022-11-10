using Newtonsoft.Json;
using FluentValidation.Results;
using Infrastructure.Shared.Helpers;

namespace Infrastructure.Shared.ResponseModels
{
    [JsonConverter(typeof(CommandResponseConverter))]
    public class CommandResponse : ExternalResponse
    {

        public ValidationResult Errors
        {
            get;
            set;
        }

        public IEnumerable<string> ErrorMessages
        {
            get
            {
                if (Errors != null)
                {
                    foreach (ValidationFailure error in Errors.Errors)
                    {
                        yield return error.ErrorMessage;
                    }
                }

                if (!string.IsNullOrWhiteSpace(base.ExternalError))
                {
                    yield return base.ExternalError;
                }
            }
        }

        public int StatusCode
        {
            get
            {
                if (Errors != null)
                {
                    if (!Errors.IsValid)
                    {
                        return 1;
                    }

                    return 0;
                }

                if (!string.IsNullOrWhiteSpace(base.ExternalError))
                {
                    return 1;
                }

                if (ErrorMessages != null && ErrorMessages.Any())
                {
                    return 1;
                }

                return 0;
            }
        }

        public CommandResponse()
        {
            Errors = new ValidationResult();
        }

        public CommandResponse(ValidationResult result)
        {
            Errors = result;
        }

        public CommandResponse SetError(string propertyName, string errorMessage)
        {
            ValidationFailure item = new ValidationFailure(propertyName, errorMessage);
            Errors.Errors.Add(item);
            return this;
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
