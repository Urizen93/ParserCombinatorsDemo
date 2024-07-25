using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using ParserCombinators.Model;
using ParserCombinators.Frontend.Models;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class NewsViewModel : ObservableObject
{
    private readonly CurrentUserNews _news;
    private bool _isLoading;

    public NewsViewModel(CurrentUserNews news) => _news = news;

    public ReadOnlyObservableCollection<PieceOfNews> News => _news.News;
    
    public bool IsLoading
    {
        get => _isLoading;
        private set => SetField(ref _isLoading, value);
    }

    public async Task Refresh(int? impersonatedUserID = null)
    {
        IsLoading = true;
        using var _ = Disposable.Create(() => IsLoading = false);

        await _news.Refresh(impersonatedUserID);
    }
}