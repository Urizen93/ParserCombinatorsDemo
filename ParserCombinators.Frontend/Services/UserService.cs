using System.Collections.Immutable;
using ParserCombinators.Model;
using ParserCombinators.Frontend.Clients;

namespace ParserCombinators.Frontend.Services;

public sealed class UserService : IHasAllUsers
{
    private readonly IServerClient _server;

    public UserService(IServerClient server) => _server = server;

    public async Task<ImmutableDictionary<int, User>> Get() =>
        await _server.Get<ImmutableDictionary<int, User>>("api/users");
}