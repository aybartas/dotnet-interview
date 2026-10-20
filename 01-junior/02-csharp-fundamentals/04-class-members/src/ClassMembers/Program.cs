using Junior.CSharpFundamentals.ClassMembers;

// J2.4 — Sınıf Üyeleri & Nesne Semantiği.
//   dotnet run          → bölüm listesi
//   dotnet run -- 2     → başlatma sırası ⭐
//   dotnet run -- all   → hepsi
// Testler: cd ../../tests/ClassMembers.Tests && dotnet test

if (args.Length == 0)
{
    Console.WriteLine("J2.4 Sınıf Üyeleri & Nesne Semantiği — bölümler:\n");
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
