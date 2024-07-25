using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ParserCombinators.Frontend.Clients;
using ParserCombinators.Frontend.Models;
using ParserCombinators.Frontend.Services;
using ParserCombinators.Frontend.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(_ =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7291/")
    })
    .AddTransient<IServerClient, ServerClient>()
    .AddTransient<IHasAllUsers, UserService>()
    .AddTransient<IHasNews, NewsService>()
    .AddTransient<AllUsers>()
    .AddTransient<CurrentUserNews>()
    .AddTransient<UserFilterParser>()
    .AddTransient<UsersViewModel>()
    .AddTransient<SingleSelectionUserViewModel>()
    .AddTransient<NewsViewModel>()
    .AddTransient<NewsBrowserViewModel>()
    .AddTransient<UserFilterViewModel>()
    .AddMudServices();

await builder.Build().RunAsync();