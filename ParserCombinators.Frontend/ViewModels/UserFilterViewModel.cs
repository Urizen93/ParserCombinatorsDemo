using System.Reactive.Linq;
using DynamicData.Binding;
using ParserCombinators.Model;
using ParserCombinators.Frontend.Models;

namespace ParserCombinators.Frontend.ViewModels;

public sealed class UserFilterViewModel : ObservableObject
{
    private string? _input;
    private NewsFilterExpression? _expression;
    private string? _error;

    public UserFilterViewModel(UserFilterParser userFilterParser) => this
        .WhenValueChanged(viewModel => viewModel.Input)
        .Throttle(TimeSpan.FromSeconds(.3))
        .Subscribe(input =>
        {
            if (userFilterParser.TryParseFilter(input ?? string.Empty, out var filter, out var error))
            {
                Expression = filter;
                Error = error;
            }
            else Error = error;
        });

    public string? Input
    {
        get => _input;
        set => SetField(ref _input, value);
    }

    public NewsFilterExpression? Expression
    {
        get => _expression;
        private set => SetField(ref _expression, value);
    }

    public string? Error
    {
        get => _error;
        private set => SetField(ref _error, value);
    }
}