using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Cronos_Samples
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var configuration = GetConfiguration(args);

            Console.WriteLine("Building host.");

            var host = CreateHostBuilder(args, configuration).Build();

            Console.WriteLine("Building host complete.");

            Console.WriteLine("Starting app...");

            await host.RunAsync();

            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            IHostBuilder builder = new HostBuilder()
                .UseWindowsService()
                .ConfigureHostConfiguration((cfg) =>
                {
                    cfg.AddConfiguration(configuration);
                })
                .ConfigureAppConfiguration((hostingContext, cfg) =>
                {
                    cfg.AddConfiguration(configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IProcessingService, ProcessingService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            return builder;
        }

        private static IConfiguration GetConfiguration(string[] args)
        {
            Console.WriteLine(nameof(GetConfiguration));

            var builder = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);

            var config = builder.Build();

            return builder.Build();
        }
    }
}