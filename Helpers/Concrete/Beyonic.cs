using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Beyonic.Net.Helpers.Abstract;
using Beyonic.Net.Services;
using Newtonsoft.Json;

namespace Beyonic.Net.Helpers.Concrete
{
    public class Beyonic : IBeyonic
    {
        private HttpResponseMessage httpResponse;

        private static string _apiURL = "https://api.mfsafrica.com/api";
        private string _apiKey { get; set; } = "";
        private string _apiVersion { get; set; } = "";

        public Beyonic(string ApiKey,string ApiVersion)
        {
            _apiKey = ApiKey;
            _apiVersion = ApiVersion;
        }
        public async  Task<object> SendRequest(string endpoint, string method = "GET", string id = null,List<KeyValuePair<string,string>> parameters = null, List<KeyValuePair<string, string>> headerParameters = null)
        {
            var requestUrl = $"{_apiURL}/{endpoint}";
            if (!string.IsNullOrEmpty(id))
                requestUrl = $"{requestUrl}/{id}";

            string jsonData = null;
            if (parameters != null)
            {
                Dictionary<string, string> metadata = new Dictionary<string, string>();
               
                foreach (var kvp in parameters)
                {
                    string key = kvp.Key;
                    string value = kvp.Value;

                    if (key.StartsWith("metadata."))
                    {
                        metadata[key.Split('.')[1]] = value;
                        parameters.Remove(kvp);
                    }
                }

                if (metadata.Count > 0)
                {
                    //parameters.AddRange("metadata", metadata);
                    parameters.AddRange(metadata.ToList<KeyValuePair<string,string>>());
                }

                jsonData = JsonConvert.SerializeObject(parameters);
            }

            var client = new HttpClient();
            if (headerParameters.Count > 0)
            {
                foreach (var item in headerParameters)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            else
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Content-Language", "en-US");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiKey);
                client.DefaultRequestHeaders.Add("Beyonic-Client", "CSharp");
                client.DefaultRequestHeaders.Add("Beyonic-Client-Version", _apiVersion);
            }

            if(!string.IsNullOrEmpty(_apiVersion) )
                client.DefaultRequestHeaders.Add("Beyonic-Version", _apiVersion);

            switch (method)
            {
                case "GET":
                    if (parameters != null)
                    {
                        requestUrl += "?";
                        foreach (var pair in parameters)
                        {
                            requestUrl += pair.Key + "=" + WebUtility.UrlEncode(pair.Value) + "&";
                        }
                    }

                    break;
                case "POST":
                    var postRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    var postContent = new StringContent(jsonData);
                    postRequest.Content = postContent;
                    postRequest.Content.Headers.ContentLength = jsonData.Length;

                    //httpResponse = client.SendAsync(postRequest).GetAwaiter().GetResult();
                    httpResponse = await client.SendAsync(postRequest);

                    break;  
                
                case "PATCH":
                    var patchRequest = new HttpRequestMessage(HttpMethod.Patch, requestUrl);
                    var patchContent = new StringContent(jsonData);
                    patchRequest.Content = patchContent;
                    patchRequest.Content.Headers.ContentLength = jsonData.Length;

                    httpResponse = await client.SendAsync(patchRequest);
                    break; 
                
                case "DELETE":
                    var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
                    var deleteContent = new StringContent(jsonData);
                    deleteRequest.Content = deleteContent;
                    deleteRequest.Content.Headers.ContentLength = jsonData.Length;

                    httpResponse = await client.SendAsync(deleteRequest);
                    break;
            }
            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                var deserializedResult = JsonConvert.DeserializeObject<object>(result);
                return deserializedResult;
            }
            else
            {
                throw new BeyonicException("error", int.Parse(httpResponse.StatusCode.ToString()), requestUrl, method,
                    result, null);
            }
            
        }

        public  string GetClientVersion()
        {
           
            return "0.0.16";
        }
    }
}
