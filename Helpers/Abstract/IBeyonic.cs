using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Beyonic.Net.Helpers.Abstract
{
    public interface IBeyonic
    {
    //    object SendRequest(string endpoint, string method, string id, List<object> parameters,List<object> headerParameters);
        string GetClientVersion();
        //object SendRequest(string endpoint, string method = "GET", string id = null,List<KeyValuePair<string,string>> parameters = null, List<object> headerParameters = null);
        Task<object> SendRequest(string endpoint, string method = "GET", string id = null,List<KeyValuePair<string,string>> parameters = null, List<KeyValuePair<string, string>> headerParameters = null);
    }
}
