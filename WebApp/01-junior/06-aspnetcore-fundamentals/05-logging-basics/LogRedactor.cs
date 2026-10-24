using System.Security.Cryptography;
using System.Text;

namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>Helpers that keep secrets and personal data (PII) out of logs. All values in this topic are FAKE.</summary>
public static class LogRedactor
{
    public const string Redacted = "[REDACTED]";

    /// <summary>demo.user@example.com -> d*******@example.com (enough to recognise, not enough to identify).</summary>
    public static string MaskEmail(string email)
    {
        var at = email.IndexOf('@');
        return at <= 1 ? Redacted : $"{email[0]}{new string('*', at - 1)}{email[at..]}";
    }

    /// <summary>4111111111111111 -> ************1111 (PCI-DSS allows at most the last 4 digits).</summary>
    public static string MaskCard(string pan) =>
        pan.Length <= 4 ? Redacted : new string('*', pan.Length - 4) + pan[^4..];

    /// <summary>A short one-way hash: lets you correlate "the same user" across log lines without storing who it is.</summary>
    public static string Fingerprint(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)))[..8].ToLowerInvariant();
}
