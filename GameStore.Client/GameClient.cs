using GameStore.Client.Models;
using System.Net.Http.Json;

namespace GameStore.Client;

public class GameClient
{
    private readonly HttpClient _httpClient;

    public GameClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Game[]?> GetGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<Game[]>("games");
    }

    public async Task AddGameAsync(Game game)
    {
        await _httpClient.PostAsJsonAsync("games", game);
    }

    public async Task<Game> GetGameAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Game>($"games/{id}") 
            ?? throw new Exception("Could not find game!");
    }

    public async Task UpdateGameAsync(Game updatedGame)
    {
        await _httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);
    }

    public async Task DeleteGameAsync(int id)
    {
        await _httpClient.DeleteAsync($"games/{id}");
    }
}