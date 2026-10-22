namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section06_Scope
{
    private static readonly int _count = 100;            // field

    public static void Run()
    {
        // SCOPE: a variable lives in the nearest enclosing { } block where it is declared.
        {
            var inner = "only in this block";
            Console.WriteLine($"inside block: {inner}");
        }
        // Console.WriteLine(inner);                     // ✗ CS0103: inner does not exist in the current context

        // the for variable belongs to the loop
        for (var i = 0; i < 2; i++) { }
        // Console.WriteLine(i);                         // ✗ CS0103

        // SHADOWING: a same-named variable in an inner scope hides the outer one.
        // C# does not allow a local variable to shadow an outer LOCAL variable (CS0136); but shadowing a FIELD is allowed:
        var _count = 5;                                   // shadows the field → '_count' is now the local one
        Console.WriteLine($"local _count = {_count}, field = {Section06_Scope._count}   (the field is reached via the type name/this)");

        // A lambda CAPTURES the outer local variable (closure); the captured variable is read when the lambda runs.
        var factor = 3;
        Func<int, int> mul = n => n * factor;             // the outer 'factor' is CAPTURED (closure) — not shadowing
        Console.WriteLine($"closure: mul(4) = {mul(4)}");

        // DEFINITE ASSIGNMENT: a local variable must be assigned on every code path BEFORE it is READ (the compiler analyzes the flow).
        int result;
        if (Environment.TickCount64 >= 0)
            result = 1;
        else
            result = 2;
        Console.WriteLine($"definite assignment: result = {result}");     // valid because both branches assign it
        // int broken; if (cond) broken = 1; Console.WriteLine(broken);  // ✗ CS0165: use of unassigned local variable

        // an out parameter also counts as assigned when the call returns
        if (int.TryParse("42", out var parsed))
            Console.WriteLine($"out variable scope: parsed = {parsed}");
        Console.WriteLine($"parsed is visible on this line too (out var lives in the enclosing block): {parsed}");

        // pattern variable scope
        object o = "text";
        if (o is string text && text.Length > 2)
            Console.WriteLine($"the is-pattern variable is definitely assigned only if the condition is true: {text}");
    }
}
