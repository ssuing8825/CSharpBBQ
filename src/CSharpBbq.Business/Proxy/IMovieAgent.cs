using CSharpBbq.Business.Model;

namespace CSharpBbq.Business.Proxy
{
    public interface IMovieAgent
    {
        Movie GetMovie(int movieId);
        Movie SaveMovie(Movie movie);
    }
}
