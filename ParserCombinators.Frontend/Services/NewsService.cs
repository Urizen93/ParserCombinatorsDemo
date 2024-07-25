using ParserCombinators.Model;
using ParserCombinators.Frontend.Clients;

namespace ParserCombinators.Frontend.Services;

public sealed class NewsService : IHasNews
{
    private readonly IServerClient _server;

    public NewsService(IServerClient server) => _server = server;

    public async Task<IReadOnlyCollection<PieceOfNews>> Get() =>
        await _server.Get<IReadOnlyCollection<PieceOfNews>>("api/news");
    
    public async Task<IReadOnlyCollection<PieceOfNews>> GetForUser(int userID) =>
        await _server.Get<IReadOnlyCollection<PieceOfNews>>($"api/users/{userID}/news");
}