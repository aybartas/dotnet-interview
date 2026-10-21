using Junior.BuildRunBasics;
using Junior.BuildRunBasics.BigPicture;
using Junior.BuildRunBasics.BinObj;
using Junior.BuildRunBasics.RunBuildPublish;

// J5 — Build & Run Temelleri: "kod derlenince ne oluyor?" Bu program kendi çıktısını (bin/), ara dosyalarını (obj/) ve IL'ini inceler.
//
//   dotnet run                    → konu listesi
//   dotnet run -- 5.1             → 5.1'in bölüm listesi
//   dotnet run -- 5.1 2           → 5.1'in 2. bölümü  (all: hepsi)
//   dotnet run -c Release -- 5.1 4   → Debug/Release farkını karşılaştırmak için

Topic[] topics =
[
    new("5.1", "C# → IL → Native: Büyük Resim",  BigPictureTopic.Run),
    new("5.2", "bin/ ve obj/",                    BinObjTopic.Run),
    new("5.3", "run vs build vs publish",         RunBuildPublishTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("J5 Build & Run Temelleri — konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]");
    return args.Length == 0 ? 0 : 1;
}

return topic.Run(args[1..]);
