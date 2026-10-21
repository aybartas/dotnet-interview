using Junior.CollectionsLinq.DelegatesLambdasEvents;

// J3.2 — Delegates, Lambdas & Events.
//   dotnet run          → bölüm listesi
//   dotnet run -- 3     → closure tuzağı ⭐
//   dotnet run -- all   → hepsi
// Testler: cd ../../tests/DelegatesLambdasEvents.Tests && dotnet test

if (args.Length == 0)
{
    Console.WriteLine("J3.2 Delegates, Lambdas & Events — bölümler:\n");
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
