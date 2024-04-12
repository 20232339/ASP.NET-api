using GameStore.api.Dtos;
using System.Text.Json;

namespace GameStore.api.Endpoints;


public static class GamesEndpoints
{
    
const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games = [

    new (1,
        "Twice Shy",
        "RPG",
        29.99M,
        new DateOnly(2024,3,20)),
    new (2,
        "Ocarina of Time",
        "Action RPG",
        59.99M,
        new DateOnly(1996,9,22)),        
    new (3,
        "The Legend of Zelda: Breath of the Wild",
        "Action RPG",
        59.99M,
        new DateOnly(2017,3,3)),
];

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
{
    var group = app.MapGroup("games").WithParameterValidation();

    //GET /games
    group.MapGet("/", () => games);
    
    
// GET: /games/{id}
    group.MapGet("/{id}", (int id) =>{ 
    
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);


// POST: /games
group.MapPost("/", (CreateGameDto newGame) => {

    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
}).WithParameterValidation();



// PUT: /games
group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
    
   
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1) {
        return Results.NotFound();
    }
    games[index] = new GameDto(
        id, 
        updatedGame.Name, 
        updatedGame.Genre, 
        updatedGame.Price, 
        updatedGame.ReleaseDate );

        return Results.NoContent();
});

//delete /games/3

group.MapDelete("/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);
    
    return Results.NoContent();
});

        return group;
    }
}