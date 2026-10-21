namespace Junior.BuildRunBasics.BinObj;

public static class Section04_CopyAndClean
{
    public static void Run()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        Console.WriteLine($"1) appsettings.json bin/'de var mı? {File.Exists(path)}   (kaynakta: 02-bin-obj/appsettings.json)");
        if (File.Exists(path)) Console.WriteLine($"   içerik: {File.ReadAllText(path).ReplaceLineEndings(" ").Trim()}");

        Console.WriteLine("""

           csproj'ta:
             <None Include="02-bin-obj/appsettings.json" Link="appsettings.json" CopyToOutputDirectory="PreserveNewest" />

           CopyToOutputDirectory değerleri:
             Never           → kopyalanmaz (varsayılan)
             PreserveNewest  → kaynak daha yeniyse kopyalar (genellikle doğrusu)
             Always          → her derlemede kopyalar
           Web projelerinde appsettings.json bunu SDK sayesinde otomatik alır; kendi içerik dosyanız (şablon, sertifika, SQL betiği) için bunu yazarsınız.
           Dosya bin/'e gelmezse uygulama çalışırken "dosya bulunamadı" der — en sık nedeni bu satırın unutulmasıdır.

        2) bin/ ve obj/ neden .gitignore'da?
             • İkisi de kaynaktan ÜRETİLEBİLİR (build çıktısı) → depoya girerse gürültü, merge çakışması, makineye özel yollar, şişme.
             • Standart .NET .gitignore: bin/ obj/  (+ .vs/, *.user). Bu repoda da öyle.

        3) dotnet clean ne zaman gerekir?
             dotnet clean      → bin/<config>/ içindeki ve obj/'daki üretilmiş dosyaları siler (obj/project.assets.json'ı silmez)
             Gerekçeler: "bende derleniyor ama eski .dll çalışıyor" şüphesi, paket/SDK değişimi sonrası tuhaf hatalar, boyut temizliği.
             Kökten temizlik: bin/ ve obj/ klasörlerini elle silip yeniden `dotnet build` (restore dahil). Sorun çözmenin ilk sırası: bin/obj sil → yeniden derle.
        """);
    }
}
