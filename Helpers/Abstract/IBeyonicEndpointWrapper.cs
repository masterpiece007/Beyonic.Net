using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyonic.Net.Helpers.Abstract
{
    public interface IBeyonicEndpointWrapper
    {
        //Task<object> Send(List<object> headerParameters);
        //object Get(string id, List<object> parameters);
        Task<object> Send(List<KeyValuePair<string, string>> headerParameters);
        Task<object> Get(string id, List<KeyValuePair<string, string>> parameters);
        Task<object> GetAll(List<KeyValuePair<string, string>> parameters);
        Task<object> Create(List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headerParameters);
        Task<object> Update(string id, List<KeyValuePair<string, string>> parameters, List<KeyValuePair<string, string>> headerParameters);
        Task<object> Delete(string id);

    }
}
