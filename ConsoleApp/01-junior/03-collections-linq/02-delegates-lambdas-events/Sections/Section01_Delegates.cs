namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section01_Delegates
{
    // 1) Delegate TYPE: "a type that can hold methods with this signature" (a type-safe function pointer)
    private delegate int BinaryOperation(int a, int b);

    public static void Run()
    {
        Console.WriteLine("1) delegate = a signature type; it holds a REFERENCE to a method (or several). A delegate is an OBJECT (System.MulticastDelegate).");
        BinaryOperation op = Add;                                      // method group → delegate conversion (shorthand for new BinaryOperation(Add))
        Console.WriteLine($"   BinaryOperation op = Add;  op(2, 3) = {op(2, 3)}  ·  op.Method.Name = {op.Method.Name}  ·  type: {op.GetType().BaseType!.Name}");

        Console.WriteLine("\n2) Built-in generic delegates (you rarely write your own delegate type):");
        Func<int, int, int> add = Add;                                 // input(s) + RETURN value: Func<T1, T2, TResult>
        Action<string> print = Console.WriteLine;                      // input, NO return (void): Action<T>
        Predicate<int> isEven = n => n % 2 == 0;                       // single input returning bool: Predicate<T> (= Func<T,bool>)
        Console.WriteLine($"   Func: add(4,5) = {add(4, 5)}  ·  Predicate: isEven(4) = {isEven(4)}");
        print("   printed via Action<string> (no return value)");
        Console.WriteLine("   The last type parameter of Func<> is the RETURN type; Action<> returns void; Predicate<T> ≡ Func<T,bool> (in older APIs: List.FindAll)");

        Console.WriteLine("\n3) Method group: passing a method name (without parentheses) where a delegate is expected:");
        var words = new List<string> { "cat", "dog", "bird" };
        Console.WriteLine($"   words.ForEach(Console.WriteLine) →");
        words.ForEach(Console.WriteLine);
        Console.WriteLine("   A method group is bound at compile time to the overload whose signature matches; if ambiguous (multiple overloads), write it out explicitly with a lambda.");

        Console.WriteLine("\n4) MULTICAST: a delegate can chain several methods (+=, -=); when invoked they run IN ORDER");
        Action<string> chain = s => Console.WriteLine($"      subscriber 1: {s}");
        chain += s => Console.WriteLine($"      subscriber 2: {s}");
        chain += s => Console.WriteLine($"      subscriber 3: {s}");
        Console.WriteLine($"   InvocationList length = {chain.GetInvocationList().Length}");
        chain("hello");

        Func<int> values = () => 1;
        values += () => 2;
        values += () => 3;
        Console.WriteLine($"   Multicast with return value: invoking Func<int> (chain of 1,2,3) → {values()}   ← ONLY THE LAST method's return value is used (the others are discarded)");

        Console.WriteLine("\n5) Delegates are IMMUTABLE: += creates a new delegate object and assigns it to the variable.");
        Action a = () => { };
        var before = a;
        a += () => { };
        Console.WriteLine($"   same object after +=? {ReferenceEquals(before, a)}   (false → a new chain)");

        Console.WriteLine("\n6) Null safety: if there are no subscribers the delegate is NULL → invoking it throws NullReferenceException; ?.Invoke() is safe:");
        Action<string>? none = null;
        none?.Invoke("nobody here");                                   // silently skipped
        try { none!("x"); }
        catch (NullReferenceException) { Console.WriteLine("   invoking a null delegate directly → NullReferenceException; use `?.Invoke(...)`"); }

        Console.WriteLine("""

        What is a delegate and what is it for?  Carrying behavior like a VALUE: passing it as a parameter (strategy), callbacks, events, LINQ (Where(predicate)).
        Delegate vs interface: a delegate is lightweight for single-method behavior; use an interface for multiple related methods/state.
        """);
    }

    private static int Add(int a, int b) => a + b;
}
