using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section08_ExceptionVsResult
{
    public static void Run()
    {
        Console.WriteLine("The same business rule in two forms:\n");

        Console.WriteLine("A) Exception: the caller is NOT FORCED to write try/catch (may forget) and the signature doesn't reveal the error");
        try { _ = AccountService.WithdrawOrThrow(balance: 100m, amount: 250m); }
        catch (InsufficientFundsException ex) { Console.WriteLine($"   caught: {ex.Message}"); }

        Console.WriteLine("\nB) Result: the signature says 'may fail'; the caller must handle both cases (Match)");
        var ok = AccountService.TryWithdraw(100m, 40m);
        var fail = AccountService.TryWithdraw(100m, 250m);
        Console.WriteLine($"   {ok.Match(v => $"remaining balance {v:N2}", e => $"error: {e}")}");
        Console.WriteLine($"   {fail.Match(v => $"remaining balance {v:N2}", e => $"error: {e}")}");

        Console.WriteLine("""

        Which one when?
        ┌──────────────────────┬──────────────────────────────────────────┬───────────────────────────────────────────────────┐
        │                      │ Exception                                │ Result / Try pattern / null                       │
        ├──────────────────────┼──────────────────────────────────────────┼───────────────────────────────────────────────────┤
        │ Kind of failure      │ Unexpected: programmer/environment error │ EXPECTED business outcome (validation, not found) │
        │ Happens often?       │ Rarely                                   │ Can be part of the normal flow                    │
        │ Performance          │ Throwing is expensive                    │ Cheap (an ordinary return value)                  │
        │ Forces the caller    │ No (can be forgotten)                    │ Yes (the type requires handling it)               │
        │ In a deep call chain │ Propagates up automatically              │ Must be carried at every level (Map/Bind)         │
        │ Example              │ DB connection lost, bug, null            │ Parse, "user not found", insufficient balance     │
        └──────────────────────┴──────────────────────────────────────────┴───────────────────────────────────────────────────┘
        In practice: framework/infrastructure and truly exceptional situations → exception; business-rule outcomes → Result or the Try pattern (int.TryParse).
        They can also be combined: the domain returns Result, the outermost layer (API) translates it to an HTTP code if needed; unexpected errors stay exceptions.
        Result pattern, ROP, the OneOf/ErrorOr libraries and the debate around them → M11.6.
        """);
    }
}
