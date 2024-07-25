using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace ParserCombinators.Frontend.Clients;

public sealed class ServerClient : IServerClient
{
    private readonly HttpClient _httpClient;

    public ServerClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<T> Get<T>([StringSyntax(StringSyntaxAttribute.Uri)] string path)
    {
        var response = await _httpClient.GetAsync(path);

        return response.IsSuccessStatusCode
               && await response.Content.ReadFromJsonAsync<T>() is { } result
            ? result
            : throw new Exception(await GetErrorMessage(response));
    }

    private static async Task<string> GetErrorMessage(HttpResponseMessage message) =>
        $"Server responded with {message.StatusCode} : {await message.Content.ReadAsStringAsync()}";
}