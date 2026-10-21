namespace Junior.CSharpFundamentals.Oop;

public static class OopTopic
{
    public static int Run(string[] args)
    {
        // J2.3 — OOP Temelleri.
        //   dotnet run -- 2.3          → bölüm listesi
        //   dotnet run -- 2.3 4     → 4. bölüm (new vs override ⭐)
        //   dotnet run -- 2.3 all   → hepsi
        //   Section06: aggregation vs composition yaşam süresi farkı (WeakReference + GC ile kanıt)
        //   Section09: inheritance → composition refactor'unun HER adımında aynı senaryolar aynı sonucu verir

        if (args.Length == 0)
        {
            Console.WriteLine("J2.3 OOP Temelleri — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 2.3 <numara> | all");
            return 0;
        }

        foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
