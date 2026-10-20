using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section06_Strings
{
    public static void Run()
    {
        Console.WriteLine("1) string IMMUTABLE'dır: her 'değişiklik' YENİ bir string üretir.");
        var s = "merhaba";
        var upper = s.ToUpperInvariant();
        Console.WriteLine($"   s = \"{s}\", s.ToUpperInvariant() = \"{upper}\" → s değişmedi; ReferenceEquals(s, upper) = {ReferenceEquals(s, upper)}");
        Console.WriteLine("   Neden immutable? Thread-safe paylaşım, güvenli Dictionary anahtarı (hash değişmez), güvenlik (yol/bağlantı dizesi sonradan değişmez), interning mümkün.");

        Console.WriteLine("\n2) Döngüde birleştirme: string += O(n²), StringBuilder O(n)");
        const int n = 20_000;
        var sw = Stopwatch.StartNew();
        var bytesBefore = GC.GetAllocatedBytesForCurrentThread();
        var text = "";
        for (var i = 0; i < n; i++) text += "x";
        var concatBytes = GC.GetAllocatedBytesForCurrentThread() - bytesBefore;
        Console.WriteLine($"   += ile {n:N0} kez        : {sw.ElapsedMilliseconds,4} ms, {concatBytes / 1024.0 / 1024.0,8:F1} MB tahsis");

        sw.Restart();
        bytesBefore = GC.GetAllocatedBytesForCurrentThread();
        var sb = new StringBuilder();
        for (var i = 0; i < n; i++) sb.Append('x');
        var built = sb.ToString();
        Console.WriteLine($"   StringBuilder ile        : {sw.ElapsedMilliseconds,4} ms, {(GC.GetAllocatedBytesForCurrentThread() - bytesBefore) / 1024.0 / 1024.0,8:F1} MB tahsis   (sonuç aynı: {built == text})");
        Console.WriteLine("   (Tek seferlik birleştirme 'a + b + c' için StringBuilder GEREKMEZ: derleyici string.Concat'e çevirir.)");

        Console.WriteLine("\n3) == string'de DEĞER karşılaştırır (operator overload), referans değil:");
        var a = new string(['a', 'b', 'c']);
        var b = new string(['a', 'b', 'c']);
        Console.WriteLine($"   a == b: {a == b}   ReferenceEquals(a, b): {ReferenceEquals(a, b)}   (farklı nesneler, aynı içerik)");
        object oa = a, ob = b;
        Console.WriteLine($"   (object)a == (object)b: {oa == ob}   ← object tipinde == REFERANS karşılaştırır (operator overload derleme zamanı tipine bağlıdır)");

        Console.WriteLine("\n4) String interning: aynı literal'ler tek nesneyi paylaşır");
        var l1 = "interned"; var l2 = "interned";
        Console.WriteLine($"   iki aynı literal: ReferenceEquals = {ReferenceEquals(l1, l2)}");
        var runtime = new string("interned".ToCharArray());
        Console.WriteLine($"   runtime'da üretilen: ReferenceEquals(l1, runtime) = {ReferenceEquals(l1, runtime)};  string.Intern(runtime) ile: {ReferenceEquals(l1, string.Intern(runtime))}");

        Console.WriteLine("\n5) TÜRKÇE I PROBLEMİ ⭐ — kültüre duyarlı büyük/küçük harf dönüşümü");
        var tr = new CultureInfo("tr-TR");
        var inv = CultureInfo.InvariantCulture;
        Console.WriteLine($"   \"title\".ToUpper(tr-TR)   = {"title".ToUpper(tr)}   ← i → İ (noktalı büyük I)");
        Console.WriteLine($"   \"title\".ToUpper(invariant) = {"title".ToUpper(inv)}");
        Console.WriteLine($"   \"FILE\".ToLower(tr-TR)    = {"FILE".ToLower(tr)}    ← I → ı (noktasız küçük ı) → \"file\" ile EŞİT DEĞİL!");

        var old = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = tr;
        try
        {
            var command = "FILE";
            Console.WriteLine($"\n   Kullanıcı kültürü tr-TR iken:");
            Console.WriteLine($"   command.ToLower() == \"file\"                          → {command.ToLower() == "file"}   ✗ HATALI (üretimde yalnızca Türkçe cihazlarda patlar!)");
            Console.WriteLine($"   command.Equals(\"file\", StringComparison.OrdinalIgnoreCase) → {command.Equals("file", StringComparison.OrdinalIgnoreCase)}   ✓ DOĞRU");
            Console.WriteLine($"   string.Compare(\"i\", \"I\", ignoreCase: true) (kültür duyarlı) = {string.Compare("i", "I", ignoreCase: true)}   (tr-TR'de i ≠ I)");
            Console.WriteLine($"   string.Equals(\"i\", \"I\", OrdinalIgnoreCase)            = {string.Equals("i", "I", StringComparison.OrdinalIgnoreCase)}");
        }
        finally { CultureInfo.CurrentCulture = old; }

        Console.WriteLine("""

           KURAL:
           • Dil-bağımsız tanımlayıcılar (komut adı, dosya yolu, URL, HTTP header, config anahtarı, enum adı) → StringComparison.Ordinal / OrdinalIgnoreCase,
             büyük-küçük dönüşümü ToUpperInvariant()/ToLowerInvariant().
           • Kullanıcıya gösterilen/sıralanan metin → kültür duyarlı (CurrentCulture).
           • Analyzer: CA1304/CA1305/CA1307/CA1309/CA1310 kültür/karşılaştırma tuzaklarını yakalar.
        """);
    }
}
