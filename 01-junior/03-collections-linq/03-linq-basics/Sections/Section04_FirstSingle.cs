namespace Junior.CollectionsLinq.LinqBasics;

public static class Section04_FirstSingle
{
    public static void Run()
    {
        int[] empty = [];
        int[] one = [7];
        int[] many = [7, 8, 9];

        Console.WriteLine("                       boş dizi                   tek eleman    birden çok eleman");
        Console.WriteLine($"First()               : {Try(() => empty.First()),-26} {Try(() => one.First()),-13} {Try(() => many.First())}");
        Console.WriteLine($"FirstOrDefault()      : {Try(() => empty.FirstOrDefault()),-26} {Try(() => one.FirstOrDefault()),-13} {Try(() => many.FirstOrDefault())}");
        Console.WriteLine($"Single()              : {Try(() => empty.Single()),-26} {Try(() => one.Single()),-13} {Try(() => many.Single())}");
        Console.WriteLine($"SingleOrDefault()     : {Try(() => empty.SingleOrDefault()),-26} {Try(() => one.SingleOrDefault()),-13} {Try(() => many.SingleOrDefault())}");

        Console.WriteLine("\nTuzaklar:");
        string[] words = [];
        Console.WriteLine($"   FirstOrDefault() referans tipte null döner: words.FirstOrDefault() is null → {words.FirstOrDefault() is null}  (null kontrolü şart)");
        Console.WriteLine($"   değer tipinde default döner (int → 0): [].FirstOrDefault() = {empty.FirstOrDefault()}  ← 'bulunamadı' mı 'değer 0' mı ayırt edilemez!");
        Console.WriteLine($"   güvenli yol: FirstOrDefault(varsayılan) → {empty.FirstOrDefault(-1)}  ·  ya da nullable: empty.Select(x => (int?)x).FirstOrDefault() = {(empty.Select(x => (int?)x).FirstOrDefault() is null ? "null" : "?")}");

        Console.WriteLine("""

        ⭐ Anlamı:
          First            "en az bir tane var, İLKİNİ ver"          — yoksa InvalidOperationException ("Sequence contains no elements")
          FirstOrDefault   "varsa ilkini ver, yoksa varsayılan"      — hata yok; yoksa default(T)
          Single           "TAM OLARAK BİR tane olmalı, onu ver"     — 0 veya 2+ ise InvalidOperationException (sözleşmeyi DOĞRULAR)
          SingleOrDefault  "0 ya da 1 tane olmalı"                    — 0 → default; 2+ → InvalidOperationException

        Hangisini seçmeli?
          • Benzersizlik BEKLENİYORSA (Id, e-posta, tekil yapılandırma kaydı) → Single/SingleOrDefault: beklentiniz bozulursa (veri hatası) sessiz yanlış sonuç yerine ANINDA hata alırsınız.
          • "Hangisi olursa olsun biri yeter" / sıralı ilk eleman → First/FirstOrDefault (genelde OrderBy ile birlikte; sıralamadan First "rastgele ilk" demektir).
          • "Olmayabilir" → ...OrDefault + null/varsayılan kontrolü.
          • Performans: First ilk eşleşmede DURUR; Single benzersizliği doğrulamak için 2. eşleşmeyi aramak zorunda kalır (tek eşleşme varsa tüm diziyi tarar). EF Core'da: FirstOrDefault → `TOP 1`, SingleOrDefault → `TOP 2` (→ J9).
          • Son eleman için Last/LastOrDefault; indeksle ElementAt/ElementAtOrDefault.
        """);
    }

    private static string Try<T>(Func<T> f)
    {
        try { return f()?.ToString() ?? "null"; }
        catch (InvalidOperationException ex) { return ex.Message.Contains("more than one", StringComparison.OrdinalIgnoreCase) ? "InvalidOp(birden çok)" : "InvalidOp(boş)"; }
    }
}
