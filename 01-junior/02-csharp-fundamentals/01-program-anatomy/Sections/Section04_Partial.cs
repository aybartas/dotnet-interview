using System.Text.RegularExpressions;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section04_Partial
{
    public static void Run()
    {
        // partial class: tek tip, birden çok dosyada. Invoice.cs (elle yazılan) + Invoice.Generated.cs (kod üretici çıktısı gibi).
        var invoice = new Invoice("INV-001", 100m);
        Console.WriteLine(invoice.Describe());

        // partial METOT: bildirim bir dosyada, uygulama başka dosyada. Source generator'lar tam olarak bunu kullanır:
        //   [GeneratedRegex(...)] static partial Regex Digits();   → derleyici, metodun GÖVDESİNİ derleme sırasında üretir.
        var m = Generated.Digits().Match("Sipariş no: 4521");
        Console.WriteLine($"\n[GeneratedRegex] partial metodu → eşleşme: {m.Value}");
        Console.WriteLine("Bu metodun üretilen kaynağını IDE'de F12 ile (Analyzers → ...RegexGenerator) görebilirsiniz.");
    }
}

public static partial class Generated
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex Digits();
}
