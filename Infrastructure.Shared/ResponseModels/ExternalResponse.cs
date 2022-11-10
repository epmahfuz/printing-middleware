using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.ResponseModels
{
    public class ExternalResponse
    {
        public string RequestUri
        {
            get;
            set;
        }

        public string ExternalError
        {
            get;
            set;
        }

        public HttpStatusCode HttpStatusCode
        {
            get;
            set;
        }
    }
}
