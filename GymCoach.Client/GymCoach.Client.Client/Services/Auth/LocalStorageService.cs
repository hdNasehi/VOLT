using System.Text.Json;
using Microsoft.JSInterop;

namespace GymCoach.Client.Client.Services.Auth;

public interface ILocalStorageService
{
    Task<T?> GetItemAsync<T>(string key);
    Task<string?> GetItemAsync(string key);
    Task SetItemAsync<T>(string key, T value);
    Task RemoveItemAsync(string key);
}

public sealed class LocalStorageService(IJSRuntime js) : ILocalStorageService
{
    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await GetItemAsync(key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task<string?> GetItemAsync(string key) =>
        await js.InvokeAsync<string?>("voltStorage.getItem", key);

    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await js.InvokeVoidAsync("voltStorage.setItem", key, json);
    }

    public Task RemoveItemAsync(string key) =>
        js.InvokeVoidAsync("voltStorage.removeItem", key).AsTask();
}
