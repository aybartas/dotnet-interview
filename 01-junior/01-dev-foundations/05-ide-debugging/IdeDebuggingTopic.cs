using Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

// Not: Exercises sınıfı ile aynı isimli namespace'te durmak zorundayız (Exercises.All çözümlemesi için).
namespace Junior.DevFoundations.IdeDebugging.Exercises;

public static class IdeDebuggingTopic
{
    public static int Run(string[] args)
    {
        // J1.5 — Debugger alıştırmaları. Her egzersiz BİLEREK hatalıdır.
        //
        //   dotnet run -- 1.5          → egzersiz listesi
        //   dotnet run -- 1.5 3        → 3. egzersizi çalıştır (hata/yanlış sonuç görürsünüz)
        //   dotnet run -- 1.5 3 --hint → ipucu (hangi debugger özelliğini kullanmalısınız?)
        //   dotnet run -- 1.5 all      → hepsini sırayla çalıştır (yakalanamayan hatalar özetlenir)
        //   dotnet run -- 1.5 analyzers → nullable/analyzer uyarılı kod (AnalyzersNullable/) — uyarılar için: dotnet build
        //
        // Hedef: kodu satır satır OKUYARAK değil, DEBUGGER ile bulmak (F5 → breakpoint → inceleme).
        // IDE'de: J01 projesini başlangıç projesi yapın (Debug argümanları: 1.5 3), RunOne'a breakpoint koyun, F11 ile içeri girin.

        if (args is ["analyzers"])
        {
            // Uyarıları üretmek için tipler yeterli; çalıştırınca da kısa bir çıktı verir.
            // Not: GetUserName(2) çalışma zamanında null döndürür — derleyicinin CS8603 ile uyardığı tam olarak bu.
            var service = new UserService { Name = "demo" };
            Console.WriteLine($"1 → {service.GetUserName(1)}");
            Console.WriteLine($"2 → {service.GetUserName(2) ?? "(null döndü)"}");
            Console.WriteLine($"Style demo: IsAllowed(admin) = {new StyleViolations().IsAllowed("admin")}");
            return 0;
        }

        var exercises = Exercises.All;

        if (args.Length == 0)
        {
            Console.WriteLine("Debugger egzersizleri:\n");
            foreach (var e in exercises)
                Console.WriteLine($"  {e.Number}. {e.Title,-40} → {e.Skill}");
            Console.WriteLine("\nÇalıştırmak için:  dotnet run -- 1.5 <numara>   (ipucu için --hint ekleyin)  |  analyzers");
            return 0;
        }

        if (args[0] == "all")
        {
            foreach (var e in exercises)
                RunOne(e);
            return 0;
        }

        if (!int.TryParse(args[0], out var number) || exercises.FirstOrDefault(e => e.Number == number) is not { } selected)
        {
            Console.Error.WriteLine($"Geçersiz egzersiz: {args[0]}");
            return 1;
        }

        if (args.Contains("--hint"))
        {
            Console.WriteLine($"[{selected.Number}] {selected.Title}\nBeceri: {selected.Skill}\nİpucu: {selected.Hint}");
            return 0;
        }

        RunOne(selected);
        return 0;

        static void RunOne(Exercise e)
        {
            Console.WriteLine($"\n━━ {e.Number}. {e.Title} ━━");
            try
            {
                e.Run();
            }
            catch (Exception ex)
            {
                // Egzersiz çöktü. Debugger olmadan sadece mesajı görürsünüz; "first-chance exception" ayarıyla
                // istisna FIRLATILDIĞI satırda durmanız gerekir. (Exception Settings / Ctrl+Alt+E)
                Console.WriteLine($"  ✗ İSTİSNA: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"    (fırlatıldığı yer: {ex.StackTrace?.Split('\n').FirstOrDefault()?.Trim()})");
            }
        }
    }
}
