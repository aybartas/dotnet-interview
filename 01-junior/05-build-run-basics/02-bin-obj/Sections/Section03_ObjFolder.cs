using System.Security.Cryptography;
using System.Text.Json;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BinObj;

public static class Section03_ObjFolder
{
    public static void Run()
    {
        var projectDir = ProjectLocator.FindProjectDirectory();
        if (projectDir is null)
        {
            Console.WriteLine("Proje klasörü bulunamadı (uygulama publish edilmiş bir kopyadan çalışıyor olabilir: obj/ yalnızca geliştirme makinesinde, kaynakla birlikte bulunur).");
            return;
        }

        var obj = Path.Combine(projectDir, "obj");
        Console.WriteLine($"obj/ = ara çıktılar (restore sonucu + derleme sırasında üretilen dosyalar): {obj}\n");

        Console.WriteLine("Kök dosyalar:");
        foreach (var file in new DirectoryInfo(obj).EnumerateFiles().OrderBy(f => f.Name, StringComparer.Ordinal))
            Console.WriteLine($"   {file.Name,-52} {ProjectLocator.HumanSize(file.Length),9}  {DescribeRoot(file.Name)}");

        var config = new DirectoryInfo(AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar)).Parent!.Name;   // bin/<config>/<tfm> → çalışan çıktının yapılandırması
        var tfmDir = Directory.Exists(Path.Combine(obj, config)) ? new DirectoryInfo(Path.Combine(obj, config)).EnumerateDirectories().FirstOrDefault() : null;
        if (tfmDir is not null)
        {
            Console.WriteLine($"\nobj/{config}/{tfmDir.Name}/ (derleme ara çıktıları):");
            foreach (var file in tfmDir.EnumerateFiles().OrderBy(f => f.Name, StringComparer.Ordinal))
                Console.WriteLine($"   {file.Name,-52} {ProjectLocator.HumanSize(file.Length),9}  {DescribeIntermediate(file.Name)}");

            var objDll = Path.Combine(tfmDir.FullName, "J05.BuildRunBasics.dll");
            var binDll = Path.Combine(AppContext.BaseDirectory, "J05.BuildRunBasics.dll");
            if (File.Exists(objDll) && File.Exists(binDll))
                Console.WriteLine($"\nobj'daki .dll ile bin'deki .dll AYNI mı? {Hash(objDll) == Hash(binDll)}   (derleyici önce obj/'a yazar; MSBuild bin/'e KOPYALAR)");

            var globalUsings = tfmDir.EnumerateFiles("*.GlobalUsings.g.cs").FirstOrDefault();
            if (globalUsings is not null)
                Console.WriteLine($"\n{globalUsings.Name} — ImplicitUsings'in gerçek yüzü (her .cs dosyasında geçerli global using'ler):\n{Indent(File.ReadAllText(globalUsings.FullName))}");
        }

        var assets = Path.Combine(obj, "project.assets.json");
        if (File.Exists(assets))
        {
            using var doc = JsonDocument.Parse(File.ReadAllText(assets));
            var targets = doc.RootElement.GetProperty("targets").EnumerateObject().Select(t => t.Name);
            var folders = doc.RootElement.GetProperty("packageFolders").EnumerateObject().Select(f => f.Name);
            Console.WriteLine($"project.assets.json özeti: targets = {string.Join(", ", targets)}; NuGet paket klasörü = {string.Join(", ", folders)}");
        }

        Console.WriteLine("""

        obj/ = MSBuild'in çalışma masası: restore sonucu (project.assets.json), NuGet'in ürettiği props/targets, AssemblyInfo, GlobalUsings, ara .dll.
        bin/ = dağıtılabilir son çıktı. İkisi de ÜRETİLEBİLİR dosyalardır → .gitignore'a girer, commit'lenmez. Sorun çıkınca silip yeniden üretilir.
        """);
    }

    private static string Hash(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)));

    private static string Indent(string text) => string.Join('\n', text.Split('\n').Select(l => "      " + l.TrimEnd()).Where(l => l.Trim().Length > 0).Take(14));

    private static string DescribeRoot(string name) => name switch
    {
        "project.assets.json" => "★ restore sonucu: çözülen paketler/bağımlılık grafı (IDE ve derleyici bunu okur)",
        _ when name.EndsWith(".nuget.g.props", StringComparison.Ordinal) => "NuGet'in ürettiği MSBuild props (paketlerin eklediği ayarlar)",
        _ when name.EndsWith(".nuget.g.targets", StringComparison.Ordinal) => "NuGet'in ürettiği MSBuild targets",
        _ when name.EndsWith(".nuget.dgspec.json", StringComparison.Ordinal) => "restore girdisi (proje/paket grafı özeti)",
        _ when name.EndsWith(".nuget.cache", StringComparison.Ordinal) => "restore önbelleği (değişmediyse restore atlanır)",
        _ => "?",
    };

    private static string DescribeIntermediate(string name) => name switch
    {
        _ when name.EndsWith(".AssemblyInfo.cs", StringComparison.Ordinal) => "derleyiciye giren ÜRETİLMİŞ kaynak: assembly öznitelikleri (sürüm, yapılandırma)",
        _ when name.EndsWith(".GlobalUsings.g.cs", StringComparison.Ordinal) => "ÜRETİLMİŞ kaynak: ImplicitUsings → global using listesi",
        _ when name.EndsWith(".AssemblyInfoInputs.cache", StringComparison.Ordinal) => "artımlı derleme önbelleği",
        _ when name.EndsWith(".GeneratedMSBuildEditorConfig.editorconfig", StringComparison.Ordinal) => "MSBuild özelliklerinden üretilen analyzer ayarları",
        _ when name.EndsWith(".dll", StringComparison.Ordinal) => "★ derleyicinin ürettiği ara .dll (bin/'e kopyalanır)",
        _ when name.EndsWith(".pdb", StringComparison.Ordinal) => "ara debug sembolleri",
        _ when name.EndsWith(".csproj.CoreCompileInputs.cache", StringComparison.Ordinal) => "artımlı derleme: girdi özeti",
        _ when name.EndsWith(".csproj.FileListAbsolute.txt", StringComparison.Ordinal) => "dotnet clean'in neyi sileceğini bilmesi için dosya listesi",
        "apphost" or "apphost.exe" => "apphost şablonunun bu projeye özel kopyası (bin/'e kopyalanır)",
        _ => "ara/üretilmiş dosya",
    };
}
