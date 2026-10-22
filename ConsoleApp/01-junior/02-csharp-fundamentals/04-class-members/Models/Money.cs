namespace Junior.CSharpFundamentals.ClassMembers.Models;

/// <summary>
/// Value object: değişmez, değer eşitlikli, operatörlü. Operator overloading KURALLARI:
///   • ==  ve != BİRLİKTE tanımlanır (aksi halde CS0216); &lt; ve &gt;, &lt;= ve &gt;= da çift çift.
///   • == tanımlıysa Equals(object) ve GetHashCode da AYNI mantıkla override edilmeli (derleyici uyarır: CS0660/CS0661).
///   • implicit conversion = veri kaybı/exception YOK ise; explicit = kayıp/exception olabilirse.
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

    // Aritmetik: aynı para birimi şart
    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount, SameCurrency(a, b));
    public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount, SameCurrency(a, b));
    public static Money operator *(Money m, decimal factor) => new(m.Amount * factor, m.Currency);
    public static Money operator *(decimal factor, Money m) => m * factor;           // değişme özelliği için ikinci overload
    public static Money operator -(Money m) => new(-m.Amount, m.Currency);            // unary

    // Eşitlik: ==/!= BİRLİKTE, Equals/GetHashCode ile TUTARLI
    public static bool operator ==(Money a, Money b) => a.Equals(b);
    public static bool operator !=(Money a, Money b) => !a.Equals(b);
    public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;
    public override bool Equals(object? obj) => obj is Money m && Equals(m);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    // Karşılaştırma: çift çift
    public static bool operator <(Money a, Money b) => a.CompareTo(b) < 0;
    public static bool operator >(Money a, Money b) => a.CompareTo(b) > 0;
    public static bool operator <=(Money a, Money b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Money a, Money b) => a.CompareTo(b) >= 0;
    public int CompareTo(Money other)
    {
        SameCurrency(this, other);
        return Amount.CompareTo(other.Amount);
    }

    // Dönüşümler
    public static implicit operator Money(decimal amount) => new(amount, "TRY");     // kayıpsız → implicit (varsayılan para birimi TRY)
    public static explicit operator decimal(Money m) => m.Amount;                     // para birimi bilgisi KAYBOLUR → explicit

    public override string ToString() => $"{Amount:N2} {Currency}";

    private static string SameCurrency(Money a, Money b)
        => a.Currency == b.Currency
            ? a.Currency
            : throw new InvalidOperationException($"Farklı para birimleri işlenemez: {a.Currency} ve {b.Currency}");
}
