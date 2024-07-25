using System.ComponentModel;

namespace ParserCombinators.Frontend.ViewModels;

public interface IHasSelectedUserViewModel : INotifyPropertyChanged
{
    UserViewModel? SelectedUser { get; }
}