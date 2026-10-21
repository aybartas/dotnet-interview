namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

/// <summary>
/// Minimal Result türü: başarısızlığı DEĞER olarak döndürmek (exception fırlatmak yerine).
/// Beklenen iş kuralı başarısızlıkları için uygundur (kayıt bulunamadı, doğrulama başarısız, bakiye yetersiz).
/// Kavramsal giriş — Result pattern, Railway-oriented programming ve kütüphaneler → M11.6.
/// </summary>
public readonly record struct Result<T>
{
    private readonly T? _value;
    private readonly string? _error;

    private Result(T? value, string? error) { _value = value; _error = error; }

    public bool IsSuccess => _error is null;
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException($"Başarısız sonuç değer taşımaz: {_error}");
    public string Error => _error ?? throw new InvalidOperationException("Başarılı sonuçta hata yoktur");

    public static Result<T> Ok(T value) => new(value, null);
    public static Result<T> Fail(string error) => new(default, error);

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<string, TOut> onError) => IsSuccess ? onSuccess(_value!) : onError(_error!);
}

public static class AccountService
{
    /// <summary>Exception sürümü: yetersiz bakiye "istisnai" kabul edilir.</summary>
    public static decimal WithdrawOrThrow(decimal balance, decimal amount)
        => amount <= balance ? balance - amount : throw new InsufficientFundsException("ACC", balance, amount);

    /// <summary>Result sürümü: yetersiz bakiye BEKLENEN bir iş sonucu; çağıran DERLEYİCİ tarafından değerlendirmeye zorlanır (Match).</summary>
    public static Result<decimal> TryWithdraw(decimal balance, decimal amount)
        => amount <= balance ? Result<decimal>.Ok(balance - amount) : Result<decimal>.Fail($"Bakiye yetersiz (eksik {amount - balance:N2})");
}
