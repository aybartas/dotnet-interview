namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section02_Lambdas
{
    public static void Run()
    {
        Console.WriteLine("Lambda expression: an unnamed (anonymous) function; the left of `=>` is the parameters, the right is the body.\n");

        // EXPRESSION lambda: the body is a SINGLE expression, the return value is implicit
        Func<int, int> square = x => x * x;
        // STATEMENT lambda: curly braces, multiple statements, explicit return
        Func<int, string> classify = x =>
        {
            if (x < 0) return "negative";
            if (x == 0) return "zero";
            return "positive";
        };
        Console.WriteLine($"   expression lambda : square(7) = {square(7)}");
        Console.WriteLine($"   statement lambda  : classify(-3) = {classify(-3)}, classify(0) = {classify(0)}, classify(5) = {classify(5)}");

        // Parameter syntax variants
        Func<int> noParam = () => 42;
        Func<int, int, int> two = (a, b) => a + b;
        Func<int, int, int> typed = (int a, int b) => a * b;
        Func<int, int, int> discards = (_, b) => b;                    // discard: an unused parameter (C# 9)
        Console.WriteLine($"   () => 42 → {noParam()} · (a,b) => a+b → {two(2, 3)} · (int a, int b) → {typed(2, 3)} · (_, b) => b → {discards(99, 7)}");

        // Natural type (C# 10): a lambda with var — the compiler infers Func/Action
        var inferred = (int x) => x + 1;
        Console.WriteLine($"   var inferred = (int x) => x + 1;  type = {inferred.GetType().Name}  → inferred(1) = {inferred(1)}");

        // Default parameter (C# 12)
        var withDefault = (int x, int y = 10) => x + y;
        Console.WriteLine($"   (int x, int y = 10) => x + y → withDefault(1) = {withDefault(1)}");

        // static lambda: CANNOT capture an outer variable (forbids capturing at compile time → no accidental closure)
        var factor = 3;
        Func<int, int> capturing = x => x * factor;                    // captures the outer variable (closure → Section 3)
        Func<int, int> noCapture = static x => x * 3;                  // cannot capture; trying to would be a compile error (CS8820)
        Console.WriteLine($"   capturing(2) = {capturing(2)} · static lambda noCapture(2) = {noCapture(2)}");

        // Lambda → Expression<Func<>> : DATA instead of code (an expression tree). LINQ to Entities/EF Core uses this to translate to SQL.
        System.Linq.Expressions.Expression<Func<int, bool>> expr = x => x > 5;
        Console.WriteLine($"\n   Expression<Func<int,bool>> expr = x => x > 5;  →  tree: {expr.Body}  (NodeType = {expr.Body.NodeType}); expr.Compile()(7) = {expr.Compile()(7)}");
        Console.WriteLine("   Func<> is compiled CODE, Expression<Func<>> is the TREE representation of the code → IQueryable/EF can translate the query to SQL (→ J3.3, J9.4, M1.3).");

        Console.WriteLine("""

        Expression lambda: a single expression, concise; Statement lambda: multiple steps, side effects, control flow.
        What does a lambda convert to?  The target delegate type (Func/Action/custom delegate) or Expression<TDelegate>. Every lambda must convert to a delegate.
        Note: outer variables captured inside a lambda = CLOSURE (next section).
        """);
    }
}
