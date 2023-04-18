using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beyonic.Net.Helpers.Abstract;

namespace Beyonic.Net.Helpers.Concrete
{
    public class BeyonicEndpointWrapper : IBeyonicEndpointWrapper
    {
        private string endpoint { get; set; }
        private IBeyonic beyonic { get; set; }
        public BeyonicEndpointWrapper(string apiKey,string apiVersion,string endpoint)
        {
            beyonic = new Beyonic(apiKey, apiVersion);
            this.endpoint = endpoint;
        }
        public async Task<object> Send(List<KeyValuePair<string, string>> headerParameters)
        {
            return await Update("0", null, headerParameters);
        }

        public async Task<object> Get(string id, List<KeyValuePair<string,string>> parameters)
        {
            return await beyonic.SendRequest(endpoint, "GET", id, parameters);
        }

        public async Task<object> GetAll(List<KeyValuePair<string, string>> parameters)
        {
            return await beyonic.SendRequest(endpoint, "GET", null, parameters);
        }

        public async Task<object> Create(List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headerParameters)
        {
            return await beyonic.SendRequest(endpoint, "POST", null,parameters, headerParameters);
        }

        public async Task<object> Update(string id, List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headerParameters)
        {
            return await beyonic.SendRequest(endpoint, "PATCH", id, parameters, headerParameters);
        }

        public async Task<object> Delete(string id)
        {
            return await beyonic.SendRequest(endpoint, "DELETE", id);
        }
    }
}
