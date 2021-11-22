using System;

namespace DataServiceLayer
{
    public class Movie
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int? Runtime { get; set; }
        public string Genres { get; set; }
    }
}
