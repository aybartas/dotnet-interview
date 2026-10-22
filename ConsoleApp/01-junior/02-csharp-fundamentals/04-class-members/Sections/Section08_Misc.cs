using System.Reflection;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section08_Misc
{
    public static void Run()
    {
        Console.WriteLine("A) Optional parameters and named arguments");
        Console.WriteLine($"   {Send("a@x.com")}");
        Console.WriteLine($"   {Send("a@x.com", urgent: true)}                  ← named argument: skip the optional in the middle and pass the next one");
        Console.WriteLine($"   {Send(subject: "Hello", to: "b@x.com")}   ← named arguments free the order");

        var p = typeof(Section08_Misc).GetMethod(nameof(Send), BindingFlags.NonPublic | BindingFlags.Static)!.GetParameters()[1];
        Console.WriteLine($"   The default value lives in METADATA: subject.HasDefaultValue = {p.HasDefaultValue}, DefaultValue = \"{p.DefaultValue}\"");
        Console.WriteLine("   ⚠ The default value is compiled into the CALLER assembly's IL (same logic as const, → J2.2): if you change the default in a library and ship only");
        Console.WriteLine("     the library, an old consumer keeps using the OLD default. Safe alternative in a public API: overloads.");

        Console.WriteLine($"   Mixing overload + optional: Log(\"x\") → {Log("x")}  (the overload without optionals wins; confusing → pick one)");

        Console.WriteLine("\nB) Anonymous types (compiler-generated, nameless, read-only, value-equality class)");
        var anon = new { Name = "Alice", Age = 30 };
        var anon2 = new { Name = "Alice", Age = 30 };
        Console.WriteLine($"   {anon}  Equals → {anon.Equals(anon2)}  type: {anon.GetType().Name}; same shape → same type: {anon.GetType() == anon2.GetType()}");
        Console.WriteLine("   CANNOT cross a method boundary (can't be written as a return type; only object/dynamic) → fine for LINQ intermediate results; use record/tuple for lasting data.");

        Console.WriteLine("\nC) Nested classes");
        var outer = new Outer();
        var inner = outer.CreateInner();
        Console.WriteLine($"   Inner can access Outer's private member: {inner.ReadOuterSecret()}");
        foreach (var nested in typeof(Outer).GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public))
            Console.WriteLine($"   nested type {nested.Name,-7} visibility: {nested.Attributes & TypeAttributes.VisibilityMask}");
        Console.WriteLine("   When does it make sense? A helper type that is only the outer class's OWN internal detail (enumerator, builder, state) → no name pollution, access to private members.");
        Console.WriteLine("   When not? Types that will also be used/tested from outside → a separate type in its own file.");
    }

    private static string Send(string to, string subject = "(no subject)", bool urgent = false)
        => $"to={to}, subject={subject}, urgent={urgent}";

    private static string Log(string message) => "Log(string)";
    private static string Log(string message, int level = 0) => "Log(string, int=0)";

    private sealed class Outer
    {
        private readonly string _secret = "outer-private";
        public Inner CreateInner() => new(this);

        private sealed class Helper;                                      // a fully private helper type (never visible from outside)
        public sealed class Inner(Outer owner)
        {
            public string ReadOuterSecret() => owner._secret;              // a nested type accesses the outer class's private member
        }
    }
}
