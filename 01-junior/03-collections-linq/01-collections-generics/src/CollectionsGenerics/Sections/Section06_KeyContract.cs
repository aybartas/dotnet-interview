using System.Diagnostics;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section06_KeyContract
{
    public static void Run()
    {
        Console.WriteLine("Her deneyde Dictionary'ye anahtar 1 → \"değer\" ekleniyor, sonra AYNI mantıksal anahtarla (yeni bir nesne) aranıyor:\n");

        Console.WriteLine($"   DOĞRU       (Equals + GetHashCode tutarlı)       : bulundu mu? {Lookup(() => new GoodKey(1), () => new GoodKey(1))}");
        Console.WriteLine($"   YANLIŞ #1   (sadece Equals override)             : bulundu mu? {Lookup(() => new EqualsOnlyKey(1), () => new EqualsOnlyKey(1))}   ← hash'ler farklı kovaya baktı");
        Console.WriteLine($"   YANLIŞ #2   (sadece GetHashCode override)        : bulundu mu? {Lookup(() => new HashOnlyKey(1), () => new HashOnlyKey(1))}   ← aynı kova ama Equals referans karşılaştırdı");

        Console.WriteLine("\nYANLIŞ #3 — Sabit hash (çalışır ama O(n)):");
        const int n = 5_000;
        var good = Enumerable.Range(0, n).ToDictionary(i => new GoodKey(i), i => i);
        var bad = Enumerable.Range(0, n).ToDictionary(i => new ConstantHashKey(i), i => i);
        var keysGood = Enumerable.Range(0, n).Select(i => new GoodKey(i)).ToArray();
        var keysBad = Enumerable.Range(0, n).Select(i => new ConstantHashKey(i)).ToArray();
        var sw = Stopwatch.StartNew(); foreach (var k in keysGood) _ = good[k]; var tGood = sw.Elapsed.TotalMilliseconds;
        sw.Restart(); foreach (var k in keysBad) _ = bad[k]; var tBad = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine($"   {n:N0} arama: tutarlı hash = {tGood:F2} ms · sabit hash = {tBad:F2} ms  (≈ {tBad / Math.Max(tGood, 0.001):N0}× yavaş — tüm anahtarlar tek kovada, zincir taranıyor)");

        Console.WriteLine("\nYANLIŞ #4 — Mutable key (koleksiyondayken hash değişirse):");
        var key = new MutableKey("a");
        var map = new Dictionary<MutableKey, int> { [key] = 1 };
        key.Name = "b";
        Console.WriteLine($"   key.Name değiştirildi → ContainsKey(key) = {map.ContainsKey(key)} (false!), Count = {map.Count}, Remove(key) = {map.Remove(key)} (silinemez → hayalet kayıt)");

        Console.WriteLine("\nÖzel karşılaştırma: tipi değiştirmeden IEqualityComparer<T>:");
        var ci = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Elma"] = 1 };
        Console.WriteLine($"   OrdinalIgnoreCase: ci[\"ELMA\"] = {ci["ELMA"]}   ·   özel IEqualityComparer: {new Dictionary<string, int>(new CaseInsensitiveComparer()) { ["Elma"] = 2 }["eLMa"]}");
        Console.WriteLine("   Dikkat: string anahtarlarda varsayılan karşılaştırma ORDINAL (büyük/küçük harf duyarlı); kültüre duyarlı karşılaştırıcıyı anahtar için seçmeyin (→ J2.2).");

        Console.WriteLine("""

        SÖZLEŞME ⭐
          1) a.Equals(b) ⇒ a.GetHashCode() == b.GetHashCode()           (ihlal → arama başarısız)
          2) Hash ve Equals AYNI (ve DEĞİŞMEZ) alanlara dayanmalı          (mutable key → kayıp kayıt)
          3) İyi dağılan hash                                              (sabit/zayıf hash → O(n))
        Pratik: anahtar olarak string, int, Guid, enum, record, readonly record struct kullanın. HashCode.Combine(a, b, …) iyi bir başlangıçtır.
        """);
    }

    private static bool Lookup<T>(Func<T> store, Func<T> find) where T : notnull
    {
        var dict = new Dictionary<T, string> { [store()] = "değer" };
        return dict.ContainsKey(find());
    }
}
