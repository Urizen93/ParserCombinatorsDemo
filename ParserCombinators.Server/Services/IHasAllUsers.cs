using System.Collections.Immutable;
using ParserCombinators.Model;

namespace ParserCombinators.Server.Services;

public interface IHasAllUsers
{
    Task<ImmutableDictionary<int, User>> Get();
}