using System.Diagnostics;
using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Section01_CommandsAndLaunchSettings
{
    public static void Run()
    {
        Console.WriteLine("""
        ┌──────────────────┬───────────────────────────────────┬──────────────────────────────────┬──────────────────────────────┐
        │ Komut            │ Ne yapar                          │ Çıktı                            │ Ne zaman                     │
        ├──────────────────┼───────────────────────────────────┼──────────────────────────────────┼──────────────────────────────┤
        │ dotnet restore   │ Bağımlılıkları çözer/indirir      │ obj/project.assets.json          │ CI'da explicit adım          │
        │ dotnet build     │ Derler (implicit restore)         │ bin/<config>/<tfm>/              │ Geliştirme, derleme doğrulama│
        │ dotnet run       │ Build eder VE çalıştırır          │ Çalışan process                  │ SADECE geliştirme            │
        │ dotnet watch     │ Değişimde yeniden çalıştırır      │ —                                │ Geliştirme döngüsü           │
        │ dotnet publish   │ DAĞITIMA HAZIR klasör üretir      │ bin/<config>/<tfm>/publish/      │ DEPLOYMENT                   │
        │ dotnet test      │ Testleri derleyip çalıştırır      │ Test sonuçları                   │ CI                           │
        │ dotnet pack      │ NuGet paketi üretir               │ .nupkg                           │ Kütüphane yayını             │
        └──────────────────┴───────────────────────────────────┴──────────────────────────────────┴──────────────────────────────┘
        Varsayılan yapılandırma: build/run = Debug, publish = Release (.NET 8+).
        """);

        var demoEnv = Environment.GetEnvironmentVariable("DEMO_ENV");
        var processName = Path.GetFileNameWithoutExtension(Environment.ProcessPath!);
        Console.WriteLine($"Bu program nasıl başlatıldı?  ProcessPath = {Environment.ProcessPath}");
        Console.WriteLine($"DEMO_ENV ortam değişkeni      = {(demoEnv is null ? "(tanımsız)" : $"\"{demoEnv}\"")}");

        var launchSettingsInBin = File.Exists(Path.Combine(AppContext.BaseDirectory, "launchSettings.json"));
        var projectDir = ProjectLocator.FindProjectDirectory();
        var launchSettingsInSource = projectDir is not null && File.Exists(Path.Combine(projectDir, "Properties", "launchSettings.json"));
        Console.WriteLine($"launchSettings.json kaynakta (Properties/) var mı? {launchSettingsInSource}  ·  çıktı klasöründe (bin/) var mı? {launchSettingsInBin}");

        Console.WriteLine($"""

        Gözlem:
          • `dotnet run`, Properties/launchSettings.json'daki profili okur ve DEMO_ENV'i uygulamaya VERİR → yukarıda {(demoEnv is null ? "tanımsız görünüyorsa program dotnet run ile değil doğrudan başlatılmıştır" : "değer göründü: dotnet run profile'ı uyguladı")}.
          • Doğrudan çalıştırınca (bin/Debug/net10.0/{typeof(Section01_CommandsAndLaunchSettings).Assembly.GetName().Name} veya `dotnet <dll>`) launchSettings.json HİÇ okunmaz → DEMO_ENV tanımsız.
          • launchSettings.json yalnızca Visual Studio / `dotnet run` içindir; bin/ ve publish çıktısına GİRMEZ.
            Production ayarları ortam değişkeni (ASPNETCORE_ENVIRONMENT, bağlantı dizesi…), appsettings.<Ortam>.json veya gizli depodan (Key Vault) gelir.

        Deneyin:
            dotnet run -- 5.3 1                                                    → DEMO_ENV dolu (launchSettings)
            dotnet bin/Debug/net10.0/{typeof(Section01_CommandsAndLaunchSettings).Assembly.GetName().Name}.dll 5.3 1   → DEMO_ENV tanımsız
            dotnet run --no-launch-profile -- 5.3 1                                → launchSettings'i hiç kullanma

        ⭐ `dotnet run` neden production'da kullanılmaz?
          1. SDK gerektirir (sunucuda/container'da derleme araçları = büyük image, büyük saldırı yüzeyi).
          2. Her çalıştırmada build/up-to-date kontrolü yapar (yavaş başlangıç, beklenmedik yeniden derleme).
          3. Kaynak kod ve build araçları sunucuda durur.
          4. launchSettings.json (geliştirme profili) devreye girer — üretim ayarı DEĞİL.
          Üretim: `dotnet publish` çıktısı + yalnızca runtime (framework-dependent) ya da self-contained/AOT.
        """);
    }
}
