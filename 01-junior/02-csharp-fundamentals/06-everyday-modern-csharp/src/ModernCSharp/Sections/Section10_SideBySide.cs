using Junior.CSharpFundamentals.ModernCSharp.Comparison;

namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section10_SideBySide
{
    public static void Run()
    {
        IOrderFacade[] styles = [new Facade7(), new Facade14()];
        IReadOnlyList<(string Product, int Qty, decimal Price)> lines = [("Kalem", 2, 12.5m), ("Defter", 1, 40m), ("Çanta", 1, 980m)];

        Console.WriteLine("1) DAVRANIŞ AYNI mı? (iki stil, aynı girdiler)\n");
        foreach (var s in styles)
        {
            Console.WriteLine($"── {s.StyleName} ──");
            Console.WriteLine(s.Receipt("Gold", "Ayşe Yılmaz", "  AYSE@Example.com ", "Ankara", lines));
            Console.WriteLine($"   indirim oranları: Platinum/1500 = {s.DiscountRate("Platinum", 1500m):P0}, Gold/500 = {s.DiscountRate("Gold", 500m):P0}, Standard/1500 = {s.DiscountRate("Standard", 1500m):P0}");
            Console.WriteLine($"   durum geçişi: Pending+pay = {s.Advance("Pending", "pay")}, Paid+cancel = {s.Advance("Paid", "cancel")}");
            try { s.Advance("Delivered", "pay"); }
            catch (InvalidOperationException ex) { Console.WriteLine($"   geçersiz geçiş: {ex.Message}"); }
            Console.WriteLine($"   ShippingLabel(null) = {s.ShippingLabel(null)} · NormalizeEmail = {s.NormalizeEmail("  A@B.COM ")} · aynı iki sipariş eşit mi = {s.IdenticalOrdersAreEqual()}");
            Console.WriteLine($"   RenameIfPresent(true) = {s.RenameIfPresent(true, "yeni")} · (false) = {s.RenameIfPresent(false, "yeni")}\n");
        }

        var same = styles[0].Receipt("Gold", "Ayşe Yılmaz", "  AYSE@Example.com ", "Ankara", lines)
                == styles[1].Receipt("Gold", "Ayşe Yılmaz", "  AYSE@Example.com ", "Ankara", lines);
        Console.WriteLine($"Makbuz metinleri BİREBİR aynı mı? {same}\n");

        Console.WriteLine("2) SATIR SAYISI (boş ve yalnız-yorum satırları hariç) — domain model dosyaları");
        var loc7 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp7Style"), "Domain.cs"));
        var loc14 = LineCounter.Count(Path.Combine(LineCounter.StyleDirectory("Csharp14Style"), "Domain.cs"));
        Console.WriteLine($"   C# 7 tarzı  : {loc7,4} satır");
        Console.WriteLine($"   C# 14 tarzı : {loc14,4} satır   → %{100 - 100.0 * loc14 / loc7:F0} daha az");

        Console.WriteLine("""

        Neden azaldı?
          Address / OrderLine      : elle Equals/GetHashCode/ToString/WithXxx  →  tek satır `record`
          Customer.Email           : alan + property + doğrulama               →  `field` keyword
          Order (durum makinesi)   : switch statement + break + değişken       →  switch expression + `with`
          DiscountRate             : iç içe if/switch + geçici değişken        →  tuple pattern + relational pattern
          Receipt                  : StringBuilder + string.Format             →  interpolated raw string literal
          Namespace + using        : blok girintisi + 6 using                  →  file-scoped namespace + implicit/global using
          Koleksiyon kurma         : new List<>/ReadOnlyCollection             →  collection expression `[.. xs]`
        OKUNABİLİRLİK: yeni stil "NE olması gerektiğini" söyler (deklaratif); eski stil "NASIL yapılacağını" adım adım yazar.
        Ama kısa ≠ her zaman iyi: ileri pattern/primary constructor zinciri deneyimsiz okuyucuya yabancı gelebilir; takım standardı ve okunabilirlik önce gelir.
        """);
    }
}
