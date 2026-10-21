namespace Junior.BuildRunBasics.BigPicture;

public static class Section05_CompileVsRuntime
{
    public static void Run()
    {
        Console.WriteLine("DERLEME ZAMANI HATALARI (Roslyn): kod derlenmez, .dll ÜRETİLMEZ, program hiç çalışmaz. Örnekler (yorum satırı — açarsanız build kırılır):");
        Console.WriteLine("""
              // int x = "merhaba";            → error CS0029: 'string' türü örtük olarak 'int' türüne dönüştürülemez
              // Console.WriteLIne("a");        → error CS0117: 'Console' tanımında 'WriteLIne' yok          (yazım hatası)
              // int y;  Console.WriteLine(y);  → error CS0165: Atanmamış yerel değişken kullanıldı
              // string s = null;               → warning CS8600 (nullable açıkken): null'ı non-nullable'a atama (derlenir, uyarır)
              // return eksik / noktalı virgül  → error CS1002, CS0161 ...
        """);

        Console.WriteLine("ÇALIŞMA ZAMANI HATALARI (exception): kod DERLENİR, .dll üretilir; hata o satır çalıştığında fırlar. Örnekler:\n");
        Try("InvalidCastException  ", () => { object o = "metin"; _ = (int)o; });
        Try("IndexOutOfRange       ", () => { int[] a = [1, 2, 3]; var i = 5; _ = a[i]; });
        Try("NullReference         ", () => { string? s = Environment.GetEnvironmentVariable("YOK_BOYLE_BIR_DEGISKEN"); _ = s!.Length; });
        Try("DivideByZero          ", () => { var zero = int.Parse("0"); _ = 10 / zero; });
        Try("FormatException       ", () => _ = int.Parse("abc"));
        Try("DirectoryNotFound    ", () => File.ReadAllText("/yok/boyle/dosya.txt"));

        Console.WriteLine("""

        Fark:
          • Ne zaman?    : compile-time → derleyici kodu analiz ederken · runtime → program çalışırken, o kod yolu işlenince
          • Sonuç        : compile-time → .dll yok, hiçbir şey çalışmaz · runtime → .dll var, hata kullanıcıya/log'a yansır
          • Maliyet      : compile-time → ucuz, geliştirici hemen görür · runtime → pahalı: üretimde, belki yalnızca belirli veride
          • Kim yakalar? : compile-time → derleyici, analyzer, nullable analizi · runtime → test, log, exception handling
        Strateji: hatayı mümkün olduğunca ERKEN (derleme zamanına) çek — tipler, nullable reference types (<Nullable>enable</Nullable>),
        analyzer'lar, `required`, enum/record ile geçersiz durumları ifade edilemez kılmak. Kalanını testle yakala (→ J10).
        """);
    }

    private static void Try(string name, Action action)
    {
        try { action(); }
        catch (Exception ex) { Console.WriteLine($"   {name}: {ex.GetType().Name}: {ex.Message}"); }
    }
}
