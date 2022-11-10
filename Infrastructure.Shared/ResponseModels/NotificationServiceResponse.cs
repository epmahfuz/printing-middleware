using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Shared.ResponseModels.ErrorResponse;

namespace Infrastructure.Shared.ResponseModels
{
    public class NotificationServiceResponse
    {
        public Error Errors { get; set; }
        public List<string> ErrorMessages { get; set; }
        public int StatusCode { get; set; }
        public string RequestUri { get; set; }
        public object ExternalError { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
