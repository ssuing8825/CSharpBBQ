using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;

namespace CSharpBbq.Test.Utils
{
    public class FakeChannel<T> : HttpClientChannel
    {
        private T responseObject;

        public FakeChannel(T responseObject)
        {
            this.responseObject = responseObject;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return new HttpResponseMessage()
            {
                RequestMessage = request,
                Content = new StreamContent(this.GetContentStream())
            };
        }

        private Stream GetContentStream()
        {
            var serializer = new DataContractSerializer(typeof(T));
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, this.responseObject);
            stream.Position = 0;
            return stream;
        }
    }
}
