using Junior.DevFoundations.IdeDebugging.Exercises;

// J1.5 — Debugger alıştırmaları. Her egzersiz BİLEREK hatalıdır.
//
//   dotnet run                 → egzersiz listesi
//   dotnet run -- 3            → 3. egzersizi çalıştır (hata/yanlış sonuç görürsünüz)
//   dotnet run -- 3 --hint     → ipucu (hangi debugger özelliğini kullanmalısınız?)
//   dotnet run -- all          → hepsini sırayla çalıştır (yakalanamayan hatalar özetlenir)
//
// Hedef: kodu satır satır OKUYARAK değil, DEBUGGER ile bulmak (F5 → breakpoint → inceleme).
// IDE'de: bu projeyi başlangıç projesi yapın, Program.cs'te "Exercises.Run" çağrısına breakpoint koyun, F11 ile içeri girin.

var exercises = Exercises.All;

if (args.Length == 0)
{
    Console.WriteLine("Debugger egzersizleri:\n");
    foreach (var e in exercises)
        Console.WriteLine($"  {e.Number}. {e.Title,-40} → {e.Skill}");
    Console.WriteLine("\nÇalıştırmak için:  dotnet run -- <numara>   (ipucu için --hint ekleyin)");
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
