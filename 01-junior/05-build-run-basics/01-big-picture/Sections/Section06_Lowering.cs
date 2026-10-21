namespace Junior.BuildRunBasics.BigPicture;

/// <summary>
/// 🛠 J5.1: 'foreach' ve 'using'in "lowered" (derleyicinin açtığı) hali. Aynı mantığı elle yazıp sonucu karşılaştırırız;
/// gerçeğini sharplab.io'da (sağ panel: C# / IL / "Lowered") görebilirsiniz.
/// </summary>
public static class Section06_Lowering
{
    private sealed class Resource(string name) : IDisposable
    {
        public void Dispose() => Console.WriteLine($"   [{name}] Dispose() çağrıldı");
    }

    public static void Run()
    {
        // ─── foreach ───
        var list = new List<int> { 1, 2, 3 };

        var sum1 = 0;
        foreach (var item in list) sum1 += item;                       // yazdığımız

        var sum2 = 0;
        {                                                              // derleyicinin açtığı (yaklaşık):
            List<int>.Enumerator e = list.GetEnumerator();             //   struct enumerator (kutulanmaz)
            try
            {
                while (e.MoveNext())
                {
                    var item = e.Current;
                    sum2 += item;
                }
            }
            finally
            {
                e.Dispose();                                           //   foreach, enumerator'ı HER ZAMAN dispose eder
            }
        }
        Console.WriteLine($"foreach        → {sum1}\nlowered while  → {sum2}   (aynı sonuç)\n");

        // ─── using ───
        Console.WriteLine("using var r = new Resource(\"A\");  // yazdığımız");
        {
            using var r = new Resource("A");
            Console.WriteLine("   [A] kullanılıyor");
        }

        Console.WriteLine("\nlowered: try/finally");
        {
            var r = new Resource("B");
            try
            {
                Console.WriteLine("   [B] kullanılıyor");
            }
            finally
            {
                if (r != null) r.Dispose();                            // null kontrolü: using null kaynağı sessizce geçer
            }
        }

        // ─── dizi foreach ───
        int[] array = [4, 5, 6];
        var sum3 = 0;
        for (var i = 0; i < array.Length; i++) sum3 += array[i];       // dizide foreach IL'de indeksli for'a iner (enumerator yok)
        Console.WriteLine($"\ndizi foreach → indeksli for: {sum3}");

        Console.WriteLine("""

        sharplab.io'da deneyin (kod kutusuna yapıştırın, sağ üstten "Results: Lowered C#" / "IL" seçin):
            using System; using System.Collections.Generic;
            public class C { public int M(List<int> l) { var s = 0; foreach (var x in l) s += x; return s; } }
        Göreceğiniz: GetEnumerator → try { while (MoveNext) { x = Current; ... } } finally { Dispose }.

        Diğer "şeker" (syntactic sugar) örnekleri — hepsi derleyicide başka koda açılır:
          • using / await using  → try/finally
          • foreach              → GetEnumerator + MoveNext/Current + try/finally (dizi için for)
          • lock                 → Monitor.Enter/Exit (veya .NET 9+ System.Threading.Lock.EnterScope)
          • async/await          → state machine sınıfı (→ M4.2)
          • string interpolation → DefaultInterpolatedStringHandler
          • record, primary ctor, pattern matching, switch expression → sıradan sınıf/if-else kodu
          • lambda / closure     → derleyicinin ürettiği 'display class' (→ J3.2)
        """);
    }
}
