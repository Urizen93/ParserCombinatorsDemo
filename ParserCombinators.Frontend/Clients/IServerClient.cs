using System.Diagnostics.CodeAnalysis;

namespace ParserCombinators.Frontend.Clients;

public interface IServerClient
{
    Task<T> Get<T>([StringSyntax(StringSyntaxAttribute.Uri)] string path);
}