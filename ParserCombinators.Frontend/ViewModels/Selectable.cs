using System.ComponentModel;
using System.Reactive.Linq;
using DynamicData.Binding;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class Selectable<T> : ObservableObject, INotifyPropertyChanging
{
    private bool _isSelected;
    
    public Selectable(T value)
    {
        Value = value;
        WhenIsSelectedChanging = Observable
            .FromEventPattern<PropertyChangingEventHandler, PropertyChangingEventArgs>(
                handler => PropertyChanging += handler,
                handler => PropertyChanging -= handler)
            .Where(pattern => pattern.EventArgs.PropertyName == nameof(IsSelected))
            .Select(_ => new PropertyValue<Selectable<T>, bool>(this, !IsSelected));
    }

    public event PropertyChangingEventHandler? PropertyChanging;

    public T Value { get; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(IsSelected)));
            
            SetField(ref _isSelected, value);
        }
    }

    public IObservable<PropertyValue<Selectable<T>, bool>> WhenIsSelectedChanging { get; }

    public void Toggle() => IsSelected = !IsSelected;
}