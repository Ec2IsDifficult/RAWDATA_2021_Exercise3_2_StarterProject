using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class DataService : IDataService
    {
        IList<Movie> _movies = new List<Movie>();

        public DataService()
        {
            var tsv = File.OpenText("movies.tsv");

            if (!tsv.EndOfStream)
            {
                while (!tsv.EndOfStream)
                {
                    var line = tsv.ReadLine();

                    var movie = Parse(line);
                    if (movie != null)
                    {
                        _movies.Add(movie);
                    }
                }
            }
        }

        public List<Movie> GetMovies(int page, int pageSize)
        {
            return _movies
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public Movie GetMovie(string id)
        {
            return _movies.FirstOrDefault(x => x.Id == id);
        }

        public int NumberOfMovies()
        {
            return _movies.Count;
        }

        /***************************************************
         * 
         * Helpers 
         * 
         **************************************************/


        private static Movie Parse(string line)
        {
            var movie = new Movie();
            var pos = 0;
            var data = "";

            (data, pos) = ParseField(pos, line);
            movie.Id = data;

            (data, pos) = ParseField(pos, line);
            movie.Type = data;

            (data, pos) = ParseField(pos, line);
            movie.Title = data;

            (data, pos) = ParseField(pos, line);
            movie.Year = int.Parse(data, NumberStyles.Integer);

            (data, pos) = ParseField(pos, line);
            if (!string.IsNullOrEmpty(data))
            {
                movie.Runtime = int.Parse(data, NumberStyles.Integer);
            }

            (data, pos) = ParseField(pos, line);
            movie.Genres = data;

            return movie;
        }

        static (string, int) ParseField(int pos, string line)
        {
            if (pos >= line.Length)
            {
                return ("", pos);
            }

            var data = "";

            while (pos < line.Length && line[pos] != '\t')
            {
                data += line[pos++];
            }

            // skip ending tab
            pos++;

            return (data.Trim(), pos);
        }
    }
}
