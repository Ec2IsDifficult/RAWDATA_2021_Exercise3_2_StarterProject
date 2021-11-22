using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3_2_StarterProject.Models
{
    public class MovieDto
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Runtime { get; set; }
        public string Genres { get; set; }
    }
}
