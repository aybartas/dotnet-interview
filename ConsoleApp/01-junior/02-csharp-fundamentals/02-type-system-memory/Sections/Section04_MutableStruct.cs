namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section04_MutableStruct
{
    public static void Run()
    {
        Console.WriteLine("Mutable struct = the mistake of 'writing to a copy and thinking the original changed'.\n");

        // PITFALL 1: the List<T> indexer returns a COPY of the value
        var list = new List<Counter> { new() };
        // list[0].Value++;                      // ✗ CS1612: Cannot modify the return value because it is not a variable (the compiler protects you)
        var copy = list[0];                      // copy
        copy.Value++;
        Console.WriteLine($"1) incremented the copy of list[0] → list[0].Value = {list[0].Value} (unchanged!), copy.Value = {copy.Value}");
        list[0] = copy;                          // the right way: copy-modify-assign back
        Console.WriteLine($"   after assigning back  → list[0].Value = {list[0].Value}");

        // PITFALL 2: the foreach variable is a copy too
        var items = new[] { new Counter(), new Counter() };
        foreach (var item in items) { var tmp = item; tmp.Value = 10; }
        Console.WriteLine($"2) modified the copy inside foreach → array: {items[0].Value}, {items[1].Value} (the array element did NOT change)");
        for (var i = 0; i < items.Length; i++) items[i].Value = 10;   // the array indexer gives a REFERENCE → this works
        Console.WriteLine($"   with for + array indexer → array: {items[0].Value}, {items[1].Value} (arrays are special: elements can be modified in place)");

        // PITFALL 3: a struct in a readonly field — a method call works on a DEFENSIVE COPY (silently does nothing)
        var holder = new Holder();
        holder.IncrementReadonlyField();
        Console.WriteLine($"3) Increment() on a struct in a readonly field → Value = {holder.ReadonlyCounterValue} (still 0: defensive copy)");

        // PITFALL 4: a struct method modifies itself, but on the copy
        var c = new Counter();
        IncrementCopy(c);
        Console.WriteLine($"4) struct passed to a method → c.Value = {c.Value} (unchanged)");

        Console.WriteLine("""

        FIX: make structs IMMUTABLE → 'readonly struct' / 'readonly record struct'.
        If you need a change, produce a new copy (with expression / new instance). The compiler doesn't make defensive copies either → faster.
        """);

        var money = new ImmutableMoney(10m, "TRY");
        var more = money with { Amount = 15m };
        Console.WriteLine($"immutable: {money} → with → {more}");
    }

    private static void IncrementCopy(Counter c) => c.Value++;

    private struct Counter
    {
        public int Value;
        public void Increment() => Value++;      // 'mutating' method
    }

    private sealed class Holder
    {
        private readonly Counter _counter = new();
        public void IncrementReadonlyField() => _counter.Increment();   // readonly field → works on a copy
        public int ReadonlyCounterValue => _counter.Value;
    }

    private readonly record struct ImmutableMoney(decimal Amount, string Currency);
}
