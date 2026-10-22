namespace Junior.AsyncBasics.Shared;

/// <summary>Simple, deterministic computation (prime counting) used as the "CPU-bound work" in the demos. It really keeps the CPU busy.</summary>
public static class Cpu
{
    public static int CountPrimes(int upTo)
    {
        var count = 0;
        for (var n = 2; n <= upTo; n++)
        {
            var isPrime = true;
            for (var d = 2; (long)d * d <= n; d++)
                if (n % d == 0) { isPrime = false; break; }
            if (isPrime) count++;
        }
        return count;
    }

    public static string Tid => $"T{Environment.CurrentManagedThreadId}";
}
