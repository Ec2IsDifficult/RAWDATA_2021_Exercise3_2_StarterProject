using System.Collections.Generic;

namespace DataServiceLayer
{
    public interface IDataService
    {
        List<Movie> GetMovies(int page, int pageSize);
        Movie GetMovie(string id);
        int NumberOfMovies();
    }
}