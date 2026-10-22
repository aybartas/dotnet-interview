using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class Section01_BinListing
{
    public static void Run()
    {
        var dir = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        var asm = typeof(Section01_BinListing).Assembly.GetName().Name!;
        Console.WriteLine($"Çıktı klasörü (bin): {dir}\n");

        Console.WriteLine($"{"Dosya",-42} {"Boyut",10}  Ne işe yarar?");
        Console.WriteLine(new string('─', 110));
        foreach (var file in new DirectoryInfo(dir).EnumerateFiles().OrderBy(f => f.Name, StringComparer.Ordinal))
            Console.WriteLine($"{file.Name,-42} {ProjectLocator.HumanSize(file.Length),10}  {Describe(file.Name, asm)}");

        foreach (var sub in new DirectoryInfo(dir).EnumerateDirectories())
            Console.WriteLine($"{sub.Name + "/",-42} {"",10}  alt klasör ({sub.EnumerateFiles("*", SearchOption.AllDirectories).Count()} dosya): yerelleştirilmiş/runtime'a özel kaynaklar olabilir");

        Console.WriteLine("""

        Eşleştirme tablosu (README 3.2): .dll = senin kodun · apphost = native başlatıcı · .pdb = debug sembolleri · .deps.json = bağımlılık manifesti
        · .runtimeconfig.json = runtime/framework ayarı · diğer .dll = NuGet bağımlılıkları · appsettings.json = CopyToOutputDirectory ile gelen içerik.
        Diğer .dll'ler (Azure.*, Microsoft.Identity.* …) Azure.Storage.Blobs NuGet paketinin ve bağımlılıklarının kopyalarıdır: bir paket eklenince onun .dll'leri de bin/'e kopyalanır.
        """);
    }

    private static string Describe(string name, string asm)
    {
        if (name == asm + ".dll") return "★ SENİN KODUN — IL + metadata (assembly)";
        if (name == asm || name == asm + ".exe") return "apphost — .dll'i başlatan native launcher (Windows'ta .exe, Linux/macOS'ta uzantısız)";
        if (name == asm + ".pdb") return "debug sembolleri: IL ↔ kaynak satır eşlemesi (breakpoint ve stack trace satır numarası bunun sayesinde)";
        if (name == asm + ".deps.json") return "bağımlılık manifesti: hangi assembly'ler/paketler; hostpolicy bunu okur";
        if (name == asm + ".runtimeconfig.json") return "hangi runtime/framework sürümü, roll-forward, GC/ayarlar; apphost/hostfxr bunu okur";
        if (name == "appsettings.json") return "CopyToOutputDirectory ile kopyalanan içerik dosyası (csproj'ta <None ... CopyToOutputDirectory>)";
        if (name.EndsWith(".dll", StringComparison.Ordinal)) return "NuGet/proje bağımlılığı (kütüphane)";
        if (name.EndsWith(".xml", StringComparison.Ordinal)) return "XML dokümantasyon (GenerateDocumentationFile)";
        return "?";
    }
}
