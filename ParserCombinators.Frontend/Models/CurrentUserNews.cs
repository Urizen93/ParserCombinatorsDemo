using System.Collections.ObjectModel;
using DynamicData.Binding;
using ParserCombinators.Model;
using ParserCombinators.Frontend.Services;

namespace ParserCombinators.Frontend.Models;

public sealed class CurrentUserNews
{
    private readonly IHasNews _service;
    private readonly ObservableCollectionExtended<PieceOfNews> _news = new();

    public CurrentUserNews(IHasNews service)
    {
        _service = service;
        News = new ReadOnlyObservableCollection<PieceOfNews>(_news);
    }

    public ReadOnlyObservableCollection<PieceOfNews> News { get; }
    
    public async Task Refresh(int? maybeUserID)
    {
        _news.Clear();
        _news.AddRange(maybeUserID is { } userID
            ? await _service.GetForUser(userID)
            : await _service.Get());
    }
}