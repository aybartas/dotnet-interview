namespace Junior.AspNetCoreFundamentals.LoggingBasics;

public sealed record LoginAttempt(string Email, string Password, string BearerToken, string CardNumber);

/// <summary>Logs a login attempt twice: once the way you must NOT, once with redaction. FAKE data only.</summary>
public sealed class AccountService(ILogger<AccountService> logger)
{
    public static LoginAttempt FakeAttempt() => new(
        Email: "demo.user@example.com",
        Password: "P@ssw0rd-FAKE",
        BearerToken: "eyFAKE.TOKEN.NOT-REAL",
        CardNumber: "4111111111111111");   // well-known public test card number, not a real card

    public void LogLeaky(LoginAttempt attempt)
    {
        // ANTI-PATTERN: logs end up in files, dashboards, backups and third-party SaaS - for months, readable by many.
        logger.LogInformation("LEAKY login attempt: email={Email} password={Password} token={Token} card={Card}",
            attempt.Email, attempt.Password, attempt.BearerToken, attempt.CardNumber);
    }

    public void LogRedacted(LoginAttempt attempt)
    {
        // Passwords and tokens are never logged at all (not even masked); PII is masked or replaced by a fingerprint.
        logger.LogInformation("Redacted login attempt: email={Email} userFingerprint={Fingerprint} password={Password} token={Token} card={Card}",
            LogRedactor.MaskEmail(attempt.Email),
            LogRedactor.Fingerprint(attempt.Email),
            LogRedactor.Redacted,
            LogRedactor.Redacted,
            LogRedactor.MaskCard(attempt.CardNumber));
    }
}
