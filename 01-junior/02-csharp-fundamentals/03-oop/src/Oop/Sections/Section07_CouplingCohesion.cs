namespace Junior.CSharpFundamentals.Oop;

public static class Section07_CouplingCohesion
{
    public static void Run()
    {
        Console.WriteLine("COUPLING (bağlılık): bir sınıfın başkasını ne kadar BİLDİĞİ. Hedef: gevşek bağlılık.");
        Console.WriteLine("COHESION (uyum)    : bir sınıfın içindekilerin ne kadar AYNI AMACA hizmet ettiği. Hedef: yüksek uyum.\n");

        Console.WriteLine("1) SIKI bağlı: OrderService içinde 'new SmtpSender()' — somut tipe bağlı");
        var tight = new TightOrderService();
        tight.Place("kitap");                                 // test ederken GERÇEK e-posta gönderilir (yan etki, yavaş, kırılgan)

        Console.WriteLine("\n2) GEVŞEK bağlı: OrderService yalnızca INotifier soyutlamasını bilir (bağımlılık DIŞARIDAN verilir)");
        var fake = new FakeNotifier();
        var loose = new LooseOrderService(fake);
        loose.Place("kitap");
        Console.WriteLine($"   test double yakaladı: [{string.Join(" | ", fake.Messages)}]  (gerçek e-posta yok)");
        new LooseOrderService(new SmsNotifier()).Place("kalem");   // gönderim kanalı değişti; OrderService'e DOKUNULMADI

        Console.WriteLine("""

        Bağlılığı ölçmek: "Bu sınıfı değiştirince kaç yer etkileniyor?" ve "Bu sınıfı test etmek için kaç somut şey ayağa kalkmalı?"
          • SmtpSender'ın imzası değişince: Tight → OrderService'in kodu değişir (+ onu kullanan her yer). Loose → yalnızca SmtpSender ve DI kaydı.
        Uyumu ölçmek: "Sınıfın kaç farklı DEĞİŞME NEDENİ var?" (SRP). OrderManager'ın e-posta + PDF + vergi + DB'yi birlikte yapması = düşük uyum:
          vergi kuralı da e-posta şablonu da aynı dosyayı değiştirir → birbirinden bağımsız değişiklikler çakışır.
        'new' ile somut tipe bağlanmanın maliyeti: test edilemezlik, değiştirememe, gizli bağımlılık. Çözüm: interface + constructor injection (→ J6.2 DI).
        """);
    }

    private interface INotifier { void Notify(string message); }
    private sealed class SmtpSender { public void Send(string to, string body) => Console.WriteLine($"   [SMTP] gerçek e-posta gönderildi → {to}: {body}"); }
    private sealed class SmsNotifier : INotifier { public void Notify(string message) => Console.WriteLine($"   [SMS] {message}"); }
    private sealed class FakeNotifier : INotifier { public List<string> Messages { get; } = []; public void Notify(string message) => Messages.Add(message); }

    private sealed class TightOrderService
    {
        private readonly SmtpSender _sender = new();                   // ← somut tipe, KENDİ içinde 'new'
        public void Place(string product) => _sender.Send("musteri@example.com", $"{product} siparişi alındı");
    }

    private sealed class LooseOrderService(INotifier notifier)         // ← soyutlama, dışarıdan verilir
    {
        public void Place(string product) => notifier.Notify($"{product} siparişi alındı");
    }
}
