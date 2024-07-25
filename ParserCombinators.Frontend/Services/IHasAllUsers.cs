using System.Collections.Immutable;
using ParserCombinators.Model;

namespace ParserCombinators.Frontend.Services;

public interface IHasAllUsers
{
    Task<ImmutableDictionary<int, User>> Get();
}