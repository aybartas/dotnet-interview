using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section03_VirtualCallInConstructor
{
    public static void Run()
    {
        var derived = new DerivedWithState();

        Console.WriteLine("Taban constructor'ın İÇİNDE (virtual çağrı) türetilmiş Describe() şunu gördü:");
        Console.WriteLine($"   {derived.Seen[0]}");
        Console.WriteLine("Constructor bittikten SONRA aynı metot:");
        Console.WriteLine($"   {derived.Describe()}");

        Console.WriteLine("""

        Fark neden?
          • _fromInitializer  → field initializer, taban ctor'dan ÖNCE çalışır → DOLU
          • _fromConstructor  → türetilmiş ctor GÖVDESİNDE atanır; taban ctor çalışırken henüz çalışmadı → null
          • _count            → aynı sebeple 0
        Override edilen metot nesnenin yarım kurulmuş halini görür → sessiz, bulması zor hatalar. Analyzer: CA2214 (bu proje için .editorconfig ile 'warning').
        Derleme çıktısında CA2214 uyarısını görmek için:  dotnet build --no-incremental
        """);

        var safe = SafeBase.Create<SafeDerived>();
        Console.WriteLine($"Güvenli alternatif (nesne tamamen kurulduktan sonra çağır): {safe.Result}");
    }
}
