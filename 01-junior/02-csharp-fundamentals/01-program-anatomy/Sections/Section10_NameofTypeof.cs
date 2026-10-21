namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section10_NameofTypeof
{
    public static void Run()
    {
        // nameof: derleme zamanında sembolün ADINI string olarak verir → yeniden adlandırmada (refactor) güvenle güncellenir
        Console.WriteLine($"nameof(Customer.Email) = \"{nameof(Customer.Email)}\"   nameof(Console.WriteLine) = \"{nameof(Console.WriteLine)}\"");
        try { new Customer().SetAge(-1); }
        catch (ArgumentOutOfRangeException ex) { Console.WriteLine($"ParamName = \"{ex.ParamName}\"   ← magic string yerine nameof(age)"); }

        // typeof: DERLEME ZAMANI tip bilgisi (Type nesnesi)   ·   GetType(): ÇALIŞMA ZAMANI gerçek tipi
        object o = new List<int>();
        Console.WriteLine($"\ntypeof(object) = {typeof(object)}   o.GetType() = {o.GetType()}   (referans tipi object, gerçek tip List<int>)");
        Console.WriteLine($"typeof(List<>) = {typeof(List<>)}   (açık generic)   typeof(List<int>) = {typeof(List<int>)}");
        Console.WriteLine($"o is IEnumerable<int> = {o is IEnumerable<int>}   o.GetType() == typeof(IEnumerable<int>) = {o.GetType() == typeof(IEnumerable<int>)}   (is kalıtımı da kapsar; GetType() == tam eşleşme ister)");

        // sizeof: yerleşik value type boyutları (byte); kendi struct'ınız için unsafe bağlam gerekir
        Console.WriteLine($"\nsizeof(byte)={sizeof(byte)} short={sizeof(short)} int={sizeof(int)} long={sizeof(long)} double={sizeof(double)} decimal={sizeof(decimal)} char={sizeof(char)} bool={sizeof(bool)}");

        // default: tipin varsayılan değeri (value type → 0/false, reference type → null)
        Console.WriteLine($"\ndefault(int)={default(int)} default(bool)={default(bool)} default(string)={default(string) ?? "null"} default(DateTime)={default(DateTime):yyyy-MM-dd}");
        int x = default;                                  // 'default literal' (C# 7.1): tip hedeften çıkarılır
        Console.WriteLine($"int x = default → {x}");
        Console.WriteLine($"Generic'te default(T): {FirstOrDefaultValue<int>([])}, {FirstOrDefaultValue<string>([]) ?? "null"}");
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
