using Blazor_Maui_Svg_Samples.Data;
using Blazor_Maui_Svg_Samples.Code;
using Microsoft.Extensions.Logging;

namespace Blazor_Maui_Svg_Samples
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif


            var services = builder.Services;

            services.AddSingleton<ConsoleJsInterop>();
            services.AddTransient<HelperJsInterop>();
            services.AddSingleton<JSRuntimeService>();
            services.AddSingleton<IWindowSize, WindowSizeService>();

            // NOTE: If Singleton then throws. 'System.InvalidOperationException: 'Cannot invoke JavaScript outside of a WebView context.'' Maui.Blazor project
            services.AddScoped<BlazorJsInterop>();

            return builder.Build();
        }
    }
}
