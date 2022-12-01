using ClientSide;
using ClientSide.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/") });
builder.Services.AddScoped<IProductHttpService,ProductHttpService>();       
await builder.Build().RunAsync();
