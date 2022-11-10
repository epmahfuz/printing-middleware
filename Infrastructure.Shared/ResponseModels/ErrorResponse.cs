using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.ResponseModels
{
    public class ErrorResponse
    {
        public class FormattedMessagePlaceholderValues
        {
            public string PropertyName { get; set; }
            public string PropertyValue { get; set; }
        }
        public class Error
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
            public string AttemptedValue { get; set; }
            public object CustomState { get; set; }
            public string Severity { get; set; }
            public string ErrorCode { get; set; }
            public List<object> FormattedMessageArguments { get; set; }
            public FormattedMessagePlaceholderValues FormattedMessagePlaceholderValues { get; set; }
            public string ResourceName { get; set; }

            public bool IsValid { get; set; }
            public List<Error> Errors { get; set; }
            public List<string> RuleSetsExecuted { get; set; }
        }
    }
}
