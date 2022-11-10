using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.ResponseModels
{
     public class HttpRequestResponse<TSuccessResponse> where TSuccessResponse : class, new()
    {
        public TSuccessResponse SuccessResponse { get; set; }
        public object FailedResponse { get; set; }
    }
}
