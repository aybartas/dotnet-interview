namespace Junior.CollectionsLinq.LinqBasics;

public static class LinqBasicsTopic
{
    public static int Run(string[] args)
    {
        // J3.3 — LINQ Temelleri.
        //   dotnet run -- 3.3          → bölüm listesi
        //   dotnet run -- 3.3 2     → deferred vs immediate execution ⭐
        //   dotnet run -- 3.3 8     → 20 rapor alıştırması (çıktılar)
        //   dotnet run -- 3.3 all   → hepsi
        // Alıştırma: Reports/Reports.cs içindeki soruları önce KENDİNİZ çözün, sonra çözümle karşılaştırın.

        if (args.Length == 0)
        {
            Console.WriteLine("J3.3 LINQ Temelleri — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 3.3 <numara> | all");
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
