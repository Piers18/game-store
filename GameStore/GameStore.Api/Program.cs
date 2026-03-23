using GameStore.Api.Dtos;

// constants for endpoint names
const string GetGameEndpointName = "GetGameById";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new(
        1,
        "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
        59.99m,
        new DateOnly(2017, 3, 3)
    ),
    new(
        2,
        "Super Mario Odyssey",
        "Platform",
        59.99m,
        new DateOnly(2017, 10, 27)
    ),
    new(
        3,
        "Red Dead Redemption 2",
        "Action-adventure",
        59.99m,
        new DateOnly(2018, 10, 26)
    )
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).
    WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto createGameDto) =>
{
    int newId = games.Count + 1;
    GameDto newGame = new(
        newId,
        createGameDto.Name,
        createGameDto.Genre,
        createGameDto.Price,
        createGameDto.ReleaseDate
    );
    games.Add(newGame);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = newId }, newGame);
});

// PUT /games/{id}
app.MapPut("/games/{id}", (int id, CreateGameDto updateGameDto) =>
{
    GameDto? existingGame = games.Find(game => game.Id == id);
    if (existingGame is null)
    {
        return Results.NotFound();
    }
    GameDto updatedGame = new(
        id,
        updateGameDto.Name,
        updateGameDto.Genre,
        updateGameDto.Price,
        updateGameDto.ReleaseDate
    );

    return Results.Ok(updatedGame);
});

// DELETE /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
    GameDto? existingGame = games.Find(game => game.Id == id);
    if (existingGame is null)
    {
        return Results.NotFound();
    }
    games.Remove(existingGame);
    return Results.NoContent();
});

app.Run();
