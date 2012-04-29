using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CSharpBbq.Data.Model;

namespace CSharpBbq.Service
{
    [ServiceContract]
    interface IMoviesService
    {
        [OperationContract]
        Movie GetMovie(string id);

        [OperationContract]
        List<Movie> GetMovieList(string TagId);

        [OperationContract]
        Movie CreateMovie(Movie person);
    }
}
