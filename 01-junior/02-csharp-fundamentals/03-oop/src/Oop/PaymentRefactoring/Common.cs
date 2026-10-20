namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring;

/// <summary>Bir ödemenin sonucu. Üç refactor adımının hepsi AYNI sonucu üretmek zorundadır (davranış sözleşmesi).</summary>
public sealed record PaymentResult(string Method, decimal Amount, decimal Fee, decimal Total);

public sealed class PaymentRejectedException(string reason) : Exception(reason);

internal static class Rounding
{
    public static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}

/// <summary>
/// DAVRANIŞ SÖZLEŞMESİ. Her refactor adımı bu arayüzü uygular; testler yalnızca bunu bilir.
/// Refactor = dış davranışı değiştirmeden iç yapıyı değiştirmek → testler HER adımda yeşil kalmalı.
///
/// İş kuralları (tüm adımlarda aynı):
///   • Tutar pozitif olmalı.
///   • Kredi kartı: %2 komisyon; işlem limiti 5.000. Taksitli: %2 + her ek taksit için %1 (3 taksit = %4).
///     Fraud kontrolü açıksa 3.000'in üstü reddedilir.
///   • Havale: sabit 5 TL komisyon; IBAN "TR" ile başlayıp 26 karakter olmalı.
///   • Cüzdan: komisyon yok; bakiye yetersizse reddedilir; başarılı ödemede bakiye düşer.
/// </summary>
public interface IPaymentScenarios
{
    string StepName { get; }
    PaymentResult PayCreditCard(decimal amount);
    PaymentResult PayInstallmentCreditCard(decimal amount, int installments);
    PaymentResult PayFraudCheckedCreditCard(decimal amount);
    PaymentResult PayFraudCheckedInstallmentCreditCard(decimal amount, int installments);
    PaymentResult PayBankTransfer(decimal amount, string iban);
    (PaymentResult Result, decimal BalanceAfter) PayWallet(decimal amount, decimal balance);
}
