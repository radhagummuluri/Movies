using Microsoft.EntityFrameworkCore;
using Movies.RestApi.Data.Models;

namespace Movies.RestApi.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRating>()
                .HasKey(c => new { c.UserID, c.MovieID });
            modelBuilder.Entity<MovieGenre>()
                .HasKey(g => new { g.GenreID, g.MovieID});
        }
    }
}
