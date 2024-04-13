using Microsoft.EntityFrameworkCore;
using GameStore.api.Entities;

namespace GameStore.api.Data;

public class GamesStoreContext(DbContextOptions<GamesStoreContext>options)
 : DbContext(options)
{

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Action"},
            new {Id = 2, Name = "MOBA"},
            new {Id = 3, Name = "Strategy"},
            new {Id = 4, Name = "Fighting"},
            new {Id = 5, Name = "MMO"}
        );

    }
}
