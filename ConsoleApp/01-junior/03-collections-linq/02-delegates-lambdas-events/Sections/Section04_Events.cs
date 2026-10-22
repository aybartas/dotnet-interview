using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section04_Events
{
    public static void Run()
    {
        Console.WriteLine("A) KÖTÜ: çıplak public delegate alanı (BadOrderService.OnCreated)");
        var bad = new BadOrderService();
        bad.OnCreated += o => Console.WriteLine($"   [e-posta servisi] sipariş {o.Id} → müşteriye e-posta");
        bad.OnCreated += o => Console.WriteLine($"   [stok servisi]    sipariş {o.Id} → stok düş");
        bad.OnCreated = o => Console.WriteLine("   [kötü niyetli/hatalı kod] tüm aboneleri EZDİ");   // atama: öncekileri SİLER
        Console.WriteLine("   Create çağrılıyor — e-posta ve stok servisleri BEKLENİR, ama:");
        bad.Create("Ayşe", 100m);
        Console.WriteLine("   Dışarıdan sahte olay tetikleniyor:");
        bad.OnCreated?.Invoke(new Order(999, "SAHTE", 1m));                                         // dışarıdan sahte olay üretimi
        Console.WriteLine("   → Aboneler sessizce silindi ve dışarıdan olay tetiklenebildi. Yayıncı kendi olayına bile sahip değil.\n");

        Console.WriteLine("B) DOĞRU: `event` anahtar sözcüğü + EventHandler<TEventArgs> (OrderService.OrderCreated)");
        var service = new OrderService();
        service.OrderCreated += (sender, e) => Console.WriteLine($"   [e-posta servisi] sipariş {e.Order.Id} ({e.Order.Customer}) → e-posta");
        service.OrderCreated += OnStock;
        // service.OrderCreated = null;                              // ✗ CS0070: event'e dışarıdan atama yok
        // service.OrderCreated(service, null!);                     // ✗ CS0070: dışarıdan tetikleme yok
        Console.WriteLine($"   abone sayısı: {service.SubscriberCount}");
        service.Create("Ayşe", 250m);

        service.OrderCreated -= OnStock;                            // abonelikten çıkma
        Console.WriteLine($"   OnStock aboneliği kaldırıldı → abone sayısı: {service.SubscriberCount}");
        service.Create("Ali", 80m);

        Console.WriteLine("""

        `event` ne sağlar?  Delegate üzerine ENCAPSULATION: dışarıya yalnızca += / -= (abone ol / ayrıl) açılır; tetikleme ve temizleme yalnızca yayıncıda kalır.
        Bu, "yayıncı–abone" (publish/subscribe, Observer) desenidir: yayıncı abonelerin kim olduğunu bilmez → gevşek bağlılık (→ J2.3).

        Standart desen:
          public event EventHandler<OrderCreatedEventArgs>? OrderCreated;     // EventHandler<TEventArgs>: (object? sender, TEventArgs e)
          protected virtual void OnOrderCreated(OrderCreatedEventArgs e) => OrderCreated?.Invoke(this, e);
          EventArgs'tan türeyen sınıf olayın VERİSİNİ taşır (sender = yayıncı).
        Dikkat:
          • Olay tetiklenirken abonelerden biri exception fırlatırsa, kalan aboneler ÇALIŞMAZ (zincir kesilir). Dayanıklılık için GetInvocationList() ile tek tek çağırıp yakalayın.
          • Aboneler yayıncının thread'inde, SENKRON çalışır (yavaş abone yayıncıyı bloklar).
          • `?.Invoke` null kontrolü + çağrıyı atomik yapar (abone tam çağrı anında ayrılsa NullReference olmaz).
          • `event` ile async iş akışı için async olay desenleri/mesaj kuyrukları gerekir (→ J4, M12); süreçler arası olay = mesajlaşma (RabbitMQ/Kafka).
        """);
    }

    private static void OnStock(object? sender, OrderCreatedEventArgs e) =>
        Console.WriteLine($"   [stok servisi]    sipariş {e.Order.Id} → stok düş");
}
