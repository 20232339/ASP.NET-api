using GameStore.api.Endpoints;
using GameStore.api.Data;


var builder = WebApplication.CreateBuilder(args);

//connection string
var connString =builder.Configuration.GetConnectionString("GameStore");
//dependency injection
builder.Services.AddSqlite<GamesStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
