using ParserCombinators.Model;
using ParserCombinators.Server.DataLayer.Repositories;

namespace ParserCombinators.Server.Services;

public sealed class NewsService : IHasUserNews, IHasAllNews
{
    private readonly INewsRepository _newsRepository;
    private readonly IHasUser _userProvider;

    public NewsService(
        INewsRepository newsRepository,
        IHasUser userProvider)
    {
        _newsRepository = newsRepository;
        _userProvider = userProvider;
    }
    
    public async Task<IEnumerable<PieceOfNews>> GetAll()
    {
        var news = await _newsRepository.Get();
        return news.Select(pieceOfNews => new PieceOfNews(
            pieceOfNews.Text ?? throw new InvalidOperationException("A piece of news has to have text!"),
            pieceOfNews.Filter ?? string.Empty));
    }

    public async Task<IEnumerable<PieceOfNews>> Get(int userID)
    {
        var user = await _userProvider.Get(userID);
        
        var news = await _newsRepository.Get();

        return news
            .Where(pieceOfNews => ShouldBeShownTo(user, pieceOfNews.Filter))
            .Select(pieceOfNews => new PieceOfNews(
                pieceOfNews.Text ?? throw new InvalidOperationException("Piece of news has to have text!"),
                pieceOfNews.Filter));
    }
    
    private static bool ShouldBeShownTo(User? user, string? filter)
    {
        if (string.IsNullOrWhiteSpace(filter)) return true;

        if (user is null) return false;

        if (NewsFilterExpressionParser.TryParse(filter, out var expression, out var error))
        {
            return expression.ShouldBeVisibleTo(user);
        }

        throw new InvalidOperationException($"Filter cannot be parsed: {error}!");
    }
}