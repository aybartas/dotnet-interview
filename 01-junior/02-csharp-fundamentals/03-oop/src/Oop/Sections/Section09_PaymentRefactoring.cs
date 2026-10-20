using Junior.CSharpFundamentals.Oop.PaymentRefactoring;

namespace Junior.CSharpFundamentals.Oop;

public static class Section09_PaymentRefactoring
{
    public static void Run()
    {
        Console.WriteLine("Aynı iş kuralları, üç farklı iç yapı. Testler (tests/Oop.Tests) HER adımda yeşil kalır.\n");

        IPaymentScenarios[] steps = [new PaymentRefactoring.Step1.Step1Scenarios(), new PaymentRefactoring.Step2.Step2Scenarios(), new PaymentRefactoring.Step3.Step3Scenarios()];

        foreach (var step in steps)
        {
            Console.WriteLine($"── {step.StepName} ──");
            Show("   kredi kartı 1000          ", () => step.PayCreditCard(1000m));
            Show("   3 taksit 1000             ", () => step.PayInstallmentCreditCard(1000m, 3));
            Show("   fraud kontrollü 4000      ", () => step.PayFraudCheckedCreditCard(4000m));
            Show("   fraud + 3 taksit 2000     ", () => step.PayFraudCheckedInstallmentCreditCard(2000m, 3));
            Show("   havale 1000 (geçerli IBAN)", () => step.PayBankTransfer(1000m, "TR" + new string('0', 24)));
            Show("   havale (geçersiz IBAN)    ", () => step.PayBankTransfer(1000m, "XX123"));
            Show("   cüzdan 100 (bakiye 150)   ", () => step.PayWallet(100m, 150m).Result);
            Show("   cüzdan 200 (bakiye 150)   ", () => step.PayWallet(200m, 150m).Result);
            Console.WriteLine();
        }

        Console.WriteLine("""
        Ne değişti? (Davranış AYNI, yapı farklı)
          Adım 1: 3 temel + 3 "yeni gereksinim" sınıfı; fraud mantığı iki yerde KOPYA; çoklu kalıtım yok → birleştirilemiyor.
          Adım 2: komisyon IFeePolicy nesnesi oldu → taksit sınıfı silindi. Fraud hâlâ alt sınıfta.
          Adım 3: tek Payment sınıfı + küçük politika/kural sınıfları. "Yöntem" = konfigürasyon. Yeni gereksinim = yeni küçük sınıf (mevcut koda dokunmadan).

        Composition over inheritance — NEDEN?
          • Fragile base class: taban sınıftaki değişiklik tüm alt sınıfları sessizce etkiler (Validate sırası, protected davranış sözleşmesi).
          • Derin hiyerarşi bakım maliyeti: davranışı anlamak için zincirin tamamını okumak gerekir.
          • Tek kalıtım kısıtı: iki "boyutu" (taksit × fraud) aynı anda kalıtımla birleştiremezsiniz.
          • Kalıtım COMPILE-TIME'da sabit; composition ÇALIŞMA ZAMANINDA değiştirilebilir (strateji değişimi).
          • Test: parçalar ayrı ayrı test edilir; kalıtımda taban sınıfı da kurmak zorundasınız.
        Kalıtım yine de doğru araç: gerçek "is-a" + Liskov uyumu (Exception hiyerarşisi, Stream türleri, UI kontrolleri).
        """);
    }

    private static void Show(string label, Func<PaymentResult> pay)
    {
        try
        {
            var r = pay();
            Console.WriteLine($"{label} → {r.Method}: tutar {r.Amount:N2}, komisyon {r.Fee:N2}, toplam {r.Total:N2}");
        }
        catch (PaymentRejectedException ex)
        {
            Console.WriteLine($"{label} → REDDEDİLDİ: {ex.Message}");
        }
    }
}
