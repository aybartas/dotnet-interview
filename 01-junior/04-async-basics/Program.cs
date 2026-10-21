using Junior.AsyncBasics;
using Junior.AsyncBasics.AsyncAwait;
using Junior.AsyncBasics.CancellationBasics;
using Junior.AsyncBasics.Concepts;

// J4 — Async Programlamaya Giriş. Bu modül "async all the way" ilkesine uyar: giriş noktası bile async (await kullanınca derleyici Main'i async Task<int> yapar).
//
//   dotnet run                    → konu listesi
//   dotnet run -- 4.1             → 4.1'in bölüm listesi
//   dotnet run -- 4.1 5           → 4.1'in 5. bölümü  (all: hepsi)

Topic[] topics =
[
    new("4.1", "Temel Kavramlar (concurrency, parallelism, I/O vs CPU)", ConceptsTopic.Run),
    new("4.2", "async/await Kullanımı",                                  AsyncAwaitTopic.Run),
    new("4.3", "Cancellation Temelleri",                                 CancellationBasicsTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("J4 Async Programlamaya Giriş — konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]");
    return args.Length == 0 ? 0 : 1;
}

return await topic.Run(args[1..]);
