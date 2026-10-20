namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section10_RefOutIn
{
    public static void Run()
    {
        // ref: okunup YAZILABİLİR, çağıranda ÖNCEDEN atanmış olmalı
        int a = 1, b = 2;
        Swap(ref a, ref b);
        Console.WriteLine($"ref : Swap(ref a, ref b) → a={a}, b={b}");

        // out: metot İÇİNDE atanması ZORUNLU, çağıranda önceden atanması gerekmez (birden çok değer döndürmek / TryXxx deseni)
        if (TryDivide(10, 4, out var quotient, out var remainder))
            Console.WriteLine($"out : TryDivide(10, 4) → bölüm={quotient}, kalan={remainder}");
        _ = int.TryParse("x", out _);                              // discard: out değerini umursama

        // in: referansla geçir (KOPYALAMA yok) ama metot DEĞİŞTİREMEZ → büyük struct'lar için
        var big = new BigStruct { A = 1, B = 2, C = 3, D = 4 };
        Console.WriteLine($"in  : Sum(in big) = {Sum(in big)}   (32 byte'lık struct kopyalanmadı; salt okunur referans)");

        // Referans tipi DEĞERLE geçirilince: referansın KOPYASI geçer
        var names = new List<string> { "a" };
        Reassign(names);
        Console.WriteLine($"ref olmadan List yeniden atandı → çağıranda hâlâ [{string.Join(",", names)}]");
        ReassignWithRef(ref names);
        Console.WriteLine($"ref ile       List yeniden atandı → çağıranda [{string.Join(",", names)}]");

        // ref local & ref return: veri YERİNDE (kopyasız) erişim — dizi elemanına referans
        var array = new[] { 10, 20, 30 };
        ref var second = ref array[1];
        second = 99;
        Console.WriteLine($"ref local   : second = 99 → dizi [{string.Join(",", array)}]");
        ref var found = ref Find(array, 30);
        found++;
        Console.WriteLine($"ref return  : Find(array, 30)++ → dizi [{string.Join(",", array)}]");

        Console.WriteLine("""

        ref  → oku + yaz, önceden atanmış         out → metot yazmak ZORUNDA, çağıranda atama şart değil       in → salt okunur referans (kopyasız)
        Not: async metotlarda ve iterator'larda ref/out/in parametreler kullanılamaz. Modern kodda `out` yerine tuple/record döndürmek çoğu zaman daha okunaklıdır.
        """);
    }

    private static void Swap(ref int x, ref int y) => (x, y) = (y, x);

    private static bool TryDivide(int dividend, int divisor, out int quotient, out int remainder)
    {
        if (divisor == 0) { quotient = remainder = 0; return false; }   // out'lar HER yolda atanmalı
        quotient = dividend / divisor;
        remainder = dividend % divisor;
        return true;
    }

    private static long Sum(in BigStruct s) => s.A + s.B + s.C + s.D;   // s.A = 5; → ✗ CS8332: in parametresi değiştirilemez

    private static void Reassign(List<string> list) => list = ["yeni"];
    private static void ReassignWithRef(ref List<string> list) => list = ["yeni"];

    private static ref int Find(int[] items, int value)
    {
        for (var i = 0; i < items.Length; i++)
            if (items[i] == value) return ref items[i];
        throw new KeyNotFoundException();
    }

    private struct BigStruct { public long A, B, C, D; }
}
