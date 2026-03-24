using System;
using GameStore.Api.Dtos;
using Microsoft.VisualBasic;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    // constants for endpoint names
    const string GetGameEndpointName = "GetGameById";

    private static readonly List<GameDto> games = [
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

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto createGameDto) =>
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
        group.MapPut("/{id}", (int id, CreateGameDto updateGameDto) =>
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
        group.MapDelete("/{id}", (int id) =>
        {
            GameDto? existingGame = games.Find(game => game.Id == id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            games.Remove(existingGame);
            return Results.NoContent();
        });
    }
}
