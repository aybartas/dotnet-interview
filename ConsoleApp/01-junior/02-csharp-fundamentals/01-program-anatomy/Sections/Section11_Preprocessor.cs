#define DEMO_SYMBOL                  // Bu dosya için derleme sembolü tanımlar (using'lerden ÖNCE olmalı; dosya kapsamlıdır)

#nullable enable
namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section11_Preprocessor
{
    public static void Run()
    {
        // #if / #elif / #else / #endif: KOŞULLU DERLEME — seçilmeyen kod derlemeye hiç girmez (Release'de IL'e yazılmaz)
#if DEBUG
        Console.WriteLine("Derleme: DEBUG yapılandırması (dotnet run). Release için:  dotnet run -c Release");
#else
        Console.WriteLine("Derleme: RELEASE yapılandırması");
#endif

#if DEMO_SYMBOL
        Console.WriteLine("DEMO_SYMBOL bu dosyada #define edildi.");
#endif

#if NET10_0_OR_GREATER
        Console.WriteLine("Hedef framework: net10.0 veya üstü  (NETxx_0_OR_GREATER sembolleri SDK tarafından otomatik tanımlanır; çoklu hedefte işe yarar)");
#endif

        // #region / #endregion: SADECE editörde katlanabilir bölge. Derlemeye etkisi YOK. (Büyük #region'lar çoğu zaman sınıfın bölünmesi gerektiğini gösteren kod kokusudur.)
        #region Katlanabilir bölge
        Console.WriteLine("#region sadece IDE'de katlanır.");
        #endregion

        // #nullable: nullable analizini dosya/bölge bazında aç-kapat
#nullable disable
        string legacy = null;                             // uyarı YOK (nullable bağlam kapalı: eski kodla birlikte çalışma)
#nullable restore
        Console.WriteLine($"#nullable disable bölgesinde 'string legacy = null' uyarı üretmedi → legacy is null: {legacy is null}");

        // #pragma warning: belirli uyarıyı bölgesel sustur (NEDENİNİ yazın!)
#pragma warning disable CS0219                           // atanmış ama hiç kullanılmamış değişken uyarısı
        var unused = 42;
#pragma warning restore CS0219
        Console.WriteLine("#pragma warning disable/restore ile CS0219 yalnızca iki satır arasında susturuldu.");

        // #error / #warning derleme sırasında kendi hata/uyarınızı üretir (ör. geçersiz sembol kombinasyonu):
        // #error Bu yapılandırma desteklenmiyor
    }
}
