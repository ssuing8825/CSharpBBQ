using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CSharpBbq.Business.Proxy
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Verifies the HttpResponseMessage Status code and raise exception if it is not OK 
        /// </summary>
        /// <param name="response">The HttpResponseMessage instance.</param>
        public static void ThrowIfStatusNotSuccessful(this HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var serviceFault = response.Content.ReadAsString();
                throw new Exception(serviceFault);
            }
        }

        /// <summary>
        /// Creates the basic auth.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public static void CreateBasicAuth(this HttpClient httpClient)
        {
            var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["SDCUserName"] + ":" + ConfigurationManager.AppSettings["SDCPassword"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

    }
}
