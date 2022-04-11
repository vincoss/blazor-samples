using Fundamentals_BlazorWebAssemblyApp;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using Fundamentals_BlazorWebAssemblyApp.Interfaces;
using Fundamentals_BlazorWebAssemblyApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Read config file into configuration JSON file
var http = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

builder.Services.AddScoped(sp => http);

using var response = await http.GetAsync("cars.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);

// Memory Configuration Source
var vehicleData = new Dictionary<string, string>()
{
    { "color", "blue" },
    { "type", "car" },
    { "wheels:count", "3" },
    { "wheels:brand", "Blazin" },
    { "wheels:brand:type", "rally" },
    { "wheels:year", "2008" },
};

var memoryConfig = new MemoryConfigurationSource { InitialData = vehicleData };
builder.Configuration.Add(memoryConfig);

//builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

// Dependency injection
builder.Services.AddHttpClient();
builder.Services.AddTransient<IDataService, DataService>();

await builder.Build().RunAsync();
