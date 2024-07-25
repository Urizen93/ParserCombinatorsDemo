using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class SingleSelectionUserViewModel : ObservableObject, IHasSelectedUserViewModel, IDisposable
{
    private readonly UsersViewModel _usersViewModel;
    private readonly IDisposable _cleanup;
    private bool _isLoading;
    private UserViewModel? _selectedUser;

    public SingleSelectionUserViewModel(UsersViewModel usersViewModel)
    {
        _usersViewModel = usersViewModel;
        _cleanup = ObserveChildren(out var users);
        Users = users;
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set => SetField(ref _isLoading, value);
    }

    public ReadOnlyObservableCollection<Selectable<UserViewModel>> Users { get; }

    public UserViewModel? SelectedUser
    {
        get => _selectedUser;
        private set => SetField(ref _selectedUser, value);
    }

    public Task Refresh() => _usersViewModel.Refresh();

    private void ClearSelection()
    {
        foreach (var user in Users.Where(user => user.IsSelected))
            user.IsSelected = false;
    }

    private CompositeDisposable ObserveChildren(out ReadOnlyObservableCollection<Selectable<UserViewModel>> users) => new(
        ProjectSelectableUsers(out users),
        ClearSelectionWhenUserIsSelected(users),
        SyncSelectedUser(users),
        SyncIsLoading(),
        _usersViewModel);

    private IDisposable ProjectSelectableUsers(out ReadOnlyObservableCollection<Selectable<UserViewModel>> users) =>
        _usersViewModel.Users
            .ToObservableChangeSet()
            .Select(user => new Selectable<UserViewModel>(user))
            .Bind(out users)
            .Subscribe();
    
    private IDisposable ClearSelectionWhenUserIsSelected(ReadOnlyObservableCollection<Selectable<UserViewModel>> users) => users
        .ToObservableChangeSet()
        .SubscribeMany(userViewModel => userViewModel
            .WhenIsSelectedChanging
            .Where(property => property.Value)
            .Subscribe(_ => ClearSelection()))
        .Subscribe();

    private IDisposable SyncSelectedUser(ReadOnlyObservableCollection<Selectable<UserViewModel>> users) => users
        .ToObservableChangeSet()
        .SubscribeMany(userViewModel => userViewModel
            .WhenPropertyChanged(user => user.IsSelected)
            .Subscribe(_ => SelectedUser = users.SingleOrDefault(user => user.IsSelected)?.Value))
        .Subscribe();
    
    private IDisposable SyncIsLoading() => _usersViewModel
        .WhenValueChanged(users => users.IsLoading)
        .Subscribe(value => IsLoading = value);
    
    public void Dispose() => _cleanup.Dispose();
}