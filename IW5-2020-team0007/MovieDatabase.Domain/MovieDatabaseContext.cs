using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain.Entity;

namespace MovieDatabase.Domain
{
    public class MovieDatabaseContext : DbContext
    {
        public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
    }
}
