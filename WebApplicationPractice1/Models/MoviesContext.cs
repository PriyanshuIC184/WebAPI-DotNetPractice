using Microsoft.EntityFrameworkCore;

namespace WebApplicationPractice1.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

        public DbSet<Movies> Movies { get; set; } = null!;
    }
}
