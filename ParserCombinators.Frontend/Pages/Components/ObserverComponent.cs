using System.ComponentModel;
using System.Reactive.Disposables;
using DynamicData.Binding;
using Microsoft.AspNetCore.Components;

namespace ParserCombinators.Frontend.Pages.Components;

public abstract class ObserverComponent<T> : ComponentBase, IDisposable
    where T : class, INotifyPropertyChanged
{
    private readonly SerialDisposable _serial = new();

    [Parameter, EditorRequired]
    public required T ViewModel { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _serial.Disposable = ViewModel
            .WhenAnyPropertyChanged()
            .Subscribe(_ => StateHasChanged());
    }

    public void Dispose() => _serial.Dispose();
}