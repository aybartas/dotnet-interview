using System.ComponentModel.DataAnnotations;

namespace Junior.AspNetCoreFundamentals.ConfigurationBasics;

/// <summary>
/// Strongly-typed settings for the section "Smtp". Bound from configuration (appsettings.json + environment variables + ...)
/// and validated with DataAnnotations at startup. Property names match the configuration keys (case-insensitive).
/// </summary>
public sealed class SmtpOptions
{
    /// <summary>Name of the configuration section: the keys are Smtp:Host, Smtp:Port, ...</summary>
    public const string SectionName = "Smtp";

    [Required]
    public string Host { get; set; } = "";

    [Range(1, 65535)]
    public int Port { get; set; } = 25;

    public bool UseSsl { get; set; }

    [Required, EmailAddress]
    public string From { get; set; } = "";

    /// <summary>A SECRET. Not in appsettings.json: it comes from user-secrets (dev) or the Smtp__Password env var (prod).</summary>
    public string? Password { get; set; }

    /// <summary>What the API may show: the password is never written out.</summary>
    public object ToMasked() => new
    {
        Host,
        Port,
        UseSsl,
        From,
        Password = string.IsNullOrEmpty(Password) ? "(not set)" : "********",
    };
}
