using System.Collections.Immutable;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section06_CollectionExpressions
{
    public static void Run()
    {
        // ESKİ yazımlar (hepsi farklı sözdizimi)
        int[] oldArray = new int[] { 1, 2, 3 };
        var oldList = new List<int> { 1, 2, 3 };
        var oldImmutable = ImmutableArray.Create(1, 2, 3);
        Console.WriteLine($"eski: array={oldArray.Length}, list={oldList.Count}, immutable={oldImmutable.Length}   (üç tip, üç farklı sözdizimi)");

        // YENİ: tek sözdizimi — hedef tip belirler
        int[] array = [1, 2, 3];
        List<int> list = [1, 2, 3];
        ImmutableArray<int> immutable = [1, 2, 3];
        HashSet<int> set = [1, 2, 2, 3];
        Span<int> span = [1, 2, 3];
        IEnumerable<int> sequence = [1, 2, 3];
        IReadOnlyList<int> readOnly = [1, 2, 3];
        Console.WriteLine($"yeni: array={array.Length}, list={list.Count}, immutable={immutable.Length}, set={set.Count} (tekrar atıldı), span={span.Length}, IEnumerable={sequence.Count()}, IReadOnlyList={readOnly.Count}");

        // Boş koleksiyon
        List<string> empty = [];
        Console.WriteLine($"boş: [] → {empty.Count} eleman (List<string>); Array için `[]` → Array.Empty'ye benzer paylaşılan boş dizi");

        // Spread (..): bir koleksiyonun elemanlarını İÇERİ YAY — birleştirme için Concat/AddRange yerine
        int[] first = [1, 2], second = [4, 5];
        int[] merged = [.. first, 3, .. second];
        Console.WriteLine($"spread: [.. [1,2], 3, .. [4,5]] → [{string.Join(",", merged)}]");

        // Gerçekçi kullanım: koşullu eleman ekleme
        var isAdmin = true;
        string[] baseRoles = ["user"];
        string[] roles = [.. baseRoles, .. isAdmin ? new[] { "admin" } : []];
        Console.WriteLine($"koşullu: roles = [{string.Join(",", roles)}]");

        // Parametre olarak
        Console.WriteLine($"metot argümanı: Sum([1,2,3]) = {Sum([1, 2, 3])}   (hedef tip IEnumerable<int>)");

        Console.WriteLine("""

        Hedef tipli (target-typed): `[...]` kendi başına tip DEĞİLDİR; atandığı/geçirildiği tipe göre oluşur (array, List<T>, Span<T>, ImmutableArray<T>, IEnumerable<T>…).
        `var x = [1,2,3];` DERLENMEZ (CS9176: hedef tip yok) — açık tip yazın.
        Derleyici hedef tipe göre en verimli kodu üretir (Span için stack, array için doğrudan boyutlu tahsis; List<T> için kapasiteli) — AddRange döngüsünden iyi.
        Dictionary gibi anahtar-değer koleksiyonları için collection expression kullanılamaz; index initializer kullanın: new() { ["a"] = 1 }.
        """);
    }

    private static int Sum(IEnumerable<int> items) => items.Sum();
}
