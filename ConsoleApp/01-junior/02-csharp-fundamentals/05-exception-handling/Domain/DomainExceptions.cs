namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

// ═════════ Custom exception hierarchy (domain) ═════════
// When is a custom exception warranted?
//   • When the caller needs to behave differently depending on the exception TYPE (catch (InsufficientFundsException) → tell the user "insufficient balance").
//   • When it needs to carry extra data (which account, how much is missing) — via a property instead of parsing the message string.
// When NOT?  A new type just for a different MESSAGE; or when a built-in framework type (ArgumentException, InvalidOperationException…) is enough.
//
// Rules: (1) the name ends with 'Exception', (2) derive from System.Exception (NOT from ApplicationException),
// (3) at least the standard ctors (message, message+inner), (4) extra info → read-only property, (5) keep the hierarchy simple (root → a few subtypes).

/// <summary>Root of all exceptions that represent a domain rule violation. The upper layer can tell a "business rule error" apart with a single catch.</summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
    protected DomainException(string message, Exception inner) : base(message, inner) { }

    /// <summary>Machine-readable error code (for API responses, logging, i18n).</summary>
    public abstract string Code { get; }
}

public sealed class InsufficientFundsException(string accountId, decimal balance, decimal requested)
    : DomainException($"Account {accountId}: balance {balance:N2}, requested {requested:N2}")
{
    public string AccountId { get; } = accountId;
    public decimal Balance { get; } = balance;
    public decimal Requested { get; } = requested;
    public decimal Shortfall => Requested - Balance;
    public override string Code => "INSUFFICIENT_FUNDS";
}

public sealed class OrderNotFoundException(int orderId) : DomainException($"Order not found: {orderId}")
{
    public int OrderId { get; } = orderId;
    public override string Code => "ORDER_NOT_FOUND";
}

public sealed class ValidationFailedException : DomainException
{
    public ValidationFailedException(IReadOnlyDictionary<string, string> errors)
        : base($"{errors.Count} validation error(s): {string.Join("; ", errors.Select(e => $"{e.Key}: {e.Value}"))}")
        => Errors = errors;

    public IReadOnlyDictionary<string, string> Errors { get; }
    public override string Code => "VALIDATION_FAILED";
}

/// <summary>When translating an infrastructure error (DB, network) into the domain language, KEEPS the original error as InnerException (no information loss).</summary>
public sealed class OrderRepositoryException(string message, Exception inner) : Exception(message, inner);
