namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step3;

// ═════════════════════════════════════════════════════════════════════════════════════════
// ADIM 3 — TAM COMPOSITION. Kalıtım hiyerarşisi KALMADI.
//   Payment (tek, sealed sınıf) = IFeePolicy + IPaymentRule listesi + IPaymentSource
//   • Doğrulamalar küçük, bağımsız KURALLAR (rule) → istenen kombinasyon listeye eklenerek kurulur
//   • "Fraud + taksit" artık YENİ SINIF DEĞİL: aynı parçaları farklı birleştirmek (kopya yok)
//   • Yeni boyut (3D Secure, kampanya) = yeni bir kural/politika SINIFI; mevcut sınıflara dokunulmaz (Open/Closed)
//   • Her parça tek başına test edilebilir ve değiştirilebilir (düşük coupling, yüksek cohesion)
// ═════════════════════════════════════════════════════════════════════════════════════════

public interface IFeePolicy { decimal Calculate(decimal amount); }
public interface IPaymentRule { void Check(decimal amount); }
public interface IPaymentSource { void Charge(decimal total); }

public sealed class PercentageFee(decimal rate) : IFeePolicy { public decimal Calculate(decimal amount) => amount * rate; }
public sealed class FlatFee(decimal fee) : IFeePolicy { public decimal Calculate(decimal amount) => fee; }
public sealed class NoFee : IFeePolicy { public decimal Calculate(decimal amount) => 0m; }

public sealed class PositiveAmountRule : IPaymentRule
{
    public void Check(decimal amount) { if (amount <= 0) throw new PaymentRejectedException("Tutar pozitif olmalı"); }
}
public sealed class MaxAmountRule(decimal limit, string reason) : IPaymentRule
{
    public void Check(decimal amount) { if (amount > limit) throw new PaymentRejectedException(reason); }
}
public sealed class IbanRule(string iban) : IPaymentRule
{
    public void Check(decimal amount)
    {
        if (iban is not { Length: 26 } || !iban.StartsWith("TR", StringComparison.Ordinal))
            throw new PaymentRejectedException("Geçersiz IBAN");
    }
}
public sealed class SufficientBalanceRule(WalletAccount wallet) : IPaymentRule
{
    public void Check(decimal amount) { if (amount > wallet.Balance) throw new PaymentRejectedException("Yetersiz bakiye"); }
}

public sealed class NoSource : IPaymentSource { public void Charge(decimal total) { } }
public sealed class WalletAccount(decimal balance) : IPaymentSource
{
    public decimal Balance { get; private set; } = balance;
    public void Charge(decimal total) => Balance -= total;
}

public sealed class Payment(string method, IFeePolicy fee, IReadOnlyList<IPaymentRule> rules, IPaymentSource source)
{
    public PaymentResult Pay(decimal amount)
    {
        foreach (var rule in rules) rule.Check(amount);
        var feeAmount = Rounding.Round(fee.Calculate(amount));
        var total = amount + feeAmount;
        source.Charge(total);
        return new PaymentResult(method, amount, feeAmount, total);
    }
}

/// <summary>Parçaları birleştiren fabrika: "hangi ödeme yöntemi" artık KONFİGÜRASYON, sınıf hiyerarşisi değil.</summary>
public static class PaymentFactory
{
    public static Payment CreditCard(int installments = 1, bool fraudCheck = false)
    {
        List<IPaymentRule> rules = [new PositiveAmountRule(), new MaxAmountRule(5000m, "Kart limiti aşıldı")];
        if (fraudCheck) rules.Add(new MaxAmountRule(3000m, "Fraud şüphesi"));
        return new Payment("CreditCard", new PercentageFee(0.02m + 0.01m * (installments - 1)), rules, new NoSource());
    }

    public static Payment BankTransfer(string iban)
        => new("BankTransfer", new FlatFee(5m), [new PositiveAmountRule(), new IbanRule(iban)], new NoSource());

    public static Payment Wallet(WalletAccount wallet)
        => new("Wallet", new NoFee(), [new PositiveAmountRule(), new SufficientBalanceRule(wallet)], wallet);
}

public sealed class Step3Scenarios : IPaymentScenarios
{
    public string StepName => "Adım 3 — tam composition";
    public PaymentResult PayCreditCard(decimal amount) => PaymentFactory.CreditCard().Pay(amount);
    public PaymentResult PayInstallmentCreditCard(decimal amount, int installments) => PaymentFactory.CreditCard(installments).Pay(amount);
    public PaymentResult PayFraudCheckedCreditCard(decimal amount) => PaymentFactory.CreditCard(fraudCheck: true).Pay(amount);
    public PaymentResult PayFraudCheckedInstallmentCreditCard(decimal amount, int installments) => PaymentFactory.CreditCard(installments, fraudCheck: true).Pay(amount);
    public PaymentResult PayBankTransfer(decimal amount, string iban) => PaymentFactory.BankTransfer(iban).Pay(amount);

    public (PaymentResult Result, decimal BalanceAfter) PayWallet(decimal amount, decimal balance)
    {
        var wallet = new WalletAccount(balance);
        var result = PaymentFactory.Wallet(wallet).Pay(amount);
        return (result, wallet.Balance);
    }
}
