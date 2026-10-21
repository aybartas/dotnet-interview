using Junior.CSharpFundamentals.ExceptionHandling;

// J2.5 — Exception Handling.
//   dotnet run          → bölüm listesi
//   dotnet run -- 4     → throw vs throw ex ⭐
//   dotnet run -- all   → hepsi
// Testler: cd ../../tests/ExceptionHandling.Tests && dotnet test

if (args.Length == 0)
{
    Console.WriteLine("J2.5 Exception Handling — bölümler:\n");
    foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
    Console.WriteLine("\nÇalıştırmak için: dotnet run -- <numara> | all");
    return 0;
}

foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
{
    Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
    await section.Run();
}
return 0;
