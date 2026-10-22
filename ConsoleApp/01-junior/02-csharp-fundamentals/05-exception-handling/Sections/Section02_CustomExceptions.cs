using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section02_CustomExceptions
{
    public static void Run()
    {
        Console.WriteLine("Standard hierarchy (abridged):");
        Console.WriteLine("   Exception");
        Console.WriteLine("   ├─ SystemException ── NullReferenceException, InvalidCastException, IndexOutOfRangeException, OutOfMemoryException…");
        Console.WriteLine("   │   └─ ArgumentException ── ArgumentNullException, ArgumentOutOfRangeException");
        Console.WriteLine("   ├─ InvalidOperationException ── ObjectDisposedException");
        Console.WriteLine("   ├─ NotSupportedException, NotImplementedException");
        Console.WriteLine("   ├─ IOException ── FileNotFoundException, DirectoryNotFoundException");
        Console.WriteLine("   └─ (your own domain exceptions: DomainException → InsufficientFundsException, OrderNotFoundException …)");

        Console.WriteLine("\nPick the right built-in type:");
        Console.WriteLine("   ArgumentNullException       → argument is null (ThrowIfNull)      ArgumentOutOfRangeException → value is out of range");
        Console.WriteLine("   InvalidOperationException   → the object can't do this in THIS state   NotSupportedException        → the operation isn't supported at all");

        Console.WriteLine("\nCustom domain exception: carries extra DATA, the caller decides by type:");
        try { Withdraw("ACC-1", balance: 100m, amount: 250m); }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"   catch InsufficientFundsException → to the user: 'Shortfall: {ex.Shortfall:N2}'  (Code={ex.Code})  ← no need to parse the message");
        }

        Console.WriteLine("\nThanks to the root type, the upper layer tells all business-rule errors apart with a SINGLE catch:");
        foreach (var action in new Action[]
                 {
                     () => throw new OrderNotFoundException(42),
                     () => throw new ValidationFailedException(new Dictionary<string, string> { ["email"] = "invalid", ["age"] = "negative" }),
                     () => throw new NullReferenceException("unexpected"),
                 })
        {
            try { action(); }
            catch (DomainException ex) { Console.WriteLine($"   [{ex.Code}] → translated to 4xx: {ex.Message}"); }
            catch (Exception ex) { Console.WriteLine($"   [UNEXPECTED] → translated to 500 and LOGGED: {ex.GetType().Name}"); }
        }

        Console.WriteLine("\nWhen translating an infrastructure error, KEEP the InnerException:");
        try { LoadOrder(1); }
        catch (OrderRepositoryException ex) { Console.WriteLine($"   {ex.Message}  ← inner cause: {ex.InnerException!.GetType().Name}: {ex.InnerException.Message}"); }

        Console.WriteLine("""

        Don't create many custom exception types: a type for cases where the caller will behave DIFFERENTLY, built-in types for the rest.
        Which layer throws what?  Domain → DomainExceptions · Infrastructure (repository) → wraps into its own type · Boundary (API/middleware) → translates to an HTTP code.
        """);
    }

    private static void Withdraw(string accountId, decimal balance, decimal amount)
    {
        if (amount > balance) throw new InsufficientFundsException(accountId, balance, amount);
    }

    private static void LoadOrder(int id)
    {
        try { throw new TimeoutException("database timeout"); }
        catch (TimeoutException ex)
        {
            throw new OrderRepositoryException($"Could not load order {id}", ex);       // wrap, keep the original as inner
        }
    }
}
