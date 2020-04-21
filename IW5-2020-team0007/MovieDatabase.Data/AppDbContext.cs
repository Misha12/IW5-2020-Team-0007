using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data.Entity;

namespace MovieDatabase.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(o => o.RefreshTokens).WithOne(o => o.User);

            modelBuilder.Entity<GenreMap>(builder =>
            {
                builder.HasOne(o => o.Movie).WithMany(o => o.Genres);
                builder.HasOne(o => o.Genre).WithMany(o => o.Movies);
            });

            modelBuilder.Entity<MoviePerson>(builder =>
            {
                builder.HasOne(o => o.Movie).WithMany(o => o.Persons);
                builder.HasOne(o => o.Person).WithMany(o => o.Movies);
            });

            modelBuilder.Entity<Movie>(builder =>
            {
                builder.HasMany(o => o.Ratings).WithOne(o => o.Movie);
                builder.HasMany(o => o.Names).WithOne(o => o.Movie);
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
