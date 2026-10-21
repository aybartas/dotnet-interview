using Junior.CSharpFundamentals;
using Junior.CSharpFundamentals.ClassMembers;
using Junior.CSharpFundamentals.ExceptionHandling;
using Junior.CSharpFundamentals.ModernCSharp;
using Junior.CSharpFundamentals.Oop;
using Junior.CSharpFundamentals.ProgramAnatomy;
using Junior.CSharpFundamentals.TypeSystemMemory;

// J2 — C# Temelleri. Bu dosyanın KENDİSİ "top-level statements" örneğidir (bkz. J2.1 Section01):
// class ve Main yazmadık; derleyici üretti. `args` komut satırı argümanlarıdır, `return <int>` process exit code'udur.
//
//   dotnet run                    → konu listesi
//   dotnet run -- 2.3             → 2.3'ün bölüm listesi
//   dotnet run -- 2.3 4           → 2.3'ün 4. bölümü  (all: hepsi)

Topic[] topics =
[
    new("2.1", "Program Anatomisi",                ProgramAnatomyTopic.Run),
    new("2.2", "Tip Sistemi & Bellek Modeli",      TypeSystemMemoryTopic.Run),
    new("2.3", "OOP Temelleri",                    OopTopic.Run),
    new("2.4", "Sınıf Üyeleri & Nesne Semantiği",  ClassMembersTopic.Run),
    new("2.5", "Exception Handling",               ExceptionHandlingTopic.Run),
    new("2.6", "Günlük Modern C#",                 ModernCSharpTopic.Run),
];

if (args.Length == 0 || topics.FirstOrDefault(t => t.Id == args[0]) is not { } topic)
{
    Console.WriteLine("J2 C# Temelleri — konular:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id}  {t.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <konu> [bölüm | all]");
    return args.Length == 0 ? 0 : 1;
}

return topic.Run(args[1..]);
