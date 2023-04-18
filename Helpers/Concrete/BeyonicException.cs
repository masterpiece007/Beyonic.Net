using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyonic.Net.Helpers.Concrete
{
   

    public class BeyonicException : Exception
    {
        public string RequestURL { get; }
        public string RequestMethod { get; }
        public string ResponseBody { get; }

        public BeyonicException(string message, int code, string requestURL, string requestMethod, string responseBody, Exception previous = null)
            : base(message)
        {
            RequestURL = requestURL;
            ResponseBody = responseBody;
            RequestMethod = requestMethod;
        }

        // custom string representation of object
        public override string ToString()
        {
            return $"{GetType().Name} Error: {base.Message} when sending {RequestMethod} to {RequestURL}.\nError Details: {ResponseBody}\n";
        }
    }

}
