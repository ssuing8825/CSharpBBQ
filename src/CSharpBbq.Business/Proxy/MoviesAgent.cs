using System;
using System.Configuration;
using System.Net.Http;
using CSharpBbq.Business.Model;

namespace CSharpBbq.Business.Proxy
{
    public class MovieAgent : ServiceAgentBase, IMovieAgent
    {
        public MovieAgent()
            : this(null)
        {
        }

        public MovieAgent(HttpClientChannel httpClientChannel)
            : base(httpClientChannel)
        {
            this.endPoint = ConfigurationManager.AppSettings["MovieEndPoint"];
        }

        public Movie GetMovie(int movieId)
        {
            return this.Get<Movie>(String.Format("Movie({0})", movieId));
        }

        public Movie SaveMovie(Movie movie)
        {
            return this.Save("Movie", movie);
        }
    }
}
