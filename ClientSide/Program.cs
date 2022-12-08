using ClientSide;
using ClientSide.AuthProviders;
using ClientSide.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/") });
builder.Services.AddScoped<IProductHttpService,ProductHttpService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
await builder.Build().RunAsync();
