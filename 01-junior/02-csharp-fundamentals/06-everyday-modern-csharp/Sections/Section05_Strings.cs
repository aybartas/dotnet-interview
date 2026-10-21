using System.Globalization;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section05_Strings
{
    public static void Run()
    {
        var name = "Ayşe";
        var price = 1234.5m;
        var date = new DateTime(2026, 3, 9);
        var inv = CultureInfo.InvariantCulture;

        Console.WriteLine("1) Interpolation: ifade + biçim + hizalama");
        Console.WriteLine($"   ad={name,-8}| fiyat={price,10:N2}| tarih={date:yyyy-MM-dd}| hex={255:X4}| yüzde={0.256:P1}");
        Console.WriteLine($"   ifade içinde: {(price > 1000 ? "pahalı" : "ucuz")}  ·  {name.ToUpperInvariant()}  ·  Invariant: {FormattableString.Invariant($"{price:F2}")}");
        Console.WriteLine($"   eski: string.Format(\"{{0}} - {{1:N2}}\", ...) → {string.Format(inv, "{0} - {1:N2}", name, price)}");

        Console.WriteLine("\n2) Raw string literal (C# 11): \"\"\" ile — kaçış karakteri GEREKMEZ; JSON/SQL/regex/HTML için ideal");
        var json = """
            {
              "name": "Ayşe",
              "path": "C:\temp\new",
              "quote": "dedi ki \"merhaba\""
            }
            """;
        Console.WriteLine(json);
        Console.WriteLine("   (kapanış \"\"\"'in girintisi KAÇAN boşlukları belirler → içerik girintisi yazdığınız gibi kalır)");

        Console.WriteLine("\n3) Interpolated raw string: $$\"\"\" — {{ }} ile ifade, tek { } JSON'a ait kalır");
        var body = $$"""
            {
              "user": "{{name}}",
              "total": {{price.ToString("F2", inv)}}
            }
            """;
        Console.WriteLine(body);

        Console.WriteLine("\n4) Eski yazım ile karşılaştırma (aynı JSON):");
        var legacy = "{\n  \"user\": \"" + name + "\",\n  \"total\": " + price.ToString("F2", inv) + "\n}";
        Console.WriteLine($"   aynı mı? {legacy == body.ReplaceLineEndings("\n")}   (raw string: kaçışsız, okunabilir, yanlış kaçış hatası yok)");

        Console.WriteLine("""

        Verbatim (@"…"): yalnızca ters bölü ve satır sonunu kaçışsız yazar; tırnak yine "" ister. Raw string bunun yerini alır.
        Güvenlik: kullanıcı girdisini SQL/komut metnine interpolation ile gömmeyin → parametre kullanın (→ J8, J11.4). Interpolation sadece biçimlendirmedir.
        Kültür: {price:N2} CurrentCulture kullanır (makine-makine metinde FormattableString.Invariant / ToString(inv)).
        Modern derleyici interpolated string'leri 'handler' ile boxing'siz/ucuz üretir (logger'larda ek optimizasyon: LoggerMessage → J6.5).
        """);
    }
}
