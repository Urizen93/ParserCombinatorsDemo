using System.Collections.ObjectModel;
using DynamicData.Binding;
using ParserCombinators.Frontend.Services;
using ParserCombinators.Model;

namespace ParserCombinators.Frontend.Models;

public sealed class AllUsers
{
    private readonly IHasAllUsers _service;
    private readonly ObservableCollectionExtended<KeyValuePair<int, User>> _users = new();

    public AllUsers(IHasAllUsers service)
    {
        _service = service;
        Users = new ReadOnlyObservableCollection<KeyValuePair<int, User>>(_users);
    }
    
    public ReadOnlyObservableCollection<KeyValuePair<int, User>> Users { get; }
    
    public async Task Refresh()
    {
        _users.Clear();
        _users.AddRange(await _service.Get());
    }
}