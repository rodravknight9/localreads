using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LocalReads;
using LocalReads.Services;
using LocalReads.State;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5033") });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IOpenLibraryService, OpenLibraryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHttpRequest, HttpRequest>();
builder.Services.AddScoped<AppState>();
builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
