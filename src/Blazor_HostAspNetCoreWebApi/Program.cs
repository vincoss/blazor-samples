using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddSpaStaticFiles(configuration => { configuration.RootPath = "one"; });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.UseContentRoot(AppContext.BaseDirectory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "one";
});

app.Run();


//dotnet Blazor_HostAspNetCoreWebApi.dll --WebRoot C:\_Documents\_Dev\GitHub\FL\.NET\Blazor\blazor-samples\src\Blazor_HostAspNetCoreWebApi\bin\Debug\net6.0"
//dotnet Blazor_HostAspNetCoreWebApi.dll --WebRoot C:\_Documents\_Dev\GitHub\FL\.NET\Blazor\blazor-samples\src\Blazor_HostAspNetCoreWebApi\bin\Debug\net6.0\one"
//dotnet Blazor_HostAspNetCoreWebApi.dll --WebRoot C:\_Documents\_Dev\GitHub\FL\.NET\Blazor\blazor-samples\src\Blazor_HostAspNetCoreWebApi\bin\Debug\net6.0\wwwroot"