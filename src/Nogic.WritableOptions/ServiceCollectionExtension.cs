using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Nogic.WritableOptions;

/// <summary>
/// Extension methods for adding configuration related writable options services to the DI container.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>Default JSON setting file name</summary>
    private const string DefaultFileName = "appsettings.json";

    /// <summary>
    /// Registers a writable configuration instance which <typeparamref name="TOptions"/> will bind against.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being configured.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="section">The configuration being bound.</param>
    /// <param name="file">Setting JSON file name. (should be placed in content-root folder or current folder)</param>
    public static IServiceCollection ConfigureWritable<TOptions>(
        this IServiceCollection services,
        IConfigurationSection section,
        string file = DefaultFileName) where TOptions : class, new()
        => services.Configure<TOptions>(section)
            .AddTransient<IWritableOptions<TOptions>>(provider =>
            {
                var environment = provider.GetService<IHostEnvironment>();
                string jsonFilePath = environment?.ContentRootFileProvider.GetFileInfo(file).PhysicalPath
                    ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

                var configuration = provider.GetService<IConfigurationRoot>();
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>();
                return new JsonWritableOptions<TOptions>(jsonFilePath, section.Key, options, configuration);
            });

    /// <summary>
    /// Registers a writable configuration instance which <typeparamref name="TOptions"/> will bind against.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being configured.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="section">The configuration being bound.</param>
    /// <param name="directoryPath">The path to the directory containing <paramref name="file"/>.</param>
    /// <param name="file">Setting JSON file name. (should be placed in <paramref name="directoryPath"/>)</param>
    public static IServiceCollection ConfigureWritableWithExplicitPath<TOptions>(
        this IServiceCollection services,
        IConfigurationSection section,
        string directoryPath,
        string file = DefaultFileName) where TOptions : class, new()
        => services.Configure<TOptions>(section)
            .AddTransient<IWritableOptions<TOptions>>(provider =>
            {
                string jsonFilePath = Path.Combine(directoryPath, file);
                var configuration = provider.GetService<IConfigurationRoot>();
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>();
                return new JsonWritableOptions<TOptions>(jsonFilePath, section.Key, options, configuration);
            });
}
