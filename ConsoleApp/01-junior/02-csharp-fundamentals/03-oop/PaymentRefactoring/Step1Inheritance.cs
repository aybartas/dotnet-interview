namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step1;

// ═════════════════════════════════════════════════════════════════════════════════════════
// ADIM 1 — SADECE KALITIM.
// İlk bakışta zarif: PaymentMethod tabanı, üç alt sınıf. Sorun, YENİ GEREKSİNİMLER geldikçe başlar:
//   "taksit" → InstallmentCreditCardPayment : CreditCardPayment
//   "fraud kontrolü" → FraudCheckedCreditCardPayment : CreditCardPayment
//   "taksit + fraud" → ??? → FraudCheckedInstallmentCreditCardPayment — fraud mantığı KOPYALANDI
// Her yeni "boyut" (taksit × fraud × 3D Secure × ...) sınıf sayısını ÇARPAR (class explosion).
// Taban sınıftaki değişiklik (örn. Validate sırası) tüm alt sınıfları sessizce etkiler → FRAGILE BASE CLASS.
// ═════════════════════════════════════════════════════════════════════════════════════════

public abstract class PaymentMethod
{
    protected abstract string Name { get; }
    protected abstract decimal CalculateFee(decimal amount);

    public PaymentResult Pay(decimal amount)
    {
        Validate(amount);
        var fee = Rounding.Round(CalculateFee(amount));
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

public class CreditCardPayment : PaymentMethod
{
    protected override string Name => "CreditCard";
    protected override decimal CalculateFee(decimal amount) => amount * 0.02m;

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 5000m) throw new PaymentRejectedException("Kart limiti aşıldı");
    }
}

public class BankTransferPayment(string iban) : PaymentMethod
{
    protected override string Name => "BankTransfer";
    protected override decimal CalculateFee(decimal amount) => 5m;

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (iban is not { Length: 26 } || !iban.StartsWith("TR", StringComparison.Ordinal))
            throw new PaymentRejectedException("Geçersiz IBAN");
    }
}

public class WalletPayment(decimal balance) : PaymentMethod
{
    public decimal Balance { get; private set; } = balance;

    protected override string Name => "Wallet";
    protected override decimal CalculateFee(decimal amount) => 0m;

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > Balance) throw new PaymentRejectedException("Yetersiz bakiye");
    }

    protected override void Charge(decimal total) => Balance -= total;
}

// ── Yeni gereksinimler = yeni alt sınıflar ──

public class InstallmentCreditCardPayment(int installments) : CreditCardPayment
{
    protected override decimal CalculateFee(decimal amount) => amount * (0.02m + 0.01m * (installments - 1));
}

public class FraudCheckedCreditCardPayment : CreditCardPayment
{
    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 3000m) throw new PaymentRejectedException("Fraud şüphesi");
    }
}

/// <summary>Taksit + fraud: C#'ta ÇOKLU KALITIM YOK → ikisinin mantığı birleştirilemez, biri KOPYALANIR.</summary>
public class FraudCheckedInstallmentCreditCardPayment(int installments) : InstallmentCreditCardPayment(installments)
{
    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 3000m) throw new PaymentRejectedException("Fraud şüphesi");   // ← FraudCheckedCreditCardPayment'tan KOPYA
    }
}

public sealed class Step1Scenarios : IPaymentScenarios
{
    public string StepName => "Adım 1 — kalıtım";
    public PaymentResult PayCreditCard(decimal amount) => new CreditCardPayment().Pay(amount);
    public PaymentResult PayInstallmentCreditCard(decimal amount, int installments) => new InstallmentCreditCardPayment(installments).Pay(amount);
    public PaymentResult PayFraudCheckedCreditCard(decimal amount) => new FraudCheckedCreditCardPayment().Pay(amount);
    public PaymentResult PayFraudCheckedInstallmentCreditCard(decimal amount, int installments) => new FraudCheckedInstallmentCreditCardPayment(installments).Pay(amount);
    public PaymentResult PayBankTransfer(decimal amount, string iban) => new BankTransferPayment(iban).Pay(amount);

    public (PaymentResult Result, decimal BalanceAfter) PayWallet(decimal amount, decimal balance)
    {
        var wallet = new WalletPayment(balance);
        var result = wallet.Pay(amount);
        return (result, wallet.Balance);
    }
}
