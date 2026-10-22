namespace Junior.CSharpFundamentals.Oop.PaymentRefactoring;

/// <summary>The result of a payment. All three refactor steps must produce the SAME result (behavior contract).</summary>
public sealed record PaymentResult(string Method, decimal Amount, decimal Fee, decimal Total);

public sealed class PaymentRejectedException(string reason) : Exception(reason);

internal static class Rounding
{
    public static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}

/// <summary>
/// BEHAVIOR CONTRACT. Every refactor step implements this interface; the scenarios know only this.
/// Refactoring = changing the internal structure without changing the external behavior → the scenarios must give the same result at EVERY step.
///
/// Business rules (the same in all steps):
///   • The amount must be positive.
///   • Credit card: 2% fee; transaction limit 5,000. Installments: 2% + 1% for each extra installment (3 installments = 4%).
///     If the fraud check is on, anything above 3,000 is rejected.
///   • Bank transfer: flat fee of 5; the IBAN must start with "TR" and be 26 characters long.
///   • Wallet: no fee; rejected if the balance is insufficient; the balance is deducted on a successful payment.
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
