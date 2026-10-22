namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

/// <summary>
/// Minimal Result type: returning a failure as a VALUE (instead of throwing an exception).
/// Suitable for expected business-rule failures (record not found, validation failed, insufficient balance).
/// Conceptual introduction — Result pattern, Railway-oriented programming and libraries → M11.6.
/// </summary>
public readonly record struct Result<T>
{
    private readonly T? _value;
    private readonly string? _error;

    private Result(T? value, string? error) { _value = value; _error = error; }

    public bool IsSuccess => _error is null;
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException($"A failed result carries no value: {_error}");
    public string Error => _error ?? throw new InvalidOperationException("A successful result has no error");

    public static Result<T> Ok(T value) => new(value, null);
    public static Result<T> Fail(string error) => new(default, error);

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<string, TOut> onError) => IsSuccess ? onSuccess(_value!) : onError(_error!);
}

public static class AccountService
{
    /// <summary>Exception version: insufficient balance is treated as "exceptional".</summary>
    public static decimal WithdrawOrThrow(decimal balance, decimal amount)
        => amount <= balance ? balance - amount : throw new InsufficientFundsException("ACC", balance, amount);

    /// <summary>Result version: insufficient balance is an EXPECTED business outcome; the caller is forced by the COMPILER to handle it (Match).</summary>
    public static Result<decimal> TryWithdraw(decimal balance, decimal amount)
        => amount <= balance ? Result<decimal>.Ok(balance - amount) : Result<decimal>.Fail($"Insufficient balance (short by {amount - balance:N2})");
}
