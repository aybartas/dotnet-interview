namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step2;

// ═════════════════════════════════════════════════════════════════════════════════════════
// ADIM 2 — DEĞİŞEN BİR BOYUTU (komisyon hesabı) KALITIMDAN ÇIKAR → STRATEJİ (composition).
// Küçük, güvenli bir adım: PaymentMethod hâlâ taban sınıf, ama komisyon artık bir IFeePolicy NESNESİ.
//   ✓ InstallmentCreditCardPayment sınıfı SİLİNDİ: taksit = farklı bir IFeePolicy (kalıtım yerine nesne verilir).
//   ✗ Fraud kontrolü hâlâ alt sınıfta → "fraud × taksit" kopyası sürüyor. Bir sonraki adımda onu da çıkaracağız.
// Her adımın sonunda aynı davranış testleri çalışır → refactor GÜVENLİ.
// ═════════════════════════════════════════════════════════════════════════════════════════

public interface IFeePolicy { decimal Calculate(decimal amount); }

public sealed class PercentageFee(decimal rate) : IFeePolicy { public decimal Calculate(decimal amount) => amount * rate; }
public sealed class FlatFee(decimal fee) : IFeePolicy { public decimal Calculate(decimal amount) => fee; }
public sealed class NoFee : IFeePolicy { public decimal Calculate(decimal amount) => 0m; }

public static class InstallmentFee
{
    public static IFeePolicy For(int installments) => new PercentageFee(0.02m + 0.01m * (installments - 1));
}

public abstract class PaymentMethod(IFeePolicy feePolicy)
{
    protected abstract string Name { get; }

    public PaymentResult Pay(decimal amount)
    {
        Validate(amount);
        var fee = Rounding.Round(feePolicy.Calculate(amount));     // ← komisyon artık BİR NESNE (composition)
        var total = amount + fee;
        Charge(total);
        return new PaymentResult(Name, amount, fee, total);
    }

    protected virtual void Validate(decimal amount)
    {
        if (amount <= 0) throw new PaymentRejectedException("Tutar pozitif olmalı");
    }

    protected virtual void Charge(decimal total) { }
}

public class CreditCardPayment(IFeePolicy feePolicy) : PaymentMethod(feePolicy)
{
    public CreditCardPayment() : this(new PercentageFee(0.02m)) { }

    protected override string Name => "CreditCard";

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 5000m) throw new PaymentRejectedException("Kart limiti aşıldı");
    }
}

public sealed class FraudCheckedCreditCardPayment(IFeePolicy feePolicy) : CreditCardPayment(feePolicy)
{
    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 3000m) throw new PaymentRejectedException("Fraud şüphesi");
    }
}

public sealed class BankTransferPayment(string iban) : PaymentMethod(new FlatFee(5m))
{
    protected override string Name => "BankTransfer";

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (iban is not { Length: 26 } || !iban.StartsWith("TR", StringComparison.Ordinal))
            throw new PaymentRejectedException("Geçersiz IBAN");
    }
}

public sealed class WalletPayment(decimal balance) : PaymentMethod(new NoFee())
{
    public decimal Balance { get; private set; } = balance;

    protected override string Name => "Wallet";

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > Balance) throw new PaymentRejectedException("Yetersiz bakiye");
    }

    protected override void Charge(decimal total) => Balance -= total;
}

public sealed class Step2Scenarios : IPaymentScenarios
{
    public string StepName => "Adım 2 — komisyon strateji oldu";
    public PaymentResult PayCreditCard(decimal amount) => new CreditCardPayment().Pay(amount);
    public PaymentResult PayInstallmentCreditCard(decimal amount, int installments) => new CreditCardPayment(InstallmentFee.For(installments)).Pay(amount);
    public PaymentResult PayFraudCheckedCreditCard(decimal amount) => new FraudCheckedCreditCardPayment(new PercentageFee(0.02m)).Pay(amount);
    public PaymentResult PayFraudCheckedInstallmentCreditCard(decimal amount, int installments) => new FraudCheckedCreditCardPayment(InstallmentFee.For(installments)).Pay(amount);
    public PaymentResult PayBankTransfer(decimal amount, string iban) => new BankTransferPayment(iban).Pay(amount);

    public (PaymentResult Result, decimal BalanceAfter) PayWallet(decimal amount, decimal balance)
    {
        var wallet = new WalletPayment(balance);
        var result = wallet.Pay(amount);
        return (result, wallet.Balance);
    }
}
