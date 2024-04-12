using GameStore.api.Endpoints;
using GameStore.api.Data;


var builder = WebApplication.CreateBuilder(args);

var connString =builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GamesStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
