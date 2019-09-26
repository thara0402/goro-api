using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goro.Api
{
    public class MyOptions
    {
        public string CosmosDBEndpointUri { get; set; }
        public string CosmosDBKey { get; set; }
        public string SearchServiceName { get; set; }
        public string SearchApiKey { get; set; }
    }
}
