using MovieApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Data.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> Get();
        Task<List<Movie>> GetByPageNumber(int? page, int pagesize = 10);
    }
}
