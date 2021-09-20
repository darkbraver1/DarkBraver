using Experian.ApiServices.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Experian.ApiServices.Services
{
    public class JsonFakeApiCallerService : ApiCallerService, IJsonFakeApiCallerService
    {
        public JsonFakeApiCallerService(HttpClient httpClient)
            :base(httpClient)
        {
        }
    }
}
