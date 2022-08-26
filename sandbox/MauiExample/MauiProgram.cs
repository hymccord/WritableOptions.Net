using Microsoft.Extensions.Configuration;
using Nogic.WritableOptions;

namespace MauiExample;

/// <summary>Bootstrap for .NET MAUI</summary>
public static class MauiProgram
{
    /// <summary>Entry point</summary>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // MAUI
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

        // Configuration
        builder.Configuration.AddJsonFile("appsettings.json", true, true);

        // DI
        builder.Services
            .ConfigureWritable<AppOption>(builder.Configuration.GetSection(nameof(AppOption)))
            .AddSingleton<MainPage>();

        return builder.Build();
    }
}
