using Junior.CSharpFundamentals.Oop.PaymentRefactoring;

namespace Junior.CSharpFundamentals.Oop;

public static class Section09_PaymentRefactoring
{
    public static void Run()
    {
        Console.WriteLine("Same business rules, three different internal structures. The same scenarios give the same result at EVERY step.\n");

        IPaymentScenarios[] steps = [new PaymentRefactoring.Step1.Step1Scenarios(), new PaymentRefactoring.Step2.Step2Scenarios(), new PaymentRefactoring.Step3.Step3Scenarios()];

        foreach (var step in steps)
        {
            Console.WriteLine($"── {step.StepName} ──");
            Show("   credit card 1000          ", () => step.PayCreditCard(1000m));
            Show("   3 installments 1000       ", () => step.PayInstallmentCreditCard(1000m, 3));
            Show("   fraud-checked 4000        ", () => step.PayFraudCheckedCreditCard(4000m));
            Show("   fraud + 3 inst. 2000      ", () => step.PayFraudCheckedInstallmentCreditCard(2000m, 3));
            Show("   transfer 1000 (valid IBAN)", () => step.PayBankTransfer(1000m, "TR" + new string('0', 24)));
            Show("   transfer (invalid IBAN)   ", () => step.PayBankTransfer(1000m, "XX123"));
            Show("   wallet 100 (balance 150)  ", () => step.PayWallet(100m, 150m).Result);
            Show("   wallet 200 (balance 150)  ", () => step.PayWallet(200m, 150m).Result);
            Console.WriteLine();
        }

        Console.WriteLine("""
        What changed? (Behavior is the SAME, structure differs)
          Step 1: 3 base classes + 3 "new requirement" classes; fraud logic COPIED in two places; no multiple inheritance → can't be combined.
          Step 2: the fee became an IFeePolicy object → the installment class was deleted. Fraud is still in a subclass.
          Step 3: a single Payment class + small policy/rule classes. "Method" = configuration. New requirement = new small class (without touching existing code).

        Composition over inheritance — WHY?
          • Fragile base class: a change in the base class silently affects all subclasses (Validate order, protected behavior contract).
          • Deep hierarchy maintenance cost: understanding the behavior means reading the whole chain.
          • Single inheritance limit: you can't combine two "dimensions" (installments × fraud) via inheritance at the same time.
          • Inheritance is fixed at COMPILE-TIME; composition can be changed at RUNTIME (swapping a strategy).
          • Testing: the parts are tested separately; with inheritance you must also set up the base class.
        Inheritance is still the right tool: a real "is-a" + Liskov compliance (the Exception hierarchy, Stream types, UI controls).
        """);
    }

    private static void Show(string label, Func<PaymentResult> pay)
    {
        try
        {
            var r = pay();
            Console.WriteLine($"{label} → {r.Method}: amount {r.Amount:N2}, fee {r.Fee:N2}, total {r.Total:N2}");
        }
        catch (PaymentRejectedException ex)
        {
            Console.WriteLine($"{label} → REJECTED: {ex.Message}");
        }
    }
}
