using GameStore.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;

List<Game> games = new()
      {
        new Game
        {
            Id = 1,
            Name = "Assassin's Creed",
            Genre = "Action",
            Price = 19.99M,
            ReleaseDate = new DateTime(2010,11,11)
        },
        new Game
        {
            Id = 2,
            Name = "The Witcher 3",
            Genre = "RPG",
            Price = 12,
            ReleaseDate = new DateTime(2019, 11, 11)
        },
        new Game
        {
            Id = 3,
            Name = "Legend of Zelda: Breath Of The Wild",
            Genre = "RPG",
            Price = 49.99M,
            ReleaseDate = new DateTime(2017, 11, 11)
        },
    };

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("https://localhost:7288")
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var app = builder.Build();

app.UseCors();

var group = app.MapGroup("/games")
    .WithParameterValidation();

// GET /games
group.MapGet("/", () => games);

// GET /games/{id}
group.MapGet("/{id:int}", (int id) => 
{
    Game? game = games.Find(g => g.Id == id);
    
    if (game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
})
.WithName("GetGame");

// POST /games
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(x => x.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

// PUT /games/{id}
group.MapPut("/{id:int}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(g => g.Id == id);

    if (existingGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);

        return Results.CreatedAtRoute("GetGame", new { id = updatedGame.Id }, updatedGame);
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

// DELETE /games/{id}
group.MapDelete("/{id:int}", (int id) =>
{
    Game? existingGame = games.Find(g => g.Id == id);

    if (existingGame is null) 
    {
        return Results.NotFound();
    }

    games.Remove(existingGame);

    return Results.NoContent();
});

app.Run();
