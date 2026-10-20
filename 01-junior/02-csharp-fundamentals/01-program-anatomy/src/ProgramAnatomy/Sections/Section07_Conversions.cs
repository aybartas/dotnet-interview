using System.Globalization;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section07_Conversions
{
    public static void Run()
    {
        // 1) IMPLICIT: veri kaybı yok → otomatik
        int i = 123;
        long l = i;                                       // int → long
        double d = i;                                     // int → double
        Console.WriteLine($"implicit: int {i} → long {l} → double {d}");

        // 2) EXPLICIT (cast): veri kaybı olabilir → yazmak ZORUNDASINIZ, sorumluluk sizde
        double pi = 3.99;
        int truncated = (int)pi;                          // yuvarlamaz, KESER
        Console.WriteLine($"explicit cast: (int)3.99 = {truncated}   (yuvarlama değil, kesme)");
        long big = 3_000_000_000;
        Console.WriteLine($"explicit cast: (int)3_000_000_000 = {unchecked((int)big)}   (taşma sessizce olur → Section08)");

        // 3) is / as: REFERANS dönüşümleri
        object o = "merhaba";
        Console.WriteLine($"is: o is string → {o is string}");
        var asString = o as string;                       // başarısızsa null döner (exception FIRLATMAZ)
        var asInt = o as int?;                            // value type için nullable ile
        Console.WriteLine($"as: o as string → \"{asString}\", o as int? → {(asInt is null ? "null" : asInt)}");
        try { _ = (int)o; }                               // cast: başarısızsa InvalidCastException
        catch (InvalidCastException ex) { Console.WriteLine($"(int)o → {ex.GetType().Name}"); }
        if (o is string s && s.Length > 3)                // pattern matching: kontrol + cast + değişken tek adımda
            Console.WriteLine($"pattern: o is string s → s.Length = {s.Length}");

        // 4) Convert: null/format toleranslı, YUVARLAR (banker's rounding: .5 en yakın ÇİFT sayıya)
        Console.WriteLine($"\nConvert.ToInt32(2.5) = {Convert.ToInt32(2.5)}, (3.5) = {Convert.ToInt32(3.5)}   (banker's rounding!)  ·  Math.Round(2.5, MidpointRounding.AwayFromZero) = {Math.Round(2.5, MidpointRounding.AwayFromZero)}");
        Console.WriteLine($"Convert.ToInt32(null as string) = {Convert.ToInt32(null as string)}   (null → 0, exception yok)");
        // int.Parse(null) → ArgumentNullException

        // 5) Parse vs TryParse ⭐
        try { _ = int.Parse("12a"); }
        catch (FormatException ex) { Console.WriteLine($"int.Parse(\"12a\") → {ex.GetType().Name}  (hata = exception: pahalı ve kontrol akışı için yanlış araç)"); }

        var ok = int.TryParse("12a", out var value);      // exception YOK; bool döner, value = 0
        Console.WriteLine($"int.TryParse(\"12a\") → {ok}, value = {value}   (kullanıcı girdisi için doğru araç)");

        // Kültür tuzağı: aynı metin farklı kültürde farklı sayı
        var tr = new CultureInfo("tr-TR");
        Console.WriteLine($"decimal.Parse(\"1.234,5\", tr-TR)  = {decimal.Parse("1.234,5", tr)}");
        Console.WriteLine($"decimal.Parse(\"1.234,5\", invariant) → {(decimal.TryParse("1.234,5", CultureInfo.InvariantCulture, out var inv) ? inv : "BAŞARISIZ")}");
        Console.WriteLine("Kural: makine-makine veri (JSON, dosya, protokol) → InvariantCulture; kullanıcıya gösterim/girdi → kullanıcının kültürü.");
    }
}
