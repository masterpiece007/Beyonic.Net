using Beyonic.Net.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyonic.Net.Services
{
    public class Currency : BeyonicEndpointWrapper
    {
        private const string endpoint_ = "currencies";
        public Currency(string apiKey, string apiVersion) : base(apiKey, apiVersion, endpoint_)
        {

        }
    }
}
