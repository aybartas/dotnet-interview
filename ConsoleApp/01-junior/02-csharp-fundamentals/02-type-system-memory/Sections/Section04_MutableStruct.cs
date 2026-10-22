namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section04_MutableStruct
{
    public static void Run()
    {
        Console.WriteLine("Mutable struct = 'kopyaya yazıp orijinalin değiştiğini sanma' hatası.\n");

        // TUZAK 1: List<T> indexer'ı değerin KOPYASINI döner
        var list = new List<Counter> { new() };
        // list[0].Value++;                      // ✗ CS1612: Dönüş değeri bir değişken olmadığı için değiştirilemez (derleyici seni korur)
        var copy = list[0];                      // kopya
        copy.Value++;
        Console.WriteLine($"1) list[0] kopyası artırıldı → list[0].Value = {list[0].Value} (değişmedi!), copy.Value = {copy.Value}");
        list[0] = copy;                          // doğru yol: kopyala-değiştir-geri ata
        Console.WriteLine($"   geri atanınca        → list[0].Value = {list[0].Value}");

        // TUZAK 2: foreach değişkeni de kopyadır
        var items = new[] { new Counter(), new Counter() };
        foreach (var item in items) { var tmp = item; tmp.Value = 10; }
        Console.WriteLine($"2) foreach içindeki kopya değiştirildi → dizi: {items[0].Value}, {items[1].Value} (dizi elemanı DEĞİŞMEDİ)");
        for (var i = 0; i < items.Length; i++) items[i].Value = 10;   // dizi indexer'ı REFERANS verir → bu çalışır
        Console.WriteLine($"   for + dizi indexer ile → dizi: {items[0].Value}, {items[1].Value} (diziler özel: eleman yerinde değiştirilebilir)");

        // TUZAK 3: readonly alandaki struct — metot çağrısı SAVUNMACI KOPYA üzerinde çalışır (sessizce işe yaramaz)
        var holder = new Holder();
        holder.IncrementReadonlyField();
        Console.WriteLine($"3) readonly alandaki struct'ta Increment() → Value = {holder.ReadonlyCounterValue} (hâlâ 0: defensive copy)");

        // TUZAK 4: struct metodu kendini değiştirir ama kopyada
        var c = new Counter();
        IncrementCopy(c);
        Console.WriteLine($"4) metoda struct geçirildi → c.Value = {c.Value} (değişmedi)");

        Console.WriteLine("""

        ÇÖZÜM: struct'ları IMMUTABLE yapın → 'readonly struct' / 'readonly record struct'.
        Değişiklik gerekiyorsa yeni bir kopya üretin (with ifadesi / yeni örnek). Derleyici de savunmacı kopya yapmaz → daha hızlı.
        """);

        var money = new ImmutableMoney(10m, "TRY");
        var more = money with { Amount = 15m };
        Console.WriteLine($"immutable: {money} → with → {more}");
    }

    private static void IncrementCopy(Counter c) => c.Value++;

    private struct Counter
    {
        public int Value;
        public void Increment() => Value++;      // 'mutating' metot
    }

    private sealed class Holder
    {
        private readonly Counter _counter = new();
        public void IncrementReadonlyField() => _counter.Increment();   // readonly alan → kopya üzerinde çalışır
        public int ReadonlyCounterValue => _counter.Value;
    }

    private readonly record struct ImmutableMoney(decimal Amount, string Currency);
}
