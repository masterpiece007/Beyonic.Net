using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beyonic.Net.Helpers.Concrete;

namespace Beyonic.Net.Services
{
    public class Payment : BeyonicEndpointWrapper
    {
        private const string endpoint_ = "payments";
        public Payment(string apiKey, string apiVersion) : base(apiKey, apiVersion, endpoint_) 
        {
            
        }
    }
}
