using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;
using ParserCombinators.Model;
using ParserCombinators.Frontend.Models;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class UsersViewModel : ObservableObject, IDisposable
{
    private readonly AllUsers _users;
    private readonly IDisposable _cleanup;
    private bool _isLoading;

    public UsersViewModel(AllUsers users, UserFilterViewModel filter)
    {
        _users = users;
        Filter = filter;
        _cleanup = ObserveUsers(out var projectedUsers);
        Users = projectedUsers;
    }

    public UserFilterViewModel Filter { get; }

    public ReadOnlyObservableCollection<UserViewModel> Users { get; }

    public bool IsLoading
    {
        get => _isLoading;
        private set => SetField(ref _isLoading, value);
    }

    public async Task Refresh()
    {
        IsLoading = true;
        using var _ = Disposable.Create(() => IsLoading = false);

        await _users.Refresh();
    }
    
    private IDisposable ObserveUsers(out ReadOnlyObservableCollection<UserViewModel> users)
    {
        var whenFilterChanged = Filter
            .WhenValueChanged(filterViewModel => filterViewModel.Expression)
            .Select<NewsFilterExpression?, Func<UserViewModel, bool>>(expression =>
                expression is not null
                    ? userViewModel => expression.ShouldBeVisibleTo(userViewModel.User)
                    : _ => true);

        return _users.Users
            .ToObservableChangeSet()
            .Select(pair => new UserViewModel(pair.Key, pair.Value))
            .Filter(whenFilterChanged)
            .Sort(Comparer<UserViewModel>.Create((left, right) => left.UserID.CompareTo(right.UserID)))
            .Bind(out users)
            .Subscribe();
    }

    public void Dispose() => _cleanup.Dispose();
}