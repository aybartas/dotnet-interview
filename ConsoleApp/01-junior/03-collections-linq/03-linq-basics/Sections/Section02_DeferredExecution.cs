namespace Junior.CollectionsLinq.LinqBasics;

public static class Section02_DeferredExecution
{
    public static void Run()
    {
        Console.WriteLine("1) DEFINING a query is not running it (deferred execution):");
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        var query = numbers.Where(n => { Console.WriteLine($"      Where ran: {n}"); return n % 2 == 0; });
        Console.WriteLine("   query defined → no 'Where ran' lines at all; nothing has been executed yet.");
        Console.WriteLine("   calling ToList() (immediate):");
        var snapshot = query.ToList();
        Console.WriteLine($"   result: [{string.Join(",", snapshot)}]");

        Console.WriteLine("\n2) ⭐ TRAP: a query runs AGAIN on every ENUMERATION and sees the source as it is AT THAT MOMENT:");
        var data = new List<int> { 1, 2, 3 };
        var evens = data.Where(n => n % 2 == 0);                      // has not run yet
        data.Add(4); data.Add(6);                                      // the source changed AFTER the definition
        Console.WriteLine($"   4 and 6 were added after the definition → evens = [{string.Join(",", evens)}]   (not 2 but 2,4,6: the query ran just now)");
        var frozen = data.Where(n => n % 2 == 0).ToList();             // immediate: a snapshot
        data.Add(8);
        Console.WriteLine($"   ToList() snapshot = [{string.Join(",", frozen)}] · live query evens = [{string.Join(",", evens)}]");

        Console.WriteLine("\n3) Every enumeration re-runs the source + lambda (if it is expensive you pay twice, side effects happen twice):");
        var calls = 0;
        var expensive = Enumerable.Range(1, 3).Select(n => { calls++; return n * n; });
        _ = expensive.Count();     // 1st enumeration
        _ = expensive.Sum();       // 2nd enumeration
        _ = expensive.Max();       // 3rd enumeration
        Console.WriteLine($"   3 different terminal operators → the lambda ran {calls} times in total (not once, but 3 × 3 elements)");
        calls = 0;
        var materialized = expensive.ToList();
        _ = materialized.Count; _ = materialized.Sum(); _ = materialized.Max();
        Console.WriteLine($"   materialized once with ToList() → {calls} executions (only once, for 3 elements)");

        Console.WriteLine("\n4) Streaming: consecutive deferred operators advance the element ONE BY ONE (no intermediate list between them):");
        var pipeline = Enumerable.Range(1, 4)
            .Where(n => { Console.WriteLine($"      Where({n})"); return n % 2 == 0; })
            .Select(n => { Console.WriteLine($"      Select({n})"); return n * 10; });
        Console.WriteLine("   the order while iterating with foreach (each element passes through Where → Select and is yielded):");
        foreach (var x in pipeline) Console.WriteLine($"   → result {x}");

        Console.WriteLine("\n5) Buffering operators (OrderBy, GroupBy, Reverse, Distinct partially…): read the WHOLE source when the FIRST result is requested:");
        var ordered = Enumerable.Range(1, 3).Select(n => { Console.WriteLine($"      source read: {n}"); return 4 - n; }).OrderBy(n => n);
        Console.WriteLine("   OrderBy defined (nothing read yet). Requesting the first element (First):");
        Console.WriteLine($"   First() = {ordered.First()}  ← the WHOLE source was read and sorted to produce the first element");

        Console.WriteLine("\n6) Errors are deferred too (and are caught at definition time if the source is null):");
        var lazyBug = new List<int> { 1, 0 }.Select(n => 10 / n);      // the division by 0 blows up while running, not at query DEFINITION
        Console.WriteLine("   Select(10 / n) defined: no error");
        try { _ = lazyBug.ToList(); }
        catch (DivideByZeroException) { Console.WriteLine("   ToList() → DivideByZeroException (the error appeared at enumeration time; the stack trace doesn't show the definition site)"); }

        Console.WriteLine("""

        SUMMARY ⭐
          DEFERRED:   Where, Select, SelectMany, Take, Skip, Zip, Concat, OrderBy*, GroupBy*, Join*, Distinct, Chunk, …   (* buffer everything, but still at enumeration TIME)
          IMMEDIATE:  ToList, ToArray, ToDictionary, ToLookup, ToHashSet · Count, Sum, Average, Min, Max, Aggregate · First, Single, Last, ElementAt · Any, All, Contains
        Side effects:  do not put side-effecting code (logging, counters, DB writes) inside a query; how many times and when it runs depends on enumeration.
        Rule:  if you will use the result more than once, the source may change, or the computation is expensive → MATERIALIZE with ToList()/ToArray().
               otherwise leaving it deferred saves memory and unnecessary intermediate collections.
        """);
    }
}
