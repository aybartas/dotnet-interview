using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BigPicture;

public static class Section01_DllAndApphost
{
    public static void Run()
    {
        var assembly = typeof(Samples).Assembly;
        var dll = assembly.Location;
        var dir = Path.GetDirectoryName(dll)!;
        var name = assembly.GetName().Name!;
        var apphost = Directory.EnumerateFiles(dir, name + "*").FirstOrDefault(f => Path.GetFileName(f) is var n && (n == name || n == name + ".exe"));

        Console.WriteLine("Özet hat:");
        Console.WriteLine("  C# kaynak → [Roslyn] → IL + metadata içeren .dll → [MSBuild] bin/ klasörü → apphost → hostfxr → hostpolicy → CoreCLR → [JIT] native kod → CPU\n");

        Console.WriteLine($"1) SENİN KODUN HER ZAMAN .dll: {Path.GetFileName(dll)}  ({ProjectLocator.HumanSize(new FileInfo(dll).Length)})");
        Describe("   ", dll);

        if (apphost is not null)
        {
            Console.WriteLine($"\n2) '.exe' dediğimiz şey APPHOST: {Path.GetFileName(apphost)}  ({ProjectLocator.HumanSize(new FileInfo(apphost).Length)})");
            Describe("   ", apphost);
            Console.WriteLine("   → Küçük, NATIVE bir launcher'dır; içinde senin C# kodun YOKTUR. Tek işi: .NET runtime'ı bulup .dll'i başlatmak.");
        }
        else
        {
            Console.WriteLine("\n2) apphost bu klasörde bulunamadı (UseAppHost=false olabilir; o zaman 'dotnet <dll>' ile çalıştırılır).");
        }

        Console.WriteLine("\n3) Şu an kim çalışıyor?");
        Console.WriteLine($"   Environment.ProcessPath       : {Environment.ProcessPath}");
        Console.WriteLine($"   Çalışan runtime                : {RuntimeInformation.FrameworkDescription}  ({RuntimeInformation.RuntimeIdentifier})");
        Console.WriteLine($"   Runtime klasörü (CoreCLR + BCL): {RuntimeEnvironment_GetDirectory()}");
        Console.WriteLine($"   System.Private.CoreLib.dll     : {typeof(object).Assembly.Location}   ← object, string, List<T>... bu .dll'de (BCL), o da IL'dir");
        Console.WriteLine($"   Giriş noktası                   : {Assembly.GetEntryAssembly()!.EntryPoint!.DeclaringType!.Name}.{Assembly.GetEntryAssembly()!.EntryPoint!.Name} (top-level statements → derleyici üretti)");

        Console.WriteLine("""

        Başlatma zinciri ('MyApp' yazınca):
          1. apphost (MyApp / MyApp.exe)     → native; MyApp.runtimeconfig.json'u okuyup hangi runtime/framework gerektiğini öğrenir
          2. hostfxr                         → doğru .NET sürümünü seçer (roll-forward kuralları; makine genelindeki dotnet kurulumunda)
          3. hostpolicy                      → MyApp.deps.json'a göre bağımlılıkları (assembly yollarını) çözer
          4. CoreCLR (runtime)               → .dll'i yükler, belleği/GC'yi başlatır
          5. JIT                             → IL'i metotlar ilk çağrıldıkça makine koduna çevirir; CPU çalıştırır

        ⭐ "C# kodu nasıl .exe oluyor?" → OLMUYOR. Kodun IL olarak .dll'dedir; .exe yalnızca o .dll'i başlatan küçük bir native başlatıcıdır
          (`dotnet MyApp.dll` ile apphost'a hiç gerek kalmadan da çalışır). Native koda çeviri çalışma zamanında JIT ile yapılır
          (istisna: Native AOT / ReadyToRun → M2.5).
        """);
    }

    private static string RuntimeEnvironment_GetDirectory() => System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

    private static void Describe(string indent, string path)
    {
        var magic = new byte[4];
        using (var head = File.OpenRead(path)) _ = head.Read(magic, 0, 4);

        if (magic[0] == 'M' && magic[1] == 'Z')                          // Windows PE (hem yönetilen .dll hem native apphost.exe)
        {
            using var stream = File.OpenRead(path);
            using var pe = new PEReader(stream);
            if (pe.HasMetadata)
            {
                var cor = pe.PEHeaders.CorHeader!;
                Console.WriteLine($"{indent}PE dosyası: YÖNETİLEN (managed) — CLR başlığı var, ILOnly={(cor.Flags & CorFlags.ILOnly) != 0}, makine etiketi={pe.PEHeaders.CoffHeader.Machine} (AnyCPU IL'inde 'I386' normaldir; 32-bit demek değildir)");
                Console.WriteLine($"{indent}İçeriği IL + metadata (tip/metot tabloları, string'ler); işlemciye özel makine kodu yok → platform bağımsız.");
            }
            else
            {
                Console.WriteLine($"{indent}PE dosyası: NATIVE — CLR metadata'sı yok (managed değil), makine={pe.PEHeaders.CoffHeader.Machine}");
            }
        }
        else if (magic[0] == 0x7F && magic[1] == 'E' && magic[2] == 'L' && magic[3] == 'F')
        {
            Console.WriteLine($"{indent}ELF dosyası (Linux): NATIVE çalıştırılabilir — CLR metadata'sı yok; içinde C# kodu değil, runtime'ı bulup başlatan küçük bir launcher var.");
        }
        else
        {
            Console.WriteLine($"{indent}Mach-O dosyası (macOS): NATIVE çalıştırılabilir — CLR metadata'sı yok; runtime'ı bulup başlatan küçük bir launcher.");
        }
    }
}
