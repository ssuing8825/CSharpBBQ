using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.ServiceModel.Web;
using System.Web;
using CSharpBbq.Data.Model;

namespace CSharpBbq.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MoviesService : IMoviesService
    {
        [WebGet(UriTemplate = "Movie({id})")]
        public Movie GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "GetMovieByTagId{tagId}")]
        public List<Movie> GetMovieList(string tagId)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "Movie", Method = "POST")]
        public Movie CreateMovie(Data.Model.Movie person)
        {
            throw new NotImplementedException();
        }

        private List<Movie> GetFromCache()
        {
            var pslist = HttpContext.Current.Cache.Get("MovieList") as List<Movie>;
            if (pslist == null)
            {
                pslist = new List<Movie>();
                HttpContext.Current.Cache.Insert("MovieList", pslist);
            }
            return pslist;
        }

        private void AddToCache(object obj)
        {
            HttpContext.Current.Cache.Insert("MovieList", obj);
        }

    }
}
