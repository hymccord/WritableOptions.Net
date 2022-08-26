namespace MauiExample;

/// <summary>The schema of appsettings.json &gt; "AppOption" section</summary>
public class AppOption
{
    /// <summary>Last DateTime when this app was launched</summary>
    public DateTime LastChanged { get; set; }

    /// <summary>Guid</summary>
    public string? ApiKey { get; set; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{{ {nameof(LastChanged)}: {LastChanged}, {nameof(ApiKey)} : {ApiKey} }}";
}
