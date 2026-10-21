namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

// ═════════ Custom exception hiyerarşisi (domain) ═════════
// Ne zaman custom exception?
//   • Çağıranın exception TİPİNE göre farklı davranması gerekiyorsa (catch (InsufficientFundsException) → kullanıcıya "bakiye yetersiz").
//   • Ek veri taşıması gerekiyorsa (hangi hesap, ne kadar eksik) — mesajı string ayrıştırmak yerine property ile.
// Ne zaman DEĞİL?  Yeni bir tip yalnızca farklı bir MESAJ içinse; framework'ün hazır tipi (ArgumentException, InvalidOperationException…) yeterliyse.
//
// Kurallar: (1) adı 'Exception' ile biter, (2) System.Exception'dan (ApplicationException'dan DEĞİL) türet,
// (3) en az standart ctor'lar (mesaj, mesaj+inner), (4) ek bilgi → salt okunur property, (5) hiyerarşiyi basit tut (kök → birkaç alt tip).

/// <summary>Domain kurallarının ihlalini temsil eden tüm exception'ların kökü. Üst katman tek catch ile "iş kuralı hatası"nı ayırt edebilir.</summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
    protected DomainException(string message, Exception inner) : base(message, inner) { }

    /// <summary>Makine okunur hata kodu (API yanıtı, loglama, i18n için).</summary>
    public abstract string Code { get; }
}

public sealed class InsufficientFundsException(string accountId, decimal balance, decimal requested)
    : DomainException($"Hesap {accountId}: bakiye {balance:N2}, istenen {requested:N2}")
{
    public string AccountId { get; } = accountId;
    public decimal Balance { get; } = balance;
    public decimal Requested { get; } = requested;
    public decimal Shortfall => Requested - Balance;
    public override string Code => "INSUFFICIENT_FUNDS";
}

public sealed class OrderNotFoundException(int orderId) : DomainException($"Sipariş bulunamadı: {orderId}")
{
    public int OrderId { get; } = orderId;
    public override string Code => "ORDER_NOT_FOUND";
}

public sealed class ValidationFailedException : DomainException
{
    public ValidationFailedException(IReadOnlyDictionary<string, string> errors)
        : base($"{errors.Count} doğrulama hatası: {string.Join("; ", errors.Select(e => $"{e.Key}: {e.Value}"))}")
        => Errors = errors;

    public IReadOnlyDictionary<string, string> Errors { get; }
    public override string Code => "VALIDATION_FAILED";
}

/// <summary>Altyapı hatasını (DB, ağ) domain diline çevirirken orijinal hatayı InnerException olarak SAKLAR (bilgi kaybı yok).</summary>
public sealed class OrderRepositoryException(string message, Exception inner) : Exception(message, inner);
