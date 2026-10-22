using Microsoft.CSharp.RuntimeBinder;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section08_VarDynamicObject
{
    public static void Run()
    {
        Console.WriteLine("var     → DERLEME ZAMANI çıkarım. Hâlâ güçlü tipli! (dinamik değil)");
        var x = 5;                       // derleyici 'int' çıkardı; IL'de 'var' diye bir şey yok
        var list = new List<string>();
        Console.WriteLine($"   var x = 5 → {x.GetType().Name};   var list → {list.GetType().Name}");
        // x = "metin";                  // ✗ CS0029: string → int dönüşümü yok (var değişkenin tipi SABİTTİR)
        var anon = new { Ad = "Ayşe", Yas = 30 };
        Console.WriteLine($"   anonim tip için 'var' ZORUNLUDUR: {anon}  → {anon.GetType().Name}");

        Console.WriteLine("\nobject  → HER ŞEY; ama üyelere erişmek için CAST gerekir, value type için BOXING olur");
        object o = "merhaba";
        // Console.WriteLine(o.Length);  // ✗ CS1061: object'in Length'i yok
        Console.WriteLine($"   ((string)o).Length = {((string)o).Length}   — derleme zamanı güvenliği korunur, yanlış cast çalışma zamanında patlar");

        Console.WriteLine("\ndynamic → ÇALIŞMA ZAMANINDA binding; derleyici denetlemez, hata çalışırken çıkar");
        dynamic d = "merhaba";
        Console.WriteLine($"   d.Length = {d.Length}  (çalışma zamanında çözüldü)");
        try { Console.WriteLine(d.Olmayan); }
        catch (RuntimeBinderException ex) { Console.WriteLine($"   d.Olmayan → {ex.GetType().Name}: {ex.Message}"); }
        d = 42;                          // dynamic değişken tip değiştirebilir
        Console.WriteLine($"   d = 42 → d + 1 = {d + 1}");

        Console.WriteLine("""

        ┌─────────┬──────────────────────┬───────────────────────────┬──────────────────────────────────────┐
        │         │ Tip ne zaman belli?  │ Hata ne zaman çıkar?      │ Kullanım                             │
        ├─────────┼──────────────────────┼───────────────────────────┼──────────────────────────────────────┤
        │ var     │ derleme zamanı       │ derleme zamanı            │ Günlük kod (tip sağda belirgin ise)  │
        │ object  │ derleme zamanı=object│ cast anında (çalışırken)  │ Gerçekten "her şey" — genelde generic daha iyi │
        │ dynamic │ ÇALIŞMA ZAMANI       │ çalışırken                │ COM/Office interop, JSON gibi şemasız veri, nadiren │
        └─────────┴──────────────────────┴───────────────────────────┴──────────────────────────────────────┘
        dynamic: IntelliSense yok, derleme güvenliği yok, ilk çağrıda binder maliyeti var. Yeni kodda kaçının.
        """);
    }
}
