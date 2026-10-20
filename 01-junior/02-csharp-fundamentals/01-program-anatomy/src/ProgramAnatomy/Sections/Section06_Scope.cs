namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section06_Scope
{
    private static readonly int _count = 100;            // alan (field)

    public static void Run()
    {
        // KAPSAM (scope): bir değişken, tanımlandığı en yakın { } bloğunda yaşar.
        {
            var inner = "yalnızca bu blokta";
            Console.WriteLine($"blok içi: {inner}");
        }
        // Console.WriteLine(inner);                     // ✗ CS0103: inner mevcut bağlamda yok

        // for değişkeni döngüye aittir
        for (var i = 0; i < 2; i++) { }
        // Console.WriteLine(i);                         // ✗ CS0103

        // GÖLGELEME (shadowing): iç kapsamdaki aynı adlı değişken dıştakini gizler.
        // C# yerel değişkenin dıştaki YEREL değişkeni gölgelemesine izin vermez (CS0136); ama ALANI gölgelemek serbesttir:
        var _count = 5;                                   // alanı gölgeler → artık '_count' yerel olandır
        Console.WriteLine($"yerel _count = {_count}, alan = {Section06_Scope._count}   (alana tip adıyla/this ile ulaşılır)");

        // Lambda dıştaki yerel değişkeni YAKALAR (closure); yakalanan değişken lambda çalıştığı anda okunur.
        var factor = 3;
        Func<int, int> mul = n => n * factor;             // dıştaki 'factor' YAKALANIR (closure) — gölgeleme değil
        Console.WriteLine($"closure: mul(4) = {mul(4)}");

        // DEFINITE ASSIGNMENT: bir yerel değişken OKUNMADAN ÖNCE her kod yolunda atanmış olmalı (derleyici akışı analiz eder).
        int result;
        if (Environment.TickCount64 >= 0)
            result = 1;
        else
            result = 2;
        Console.WriteLine($"definite assignment: result = {result}");     // iki dal da atadığı için geçerli
        // int broken; if (cond) broken = 1; Console.WriteLine(broken);  // ✗ CS0165: kullanılmayan yerel değişken atanmadı

        // out parametresi de çağrı dönüşünde atanmış sayılır
        if (int.TryParse("42", out var parsed))
            Console.WriteLine($"out değişkeni kapsamı: parsed = {parsed}");
        Console.WriteLine($"parsed bu satırda da görünür (out var, çevreleyen blokta yaşar): {parsed}");

        // pattern değişkeni kapsamı
        object o = "metin";
        if (o is string text && text.Length > 2)
            Console.WriteLine($"is-pattern değişkeni yalnızca koşul doğruysa kesin atanmıştır: {text}");
    }
}
