namespace Junior.CSharpFundamentals.ModernCSharp;

public static class Section02_InitRequired
{
    public static void Run()
    {
        // init-only: yalnızca NESNE OLUŞTURULURKEN (object initializer / ctor / with) atanabilir, sonra salt okunur
        var settings = new ConnectionSettings { Host = "localhost", Port = 5432 };
        // settings.Port = 1;                                   // ✗ CS8852: init-only property yalnızca nesne başlatıcıda atanabilir
        var changed = new ConnectionSettings { Host = settings.Host, Port = 6000 };
        Console.WriteLine($"init-only      : {settings.Host}:{settings.Port} → yeni örnek {changed.Host}:{changed.Port}");

        // required: nesne oluştururken bu üyeyi ATAMAK ZORUNLU (yoksa DERLEME hatası); 'yarım' nesne oluşmaz
        // var bad = new ConnectionSettings { Port = 1 };       // ✗ CS9035: required 'Host' atanmadı
        Console.WriteLine($"required       : Host atlanırsa derleme hatası (CS9035); Port'un varsayılanı var → atlanabilir: {new ConnectionSettings { Host = "x" }.Port}");

        // required + init birlikte: doğrulanmış, değişmez, eksiksiz nesne için en pratik desen (constructor yazmadan)
        var order = new OrderDto { Id = "ORD-1", Total = 10m };
        Console.WriteLine($"DTO            : {order.Id} / {order.Total}");

        // with ifadesi record'larda init property'leri 'değiştirir' (aslında kopya üretir)
        Console.WriteLine($"required + record: {new Contact { Email = "a@x.com" } with { Email = "b@x.com" }}");

        Console.WriteLine("""

        Neden? Eskiden: ya parametreli ctor (parametre sayısı patlar) ya set; (nesne sonradan değişebilir, yarım kalır).
        init → "kurarken ata, sonra değişmez"; required → "atamayı unutamazsın". İkisi birleşince ctor'suz immutable DTO yazılır.
        Serileştiriciler (System.Text.Json) init-only ve required'ı destekler; EF Core'da entity'ler genelde set; kullanır.
        Constructor içinde `[SetsRequiredMembers]` ile required kontrolü atlanabilir (dikkatli kullanın).
        """);
    }

    private sealed class ConnectionSettings
    {
        public required string Host { get; init; }
        public int Port { get; init; } = 5432;
    }

    private sealed class OrderDto
    {
        public required string Id { get; init; }
        public required decimal Total { get; init; }
    }

    private sealed record Contact
    {
        public required string Email { get; init; }
    }
}
