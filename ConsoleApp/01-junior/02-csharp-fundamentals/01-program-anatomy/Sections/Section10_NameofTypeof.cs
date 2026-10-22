namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section10_NameofTypeof
{
    public static void Run()
    {
        // nameof: gives the symbol's NAME as a string at compile time → updated safely on rename (refactor)
        Console.WriteLine($"nameof(Customer.Email) = \"{nameof(Customer.Email)}\"   nameof(Console.WriteLine) = \"{nameof(Console.WriteLine)}\"");
        try { new Customer().SetAge(-1); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"ParamName = \"{ex.ParamName}\"   ← nameof(age) instead of a magic string"); }

        // typeof: COMPILE-TIME type info (a Type object)   ·   GetType(): the actual RUNTIME type
        object o = new List<int>();
        Console.WriteLine($"\ntypeof(object) = {typeof(object)}   o.GetType() = {o.GetType()}   (reference type is object, actual type is List<int>)");
        Console.WriteLine($"typeof(List<>) = {typeof(List<>)}   (open generic)   typeof(List<int>) = {typeof(List<int>)}");
        Console.WriteLine($"o is IEnumerable<int> = {o is IEnumerable<int>}   o.GetType() == typeof(IEnumerable<int>) = {o.GetType() == typeof(IEnumerable<int>)}   (is covers inheritance; GetType() == requires an exact match)");

        // sizeof: sizes of built-in value types (bytes); your own struct needs an unsafe context
        Console.WriteLine($"\nsizeof(byte)={sizeof(byte)} short={sizeof(short)} int={sizeof(int)} long={sizeof(long)} double={sizeof(double)} decimal={sizeof(decimal)} char={sizeof(char)} bool={sizeof(bool)}");

        // default: the type's default value (value type → 0/false, reference type → null)
        Console.WriteLine($"\ndefault(int)={default(int)} default(bool)={default(bool)} default(string)={default(string) ?? "null"} default(DateTime)={default(DateTime):yyyy-MM-dd}");
        int x = default;                                  // 'default literal' (C# 7.1): the type is inferred from the target
        Console.WriteLine($"int x = default → {x}");
        Console.WriteLine($"default(T) in a generic: {FirstOrDefaultValue<int>([])}, {FirstOrDefaultValue<string>([]) ?? "null"}");
    }

    private static T? FirstOrDefaultValue<T>(IReadOnlyList<T> items) => items.Count > 0 ? items[0] : default;

    private sealed class Customer
    {
        public string Email { get; set; } = "";
        public void SetAge(int age)
        {
            if (age < 0) throw new ArgumentOutOfRangeException(nameof(age));
        }
    }
}
