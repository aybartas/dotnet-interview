using Junior.CSharpFundamentals.ModernCSharp;

// J2.6 — Günlük Modern C#.
//   dotnet run          → bölüm listesi
//   dotnet run -- 10    → AYNI sipariş modeli, C# 7 tarzı vs C# 14 tarzı (satır sayısı + davranış eşitliği)
//   dotnet run -- all   → hepsi
// Testler: cd ../../tests/ModernCSharp.Tests && dotnet test
// Satır sayısı: ../compare-loc.sh

if (args.Length == 0)
{
    Console.WriteLine("J2.6 Günlük Modern C# — bölümler:\n");
    foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <numara> | all");
    return 0;
}

foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
{
    Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
    section.Run();
}
return 0;
