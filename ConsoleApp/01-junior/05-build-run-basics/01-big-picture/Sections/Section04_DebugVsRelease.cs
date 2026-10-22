using System.Diagnostics;
using System.Reflection;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section04_DebugVsRelease
{
    public static void Run()
    {
        var assembly = typeof(Samples).Assembly;
        var debuggable = assembly.GetCustomAttribute<DebuggableAttribute>();
        var configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? "?";
        var optimizerDisabled = debuggable?.IsJITOptimizerDisabled ?? false;

        Console.WriteLine($"Bu .dll'in derleme yapılandırması     : {configuration}");
        Console.WriteLine($"JIT optimizasyonu kapalı mı?          : {optimizerDisabled}   (DebuggableAttribute.IsJITOptimizerDisabled)");

#if DEBUG
        Console.WriteLine("DEBUG sembolü tanımlı mı?             : EVET (#if DEBUG bloğu derlendi)");
#else
        Console.WriteLine("DEBUG sembolü tanımlı mı?             : HAYIR (#if DEBUG bloğu derlemeden SİLİNDİ)");
#endif

        var assertRan = false;
        Debug.Assert(Mark(ref assertRan), "Debug.Assert argümanı");         // [Conditional("DEBUG")]: Release'de çağrı VE argüman ifadesi silinir
        Console.WriteLine($"Debug.Assert içindeki ifade çalıştı mı: {assertRan}   (Release'de false → yan etkili ifadeyi Assert'e koymayın!)\n");

        var addIl = IlDisassembler.Disassemble(typeof(Samples).GetMethod(nameof(Samples.AddWithLocal))!);
        Console.WriteLine($"AddWithLocal IL'i ('var result = a + b; return result;') — {addIl.Count} komut, {addIl.Count(l => l.Contains(" nop"))} adet 'nop':");
        foreach (var line in addIl) Console.WriteLine($"     {line}");

        Console.WriteLine("""

        Karşılaştırmak için iki kez çalıştırın:
            dotnet run -- J5.1 4                 (Debug: nop'lar + gereksiz yerel değişken IL'de durur, optimizasyon kapalı)
            dotnet run -c Release -- J5.1 4      (Release: IL sadeleşir, DEBUG sembolü yok, JIT optimizasyonu açık)

        Debug vs Release:
          • Derleyici optimizasyonu : Debug KAPALI (nop'lar, geçici değişkenler) · Release AÇIK (IL sadeleşir)
          • JIT optimizasyonu       : Debug kapalı (tier-0 / minopts) · Release açık (tiered → tier-1)
          • DEBUG sembolü           : Debug'da tanımlı (#if DEBUG çalışır) · Release'de tanımlı değil
          • Debug.Assert, [Conditional("DEBUG")] : Debug'da çalışır · Release'de çağrı VE argüman ifadeleri SİLİNİR
          • Debugger deneyimi       : Debug: satır satır, her değişken görünür · Release: değişkenler "optimized away", satırlar atlar
          • Hız / boyut             : Debug yavaş ve büyük · Release hızlı ve küçük
        • `dotnet build` / `dotnet run` varsayılanı Debug; `dotnet publish` varsayılanı Release (.NET 8+). Üretime Debug çıktısı gitmemeli.
        • "Debug'da çalışıyor, Release'de çalışmıyor" gerçek bir sorun sınıfıdır (yan etkili assert, zamanlama/race, #if DEBUG farkı). İkisini de test edin.
        """);
    }

    private static bool Mark(ref bool flag)
    {
        flag = true;
        return true;
    }
}
