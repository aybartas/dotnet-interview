using Junior.CollectionsLinq.LinqBasics;

// J3.3 — LINQ Temelleri.
//   dotnet run          → bölüm listesi
//   dotnet run -- 2     → deferred vs immediate execution ⭐
//   dotnet run -- 8     → 20 rapor alıştırması (çıktılar)
//   dotnet run -- all   → hepsi
// Testler: cd ../../tests/LinqBasics.Tests && dotnet test
// Alıştırma: Reports/Reports.cs içindeki soruları önce KENDİNİZ çözün, sonra çözümle karşılaştırın.

if (args.Length == 0)
{
    Console.WriteLine("J3.3 LINQ Temelleri — bölümler:\n");
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
