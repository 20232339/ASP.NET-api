using Microsoft.EntityFrameworkCore;
using GameStore.api.Entities;

namespace GameStore.api.Data;

public class GamesStoreContext(DbContextOptions<GamesStoreContext>options)
 : DbContext(options)
{

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

}
