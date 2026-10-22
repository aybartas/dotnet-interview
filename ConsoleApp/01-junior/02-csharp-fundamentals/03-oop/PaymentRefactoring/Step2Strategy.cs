namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step2;

// ═════════════════════════════════════════════════════════════════════════════════════════
// STEP 2 — TAKE ONE CHANGING DIMENSION (fee calculation) OUT OF INHERITANCE → STRATEGY (composition).
// A small, safe step: PaymentMethod is still the base class, but the fee is now an IFeePolicy OBJECT.
//   ✓ The InstallmentCreditCardPayment class was DELETED: installments = a different IFeePolicy (an object is passed instead of inheriting).
//   ✗ The fraud check is still in a subclass → the "fraud × installments" copy persists. We'll extract it in the next step too.
// At the end of every step the same scenarios run (Section09) → the refactor is SAFE.
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
        var fee = Rounding.Round(feePolicy.Calculate(amount));     // ← the fee is now AN OBJECT (composition)
        var total = amount + fee;
        Charge(total);
        return new PaymentResult(Name, amount, fee, total);
    }

    protected virtual void Validate(decimal amount)
    {
        if (amount <= 0) throw new PaymentRejectedException("Amount must be positive");
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
        if (amount > 5000m) throw new PaymentRejectedException("Card limit exceeded");
    }
}

public sealed class FraudCheckedCreditCardPayment(IFeePolicy feePolicy) : CreditCardPayment(feePolicy)
{
    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > 3000m) throw new PaymentRejectedException("Suspected fraud");
    }
}

public sealed class BankTransferPayment(string iban) : PaymentMethod(new FlatFee(5m))
{
    protected override string Name => "BankTransfer";

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (iban is not { Length: 26 } || !iban.StartsWith("TR", StringComparison.Ordinal))
            throw new PaymentRejectedException("Invalid IBAN");
    }
}

public sealed class WalletPayment(decimal balance) : PaymentMethod(new NoFee())
{
    public decimal Balance { get; private set; } = balance;

    protected override string Name => "Wallet";

    protected override void Validate(decimal amount)
    {
        base.Validate(amount);
        if (amount > Balance) throw new PaymentRejectedException("Insufficient balance");
    }

    protected override void Charge(decimal total) => Balance -= total;
}

public sealed class Step2Scenarios : IPaymentScenarios
{
    public string StepName => "Step 2 — fee became a strategy";
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
