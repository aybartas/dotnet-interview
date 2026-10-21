using System.Reflection;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section02_ReadingIl
{
    public static void Run()
    {
        Console.WriteLine("IL = yığın (stack) tabanlı, platformdan bağımsız ara dil. 'a + b' için: iki değeri yığına koy, topla, sonucu döndür.\n");
        Show(nameof(Samples.Add), "public static int Add(int a, int b) => a + b;");
        Show(nameof(Samples.SumArray), "foreach (var item in items) sum += item;   // dizi üzerinde: IL'de foreach YOK, indeksli döngü var");
        Show(nameof(Samples.SumList), "foreach (var item in items) sum += item;   // List<T> üzerinde: GetEnumerator/MoveNext/Current + try/finally");
        Show(nameof(Samples.ReadFirstLine), "using var reader = new StreamReader(path); ...  // using: try/finally + Dispose");
        Show(nameof(Samples.Greet), "$\"Merhaba {name}, {count}\"   // interpolation: DefaultInterpolatedStringHandler çağrıları");

        Console.WriteLine("""

        Çıkarımlar:
          • İlk/son satırlar: ldarg = argümanı yığına koy, ldloc/stloc = yerel değişken, add/ret = topla/döndür, call/callvirt = metot çağrısı,
            br/blt/brtrue = dallanma (if/for/while hepsi bunlara iner).
          • 'foreach' ve 'using' bir DİL özelliğidir — IL'de karşılığı vardır ama 'foreach'/'using' diye bir komut yoktur (→ 6. bölüm).
          • Gerçek araçlar: ILSpy (.dll'i C#/IL olarak açar), ildasm, dotnet-ildasm, sharplab.io (yazdığın C#'ın IL'ini ve 'lowered' C#'ını canlı gösterir).
            Deneyin: bin/Debug/net10.0/J05.BuildRunBasics.dll dosyasını ILSpy'de açıp BigPicture → Samples sınıfına bakın.
        """);
    }

    private static void Show(string methodName, string source)
    {
        var method = typeof(Samples).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)!;
        Console.WriteLine($"── {source}");
        foreach (var line in IlDisassembler.Disassemble(method)) Console.WriteLine($"     {line}");
        Console.WriteLine();
    }
}
