using BlazorWebAssemblyAppPWA_Svg_Samples;
using BlazorWebAssemblyAppPWA_Svg_Samples.Code;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var services = builder.Services;

services.AddSingleton<ConsoleJsInterop>();
services.AddTransient<HelperJsInterop>();
services.AddSingleton<JSRuntimeService>();

await builder.Build().RunAsync();
