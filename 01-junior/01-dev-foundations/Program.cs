using Junior.DevFoundations;
using Junior.DevFoundations.HowTheWebWorks;
using Junior.DevFoundations.IdeDebugging.Exercises;
using Junior.DevFoundations.NetworkFundamentals;

// J1 — Geliştirici Temelleri. Kod içeren konular burada çalışır; J1.1 (Git) ve J1.4 (.NET CLI) yalnızca README'dir.
//
//   dotnet run                    → konu listesi
//   dotnet run -- 1.2 help        → ağ konusunun komutları
//   dotnet run -- 1.3             → Kestrel ile ham HTTP isteği (http://localhost:5080)
//   dotnet run -- 1.5 3           → 3. debugger egzersizi

Topic[] topics =
[
    new("1.2", "Ağ Temelleri (TCP / UDP / Socket)",  NetworkFundamentalsTopic.Run),
    new("1.3", "Web Nasıl Çalışır? (Kestrel)",       HowTheWebWorksTopic.Run),
    new("1.5", "IDE, Debugging & Kod Kalitesi",      IdeDebuggingTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("J1 Geliştirici Temelleri — çalıştırılabilir konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [argümanlar]   (1.1 Git ve 1.4 .NET CLI: yalnızca README)");
    return args.Length == 0 ? 0 : 1;
}

return topic.Run(args[1..]);
