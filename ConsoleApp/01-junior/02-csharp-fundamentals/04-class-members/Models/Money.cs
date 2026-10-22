namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>
/// Value object: immutable, value-equal, with operators. Operator overloading RULES:
///   • ==  and != are defined TOGETHER (otherwise CS0216); likewise &lt; with &gt;, and &lt;= with &gt;=.
///   • If == is defined, Equals(object) and GetHashCode must be overridden with the SAME logic (the compiler warns: CS0660/CS0661).
///   • implicit conversion = if there is NO data loss/exception; explicit = if loss/exception is possible.
/// </summary>
public readonly struct Money : IEquatable<Money>, IComparable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    // Arithmetic: the same currency is required
    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount, SameCurrency(a, b));
    public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount, SameCurrency(a, b));
    public static Money operator *(Money m, decimal factor) => new(m.Amount * factor, m.Currency);
    public static Money operator *(decimal factor, Money m) => m * factor;           // second overload for commutativity
    public static Money operator -(Money m) => new(-m.Amount, m.Currency);            // unary

    // Equality: ==/!= TOGETHER, CONSISTENT with Equals/GetHashCode
    public static bool operator ==(Money a, Money b) => a.Equals(b);
    public static bool operator !=(Money a, Money b) => !a.Equals(b);
    public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;
    public override bool Equals(object? obj) => obj is Money m && Equals(m);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    // Comparison: in pairs
    public static bool operator <(Money a, Money b) => a.CompareTo(b) < 0;
    public static bool operator >(Money a, Money b) => a.CompareTo(b) > 0;
    public static bool operator <=(Money a, Money b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Money a, Money b) => a.CompareTo(b) >= 0;
    public int CompareTo(Money other)
    {
        SameCurrency(this, other);
        return Amount.CompareTo(other.Amount);
    }

    // Conversions
    public static implicit operator Money(decimal amount) => new(amount, "TRY");     // lossless → implicit (default currency is TRY)
    public static explicit operator decimal(Money m) => m.Amount;                     // the currency information is LOST → explicit

    public override string ToString() => $"{Amount:N2} {Currency}";

    private static string SameCurrency(Money a, Money b)
        => a.Currency == b.Currency
            ? a.Currency
            : throw new InvalidOperationException($"Cannot operate on different currencies: {a.Currency} and {b.Currency}");
}
