using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Shared.ResponseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Infrastructure.Shared.Helpers
{
    public class CommandResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JArray source = JArray.FromObject(JObject.Load(reader)["Errors"]!["Errors"]);
            if (!source.Any())
            {
                return new CommandResponse();
            }

            return new CommandResponse(new ValidationResult(source.Select(FromJToken)));
        }

        private static ValidationFailure FromJToken(JToken token)
        {
            string propertyName = token["PropertyName"]!.ToString();
            string errorMessage = token["ErrorMessage"]!.ToString();
            string attemptedValue = token["AttemptedValue"]!.ToString();
            return new ValidationFailure(propertyName, errorMessage, attemptedValue)
            {
                Severity = Severity.Error,
                ErrorCode = token["ErrorCode"]!.ToString(),
                CustomState = token["CustomState"]!.ToString()
            };
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
