using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorial_BlazorWebAppMovies.Models;

namespace Tutorial_BlazorWebAppMovies.Data
{
    public class Tutorial_BlazorWebAppMoviesContext : DbContext
    {
        public Tutorial_BlazorWebAppMoviesContext (DbContextOptions<Tutorial_BlazorWebAppMoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Tutorial_BlazorWebAppMovies.Models.Movie> Movie { get; set; } = default!;
    }
}
