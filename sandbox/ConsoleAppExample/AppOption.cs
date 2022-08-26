using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace ConsoleAppExample;

/// <summary>The schema of appsettings.json &gt; "ConsoleAppExample" section</summary>
public partial class AppOption
{
    /// <summary>Last DateTime when this app was launched</summary>
    public DateTime LastLaunchedAt { get; set; }

    /// <summary>Guid</summary>
    public string? ApiKey { get; set; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{{ {nameof(LastLaunchedAt)}: {LastLaunchedAt}, {nameof(ApiKey)} : {ApiKey} }}";

    /// <summary>Write infomation log to <paramref name="logger"/></summary>
    /// <param name="logger">Logger</param>
    /// <param name="option"><see cref="AppOption"/> to log</param>
    /// <param name="name">Argument name of <paramref name="option"/>.</param>
    /// <remarks>This method is auto-implemented by Microsoft.Extensions.Logging.</remarks>
    [LoggerMessage(0, LogLevel.Information, "{Name}: {Option}")]
    public static partial void LogInfomation(
        ILogger logger,
        AppOption option,
        [CallerArgumentExpression("option")] string? name = null);
}
