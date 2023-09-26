global using MagnificentSuperHeroes.WAApp.SuperHeroesService;
global using MagnificentSuperHeroes.WAApp.Models;

using MagnificentSuperHeroes.WAApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ISuperHeroesService, SuperHeroesService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
