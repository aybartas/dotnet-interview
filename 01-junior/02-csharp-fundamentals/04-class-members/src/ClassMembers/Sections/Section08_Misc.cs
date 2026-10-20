using System.Reflection;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section08_Misc
{
    public static void Run()
    {
        Console.WriteLine("A) Optional parametreler ve named argument'lar");
        Console.WriteLine($"   {Send("a@x.com")}");
        Console.WriteLine($"   {Send("a@x.com", urgent: true)}                  ← named argument: ortadaki optional'ı atlayıp sonrakini ver");
        Console.WriteLine($"   {Send(subject: "Selam", to: "b@x.com")}   ← named argument sırayı serbest bırakır");

        var p = typeof(Section08_Misc).GetMethod(nameof(Send), BindingFlags.NonPublic | BindingFlags.Static)!.GetParameters()[1];
        Console.WriteLine($"   Varsayılan değer METADATA'da: subject.HasDefaultValue = {p.HasDefaultValue}, DefaultValue = \"{p.DefaultValue}\"");
        Console.WriteLine("   ⚠ Varsayılan değer ÇAĞIRAN assembly'nin IL'ine derlenir (const ile aynı mantık, → J2.2): kütüphanede varsayılanı değiştirip yalnızca");
        Console.WriteLine("     kütüphaneyi dağıtırsanız eski tüketici ESKİ varsayılanı kullanmaya devam eder. Public API'de güvenli alternatif: overload'lar.");

        Console.WriteLine($"   Overload + optional karışırsa: Log(\"x\") → {Log("x")}  (optional'sız overload önceliklidir; kafa karıştırıcı → birini seçin)");

        Console.WriteLine("\nB) Anonim tipler (derleyici ürettiği, adsız, salt okunur, değer eşitlikli sınıf)");
        var anon = new { Ad = "Ayşe", Yas = 30 };
        var anon2 = new { Ad = "Ayşe", Yas = 30 };
        Console.WriteLine($"   {anon}  Equals → {anon.Equals(anon2)}  tip: {anon.GetType().Name}; aynı yapı → aynı tip: {anon.GetType() == anon2.GetType()}");
        Console.WriteLine("   Metot sınırını GEÇEMEZ (dönüş tipi olarak yazılamaz; ancak object/dynamic) → LINQ ara sonuçları için uygun; kalıcı veri için record/tuple.");

        Console.WriteLine("\nC) Nested (iç içe) sınıflar");
        var outer = new Outer();
        var inner = outer.CreateInner();
        Console.WriteLine($"   Inner, Outer'ın private üyesine erişebilir: {inner.ReadOuterSecret()}");
        foreach (var nested in typeof(Outer).GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public))
            Console.WriteLine($"   iç tip {nested.Name,-7} görünürlüğü: {nested.Attributes & TypeAttributes.VisibilityMask}");
        Console.WriteLine("   Ne zaman anlamlı? Dış sınıfın yalnızca KENDİ iç detayı olan yardımcı tip (enumerator, builder, state) → ad kirliliği olmaz, private üyelere erişir.");
        Console.WriteLine("   Ne zaman değil? Dışarıdan da kullanılacak/test edilecek tipler → kendi dosyasında ayrı tip.");
    }

    private static string Send(string to, string subject = "(konu yok)", bool urgent = false)
        => $"to={to}, subject={subject}, urgent={urgent}";

    private static string Log(string message) => "Log(string)";
    private static string Log(string message, int level = 0) => "Log(string, int=0)";

    private sealed class Outer
    {
        private readonly string _secret = "outer-private";
        public Inner CreateInner() => new(this);

        private sealed class Helper;                                      // tamamen private yardımcı tip (dışarıdan hiç görünmez)
        public sealed class Inner(Outer owner)
        {
            public string ReadOuterSecret() => owner._secret;              // nested tip, dış sınıfın private üyesine erişir
        }
    }
}
