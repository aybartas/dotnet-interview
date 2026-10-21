using Junior.CSharpFundamentals.ProgramAnatomy.Consumer;

namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class ProgramAnatomyTopic
{
    public static int Run(string[] args)
    {
        // ═══════════════════════════════════════════════════════════════════════════════════════
        // J2.1 — Program Anatomisi. Bu dosyanın KENDİSİ "top-level statements" örneğidir:
        // class ve Main yazmadık; derleyici bir Program sınıfı ve <Main>$ metodu üretti (Section01 bunu gösterir).
        //
        //   dotnet run -- 2.1                      → bölüm listesi
        //   dotnet run -- 2.1 5                 → 5. bölümü çalıştır
        //   dotnet run -- 2.1 all               → hepsi
        //   dotnet run -- 2.1 exit-code 42      → process'i 42 çıkış koduyla bitirir (sonra:  echo $?)
        //
        // `args`: komut satırı argümanları (program adı DAHİL DEĞİL, C'den farklı olarak).
        // `return <int>`: process exit code'u. 0 = başarı, sıfırdan farklı = hata (CI/script'ler buna bakar).
        // `await` kullanırsanız derleyici Main'i `async Task<int> Main` yapar.
        // ═══════════════════════════════════════════════════════════════════════════════════════

        if (args is ["exit-code", var code] && int.TryParse(code, out var exitCode))
        {
            Console.WriteLine($"Exit code {exitCode} ile çıkılıyor. Kontrol: Linux/macOS `echo $?`, PowerShell `$LASTEXITCODE`, cmd `echo %ERRORLEVEL%`");
            return exitCode;                      // return, Main'in dönüş değeri = exit code
        }

        if (args is ["access-modifiers"])        // assembly sınırı deneyi (J02.Lib'deki Vault'a dışarıdan bakış)
        {
            AccessModifiersDemo.Run();
            return 0;
        }

        var sections = Sections.All;

        if (args.Length == 0)
        {
            Console.WriteLine("J2.1 Program Anatomisi — bölümler:\n");
            foreach (var s in sections) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 2.1 <numara>  |  all  |  exit-code <n>  |  access-modifiers");
            return 0;
        }

        foreach (var section in args[0] == "all" ? sections : sections.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }

        return 0;
    }
}
