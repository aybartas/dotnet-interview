using Junior.CSharpFundamentals.Oop;

// J2.3 — OOP Temelleri.
//   dotnet run          → bölüm listesi
//   dotnet run -- 4     → 4. bölüm (new vs override ⭐)
//   dotnet run -- all   → hepsi
//
// Ayrıca testler:  cd ../../tests/Oop.Tests && dotnet test
//   • PaymentRefactoringTests → inheritance → composition refactor'unun HER adımında aynı davranış testleri yeşil
//   • RelationshipTests       → aggregation vs composition yaşam süresi farkı (WeakReference + GC ile kanıt)

if (args.Length == 0)
{
    Console.WriteLine("J2.3 OOP Temelleri — bölümler:\n");
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
