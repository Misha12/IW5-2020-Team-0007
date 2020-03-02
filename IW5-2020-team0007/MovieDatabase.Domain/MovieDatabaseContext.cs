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

            modelBuilder.Entity<Movie>().HasMany(o => o.Names).WithOne(o => o.Movie);
            modelBuilder.Entity<Movie>().HasMany(o => o.Rates).WithOne(o => o.Movie);
            modelBuilder.Entity<Movie>().HasOne(o => o.Genre).WithMany(o => o.Movies);

            modelBuilder.Entity<Movie>().HasMany(o => o.Actors).WithOne(o => o.Movie);
            modelBuilder.Entity<Movie>().HasMany(o => o.Directors).WithOne(o => o.Movie);

            modelBuilder.Entity<Person>().HasMany(o => o.ActorInMovies).WithOne(o => o.Person);
            modelBuilder.Entity<Person>().HasMany(o => o.DirectorOfMovies).WithOne(o => o.Person);
        }
    }
}
