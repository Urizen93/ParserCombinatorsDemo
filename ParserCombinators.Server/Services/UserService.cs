using System.Collections.Immutable;
using ParserCombinators.Model;
using ParserCombinators.Server.DataLayer.Repositories;

namespace ParserCombinators.Server.Services;

public sealed class UserService : IHasAllUsers, IHasUser
{
    private readonly IUserRepository _userRepository;
    private readonly IMapUsers _userMapper;

    public UserService(IUserRepository userRepository, IMapUsers userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<User?> Get(int userID) =>
        await _userRepository.Get(userID) is { } user
            ? _userMapper.Map(user)
            : null;

    public async Task<ImmutableDictionary<int, User>> Get()
    {
        var users = await _userRepository.Get();
        return users.ToImmutableDictionary(user => user.ID, _userMapper.Map);
    }
}