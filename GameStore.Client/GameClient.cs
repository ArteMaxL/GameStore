using GameStore.Client.Models;

namespace GameStore.Client;

public static class GameClient
{
    private static readonly List<Game> games = new()
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

    public static Game[] GetGames()
    {
        return games.ToArray();
    }

    public static void AddGame(Game game)
    {
        game.Id = games.Max(x => x.Id) + 1;
        games.Add(game);
    }

    public static Game GetGame(int id)
    {
        return games.Find(x => x.Id == id) ?? throw new Exception("Could not find game!");
    }

    public static void UpdateGame(Game updatedGame)
    {
        var existingGame = GetGame(updatedGame.Id);

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public static void DeleteGame(int id)
    {
        var game = GetGame(id);
        games.Remove(game);
    }
}