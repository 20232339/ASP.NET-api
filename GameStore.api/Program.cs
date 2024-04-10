using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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
// GET: /games
app.MapGet("games", () => games);


// GET: /games/{id}
app.MapGet("games/{id}", (int id) =>{ 
    
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST: /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1) {
        return Results.NotFound();
    }
    games[index] = new GameDto(
        id, 
        updatedGame.Name, 
        updatedGame.Genre, 
        updatedGame.Price, 
        updatedGame.ReleaseDate);

        return Results.NoContent();
});

//delete /games/3

app.MapDelete("games/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);
    
    return Results.NoContent();
});


app.Run();
