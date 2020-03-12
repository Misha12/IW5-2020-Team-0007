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

            modelBuilder.Entity<Movie>().HasMany(o => o.Persons).WithOne(o => o.Movie);
            modelBuilder.Entity<Person>().HasMany(o => o.InMovies).WithOne(o => o.Person);

            modelBuilder.Entity<MoviePerson>()
                .HasKey(o => new { o.MovieID, o.PersonID });
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
    }
}
