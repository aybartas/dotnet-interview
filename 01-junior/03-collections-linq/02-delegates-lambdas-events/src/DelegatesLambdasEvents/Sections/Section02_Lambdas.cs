namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section02_Lambdas
{
    public static void Run()
    {
        Console.WriteLine("Lambda expression: adsız (anonim) fonksiyon; `=>` işaretinin solu parametreler, sağı gövde.\n");

        // EXPRESSION lambda: gövde TEK ifade, değer dönüşü örtük
        Func<int, int> square = x => x * x;
        // STATEMENT lambda: süslü parantez, birden çok ifade, açık return
        Func<int, string> classify = x =>
        {
            if (x < 0) return "negatif";
            if (x == 0) return "sıfır";
            return "pozitif";
        };
        Console.WriteLine($"   expression lambda : square(7) = {square(7)}");
        Console.WriteLine($"   statement lambda  : classify(-3) = {classify(-3)}, classify(0) = {classify(0)}, classify(5) = {classify(5)}");

        // Parametre sözdizimi çeşitleri
        Func<int> noParam = () => 42;
        Func<int, int, int> two = (a, b) => a + b;
        Func<int, int, int> typed = (int a, int b) => a * b;
        Func<int, int, int> discards = (_, b) => b;                    // discard: kullanılmayan parametre (C# 9)
        Console.WriteLine($"   () => 42 → {noParam()} · (a,b) => a+b → {two(2, 3)} · (int a, int b) → {typed(2, 3)} · (_, b) => b → {discards(99, 7)}");

        // Natural type (C# 10): var ile lambda — derleyici Func/Action çıkarır
        var inferred = (int x) => x + 1;
        Console.WriteLine($"   var inferred = (int x) => x + 1;  tip = {inferred.GetType().Name}  → inferred(1) = {inferred(1)}");

        // Varsayılan parametre (C# 12)
        var withDefault = (int x, int y = 10) => x + y;
        Console.WriteLine($"   (int x, int y = 10) => x + y → withDefault(1) = {withDefault(1)}");

        // static lambda: dış değişkeni YAKALAYAMAZ (yakalamayı derleme zamanında yasaklar → kazara closure yok)
        var factor = 3;
        Func<int, int> capturing = x => x * factor;                    // dış değişkeni yakalar (closure → Section 3)
        Func<int, int> noCapture = static x => x * 3;                  // yakalayamaz; yakalamaya çalışsa derleme hatası (CS8820)
        Console.WriteLine($"   capturing(2) = {capturing(2)} · static lambda noCapture(2) = {noCapture(2)}");

        // Lambda → Expression<Func<>> : kod yerine VERİ (ifade ağacı). LINQ to Entities/EF Core SQL'e çevirmek için bunu kullanır.
        System.Linq.Expressions.Expression<Func<int, bool>> expr = x => x > 5;
        Console.WriteLine($"\n   Expression<Func<int,bool>> expr = x => x > 5;  →  ağaç: {expr.Body}  (NodeType = {expr.Body.NodeType}); expr.Compile()(7) = {expr.Compile()(7)}");
        Console.WriteLine("   Func<> derlenmiş KOD, Expression<Func<>> kodun AĞAÇ temsilidir → IQueryable/EF sorguyu SQL'e çevirebilir (→ J3.3, J9.4, M1.3).");

        Console.WriteLine("""

        Expression lambda: tek ifade, kısa; Statement lambda: birden çok adım, yan etki, kontrol akışı.
        Lambda neye çevrilir?  Hedef delegate tipine (Func/Action/özel delegate) ya da Expression<TDelegate>'e. Her lambda'nın bir delegate'e dönüşmesi gerekir.
        Not: lambda içinde yakalanan dış değişkenler = CLOSURE (sıradaki bölüm).
        """);
    }
}
