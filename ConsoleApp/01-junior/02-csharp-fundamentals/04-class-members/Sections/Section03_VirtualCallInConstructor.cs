using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section03_VirtualCallInConstructor
{
    public static void Run()
    {
        var derived = new DerivedWithState();

        Console.WriteLine("INSIDE the base constructor (virtual call) the derived Describe() saw this:");
        Console.WriteLine($"   {derived.Seen[0]}");
        Console.WriteLine("The same method AFTER the constructor finished:");
        Console.WriteLine($"   {derived.Describe()}");

        Console.WriteLine("""

        Why the difference?
          • _fromInitializer  → field initializer, runs BEFORE the base ctor → FILLED
          • _fromConstructor  → assigned in the derived ctor BODY; hadn't run yet while the base ctor was running → null
          • _count            → 0 for the same reason
        The overridden method sees a half-constructed object → silent, hard-to-find bugs. Analyzer: CA2214 ('warning' for this project via .editorconfig).
        To see the CA2214 warning in the build output:  dotnet build --no-incremental
        """);

        var safe = SafeBase.Create<SafeDerived>();
        Console.WriteLine($"Safe alternative (call after the object is fully constructed): {safe.Result}");
    }
}
