using System;
using System.Net.Http;
using Microsoft.Runtime.Serialization;
using System.Net.Http.Headers;

namespace CSharpBbq.Business.Proxy
{
    public abstract class ServiceAgentBase
    {
        private readonly HttpClientChannel httpClientChannel;
        protected string endPoint;

        protected ServiceAgentBase(HttpClientChannel httpClientChannel)
        {
            this.httpClientChannel = httpClientChannel;
        }

        /// <summary>
        /// Gets the strongly typed model from the policy service call.
        /// </summary>
        /// <typeparam name="T">Type of model to retreive</typeparam>
        /// <param name="methodUniformResourceIdentifier">method Uri</param>
        /// <returns>Strongly typed T Object</returns>
        protected T Get<T>(string methodUniformResourceIdentifier)
        {
            using (var httpClient = new HttpClient(endPoint))
            {
                if (httpClientChannel != null)
                    httpClient.Channel = httpClientChannel;
                httpClient.CreateBasicAuth();

                var httpresponse = httpClient.Get(methodUniformResourceIdentifier);
             
                if (httpresponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return httpresponse.Content.ReadAsDataContract<T>();
                }
                else
                {
                    throw new Exception(string.Format("Invalid Status Code {0}", httpresponse.StatusCode));
                }
            }

        }

        /// <summary>
        /// Posts the strongly typed model to the policy service call.
        /// </summary>
        /// <typeparam name="T">Type of model to retreive</typeparam>
        /// <param name="methodUniformResourceIdentifier">method Uri</param>
        /// <param name="modelObject">Strongly typed object</param>
        /// <returns>Strongly typed T Object</returns>
        protected T Save<T>(string methodUniformResourceIdentifier, T modelObject)
        {
      

            using (var httpClient = new HttpClient(endPoint))
            {
                if (httpClientChannel != null)
                    httpClient.Channel = httpClientChannel;
                httpClient.CreateBasicAuth();
                HttpContent httpcontent = modelObject.ToContentUsingDataContractSerializer();
                httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                var httpresponse = httpClient.Post(methodUniformResourceIdentifier, httpcontent);
                httpresponse.ThrowIfStatusNotSuccessful();
                return httpresponse.Content.ReadAsDataContract<T>();

            }
        }
    }
}
