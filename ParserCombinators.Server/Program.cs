using ParserCombinators.Server.Components;
using ParserCombinators.Server.DataLayer;
using ParserCombinators.Server.DataLayer.Repositories;
using ParserCombinators.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services
    .AddDbContext<NewsContext>()
    .AddTransient<IUserRepository, UserRepository>()
    .AddTransient<INewsRepository, NewsRepository>()
    .AddTransient<IHasAllNews, NewsService>()
    .AddTransient<IHasUserNews, NewsService>()
    .AddTransient<IMapUsers, UserMapper>()
    .AddTransient<IHasUser, UserService>()
    .AddTransient<IHasAllUsers, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ParserCombinators.Frontend._Imports).Assembly);

app.MapGet("api/users", (IHasAllUsers service) => service.Get());
app.MapGet("api/users/{userID:int}/news", (IHasUserNews service, int userID) => service.Get(userID));
app.MapGet("api/news", (IHasAllNews service) => service.GetAll());

using (var scope = app.Services.CreateScope())
    await scope.ServiceProvider
        .GetRequiredService<NewsContext>()
        .Database
        .EnsureCreatedAsync();

await app.RunAsync();