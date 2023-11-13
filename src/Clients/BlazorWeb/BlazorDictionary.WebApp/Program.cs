using BlazorDictionary.WebApp;
using BlazorDictionary.WebApp.Infrastructure.Auth;
using BlazorDictionary.WebApp.Infrastructure.Services;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080"); // Base adres burasý olacak.
}).AddHttpMessageHandler<AuthTokenHandler>(); 

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebApiClient");
}); //ToDo: AuthTokenHandler will be here.

builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IFavService, FavService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
