using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using DynamicData.Binding;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class NewsBrowserViewModel : ObservableObject, IDisposable
{
    private readonly CompositeDisposable _cleanup;
    
    public NewsBrowserViewModel(
        SingleSelectionUserViewModel userImpersonation,
        NewsViewModel news)
    {
        UserImpersonation = userImpersonation;
        News = news;

        _cleanup = new CompositeDisposable(
            ObserveImpersonatedUser(),
            UserImpersonation);
    }

    public SingleSelectionUserViewModel UserImpersonation { get; }

    public NewsViewModel News { get; }
    
    private IDisposable ObserveImpersonatedUser() => UserImpersonation
        .WhenValueChanged(selection => selection.SelectedUser, false)
        .Throttle(TimeSpan.FromSeconds(.1))
        .Select(selectedUser => selectedUser?.UserID)
        .SelectMany(selectedUserID => News.Refresh(selectedUserID).ToObservable())
        .Subscribe();

    public void Dispose() => _cleanup.Dispose();
}