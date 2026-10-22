namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section09_Lifecycle
{
    public static void Run()
    {
        Console.WriteLine("Nesne yaşam döngüsü: oluşturma (ctor) → kullanım → erişilemez hale gelme → GC toplar.");
        Console.WriteLine("GC YALNIZCA BELLEĞİ yönetir. Dosya tanıtıcısı, soket, DB bağlantısı gibi YÖNETİLMEYEN kaynakları BIRAKMAK sizin sorumluluğunuz → IDisposable.\n");

        Console.WriteLine("1) using bloğu: Dispose() çıkışta GARANTİ çağrılır (exception olsa bile → try/finally)");
        using (var r = new Resource("A"))
        {
            r.Use();
        }

        Console.WriteLine("\n2) using declaration (C# 8): kapsam sonunda Dispose; birden çok kaynak TERS sırada bırakılır");
        UseTwo();

        Console.WriteLine("\n3) Exception olsa da Dispose çağrılır:");
        try
        {
            using var r = new Resource("C");
            throw new InvalidOperationException("kullanım sırasında hata");
        }
        catch (InvalidOperationException ex) { Console.WriteLine($"   yakalandı: {ex.Message}"); }

        Console.WriteLine("\n4) using OLMADAN: Dispose çağrılmaz → kaynak GC zamanı belirsiz bırakılır (finalizer varsa; yoksa SIZINTI):");
        var leaked = new Resource("D");
        leaked.Use();
        Console.WriteLine("   (D'nin Dispose'u çağrılmadı — çıktıda 'bırakıldı' satırı YOK)");

        Console.WriteLine("\nDispose pattern'in tamamı (finalizer, SuppressFinalize, protected Dispose(bool), async dispose) → M1.5");
    }

    private static void UseTwo()
    {
        using var first = new Resource("B1");
        using var second = new Resource("B2");
        first.Use(); second.Use();
        Console.WriteLine("   metot sonuna gelindi...");
    }                                                                       // ← B2, sonra B1 Dispose edilir

    private sealed class Resource : IDisposable
    {
        private readonly string _name;
        public Resource(string name) { _name = name; Console.WriteLine($"   [{_name}] açıldı"); }
        public void Use() => Console.WriteLine($"   [{_name}] kullanılıyor");
        public void Dispose() => Console.WriteLine($"   [{_name}] bırakıldı (Dispose)");
    }
}
