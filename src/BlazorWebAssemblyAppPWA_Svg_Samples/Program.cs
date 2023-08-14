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
services.AddSingleton<IWindowSize, WindowSizeService>();


// NOTE: If Singleton then throws. 'System.InvalidOperationException: 'Cannot invoke JavaScript outside of a WebView context.''
services.AddScoped<ConsoleJsInterop>();
services.AddScoped<BlazorJsInterop>();

await builder.Build().RunAsync();
