using Beyonic.Net.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyonic.Net.Services
{
    public class Webhook : BeyonicEndpointWrapper
    {
        private const string endpoint_ = "webhooks";
        public Webhook(string apiKey, string apiVersion) : base(apiKey, apiVersion, endpoint_)
        {

        }
    }
}
