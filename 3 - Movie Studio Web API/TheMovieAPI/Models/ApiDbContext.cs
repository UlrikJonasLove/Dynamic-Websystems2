using Microsoft.EntityFrameworkCore;

namespace TheMovieAPI.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }
        public DbSet<Movies> Movie { get; set; }
        public DbSet<Studios> Studio { get; set; }
        public DbSet<Ratings> Rating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Studios>().HasData(
                    new Studios(1, "Filmstaden", "Jönköping"),
                    new Studios(2, "Stockholms Skärgård filmstudio", "Stockholm"),
                    new Studios(3, "Skåne studio", "Lund")
           );

            modelBuilder.Entity<Movies>().HasData(
                    new Movies(1, "The Revenant", "The Survivalist Hugh Glass is hunting the man who left him for dead, after a bear attack.", "", ""),
                    new Movies(2, "Gangs Of New York", "A young man, called Amsterdam is looking for revange after his father was murded in a gang war by Bill The Butcher", "", ""),
                    new Movies(3, "Troy", "A love story in ancient time. Achilles, Hector, Paris.", "", ""),
                    new Movies(4, "Guardians Of The Galaxy", "The outlaw Peter Quill is the savior of the galaxy, along side the other guardians.", "", ""),
                    new Movies(5, "Guardians Of The Galaxy Vol 2", "The next installment of the guardians saga. This time, Peter Quill learns more about his dangerous roots.", "", "")
           );
        }
    }
}
