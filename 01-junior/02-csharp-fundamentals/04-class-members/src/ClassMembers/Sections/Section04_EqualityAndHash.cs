using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section04_EqualityAndHash
{
    public static void Run()
    {
        Console.WriteLine("A) Varsayılan: class → REFERANS eşitliği");
        var a1 = new Plain(); var a2 = new Plain();
        Console.WriteLine($"   new Plain() == new Plain()      → {a1 == a2}   Equals → {a1.Equals(a2)}   ReferenceEquals → {ReferenceEquals(a1, a2)}");

        Console.WriteLine("\nB) YANLIŞ: yalnızca Equals override edildi (GetHashCode edilmedi)");
        var b1 = new BadPoint(1, 2); var b2 = new BadPoint(1, 2);
        Console.WriteLine($"   b1.Equals(b2) = {b1.Equals(b2)}   ama hash'ler: {b1.GetHashCode()} vs {b2.GetHashCode()} (farklı!)");
        var badSet = new HashSet<BadPoint> { b1 };
        Console.WriteLine($"   HashSet<BadPoint> içinde eşit nesne var mı? Contains(b2) = {badSet.Contains(b2)}   ← false! (hash başka kovaya baktı)");
        var badDict = new Dictionary<BadPoint, string> { [b1] = "değer" };
        Console.WriteLine($"   Dictionary lookup: {(badDict.ContainsKey(b2) ? "bulundu" : "BULUNAMADI")}; aynı mantıksal anahtar iki kez eklenebilir: {badDict.TryAdd(b2, "ikinci")}");

        Console.WriteLine("\nC) DOĞRU: Equals + GetHashCode + IEquatable<T> + ==/!=");
        var g1 = new GoodPoint(1, 2); var g2 = new GoodPoint(1, 2);
        var goodSet = new HashSet<GoodPoint> { g1 };
        Console.WriteLine($"   g1 == g2 → {g1 == g2};  hash'ler eşit → {g1.GetHashCode() == g2.GetHashCode()};  Contains(g2) → {goodSet.Contains(g2)};  Add(g2) → {goodSet.Add(g2)} (zaten var)");

        Console.WriteLine("\nD) MUTABLE KEY tuzağı: koleksiyonda duran nesnenin hash'i DEĞİŞİRSE");
        var key = new MutableKey("a");
        var set = new HashSet<MutableKey> { key };
        key.Id = "b";                                                   // hash değişti, ama nesne ESKİ kovada duruyor
        Console.WriteLine($"   key.Id = \"b\" yapıldı → Contains(key) = {set.Contains(key)} (false!), Count = {set.Count}, enumerasyonla bulunur: {set.Any(k => ReferenceEquals(k, key))}");
        Console.WriteLine("   Nesne sette DURUYOR ama hash ile ARANAMIYOR → silinemez, tekrar eklenebilir → 'hayalet' kayıt, bellek sızıntısı.");
        Console.WriteLine("   Kural: hash'e/eşitliğe giren alanlar DEĞİŞMEZ olmalı (immutable key: record, readonly struct, string, int).");
        var safe = new HashSet<SafeKey> { new("a") };
        Console.WriteLine($"   SafeKey (record, immutable): Contains(new SafeKey(\"a\")) = {safe.Contains(new SafeKey("a"))}");

        Console.WriteLine("\nE) IComparable<T> (doğal sıra) ve IComparer<T> (alternatif sıra)");
        var versions = new List<Version2> { new(1, 10), new(1, 2), new(2, 0) };
        versions.Sort();
        Console.WriteLine($"   Sort() (IComparable): {string.Join(", ", versions)}");
        versions.Sort(new DescendingVersionComparer());
        Console.WriteLine($"   Sort(IComparer, azalan): {string.Join(", ", versions)}");

        Console.WriteLine("""

        KURALLAR
          • Equals + GetHashCode + (==, !=) + IEquatable<T> BİRLİKTE: eşitliği tanımlayan ALANLAR hash'e de girer.
          • a.Equals(b) ⇒ aynı hash (tersi şart değil: çakışma serbest, sadece performansı düşürür).
          • IEquatable<T>: tipli Equals(T) → struct'larda boxing'i önler, koleksiyonlar onu tercih eder.
          • GetHashCode kararlı olmalı: nesne ömrü boyunca (veya en azından koleksiyondayken) DEĞİŞMEMELİ → mutable alanlardan türetme.
          • record / record struct bunların hepsini otomatik üretir (→ J2.6) — değer nesneleri için en kolay yol.
          • ToString(): hata ayıklama ve loglama için anlamlı bir metin döndürün (varsayılan tip adıdır).
        """);
    }

    private sealed class Plain;
}
