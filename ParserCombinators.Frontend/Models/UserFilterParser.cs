using System.Diagnostics.CodeAnalysis;
using ParserCombinators.Model;

namespace ParserCombinators.Frontend.Models;

public sealed class UserFilterParser
{
    public bool TryParseFilter(string input, out NewsFilterExpression? filter, [NotNullWhen(false)] out string? error)
    {
        if (string.IsNullOrEmpty(input))
        {
            filter = null;
            error = null;
            return true;
        }

        if (NewsFilterExpressionParser.TryParse(input, out var filterExpression, out error))
        {
            filter = filterExpression;
            return true;
        }

        filter = null;
        return false;
    }
}