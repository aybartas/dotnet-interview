using Junior.CollectionsLinq;
using Junior.CollectionsLinq.CollectionsGenerics;
using Junior.CollectionsLinq.DelegatesLambdasEvents;
using Junior.CollectionsLinq.LinqBasics;

// J3 — Collections, Delegates & LINQ.
//
//   dotnet run                    → konu listesi
//   dotnet run -- 3.3             → 3.3'ün bölüm listesi
//   dotnet run -- 3.3 2           → 3.3'ün 2. bölümü  (all: hepsi)

Topic[] topics =
[
    new("3.1", "Collections & Generics",     CollectionsGenericsTopic.Run),
    new("3.2", "Delegates, Lambdas & Events", DelegatesLambdasEventsTopic.Run),
    new("3.3", "LINQ Temelleri",              LinqBasicsTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("J3 Collections, Delegates & LINQ — konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]");
    return args.Length == 0 ? 0 : 1;
}

return topic.Run(args[1..]);
