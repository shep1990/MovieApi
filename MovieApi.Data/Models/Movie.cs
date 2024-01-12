using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Data.Models
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Release_Date { get; set; }  
        public string Overview { get; set; } = string.Empty;
        public decimal Popularity { get; set; }
        public int Vote_Count { get; set; }
        public decimal Vote_Average { get; set; }
        public string Original_Language { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Poster_Url { get; set; } = string.Empty;
    }
}
