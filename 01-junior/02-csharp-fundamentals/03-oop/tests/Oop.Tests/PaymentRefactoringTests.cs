using Junior.CSharpFundamentals.Oop.PaymentRefactoring;
using S1 = Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step1;
using S2 = Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step2;
using Junior.CSharpFundamentals.Oop.PaymentRefactoring.Step3;   // Adım 3 türleri doğrudan kullanılır

namespace Junior.CSharpFundamentals.Oop.Tests;

// ═══════════════════════════════════════════════════════════════════════════════════════
// Refactor'un güvence ağı: AYNI testler, üç adımın üçüne karşı koşar.
// Abstract taban test sınıfı davranış sözleşmesini yazar; her adım için ince bir alt sınıf gerçek uygulamayı verir.
// Adım 1 → 2 → 3 geçişinde bu testlerin YEŞİL kalması, iç yapının değiştiği ama dış davranışın korunduğunun kanıtıdır.
// ═══════════════════════════════════════════════════════════════════════════════════════
public abstract class PaymentBehaviorContract
{
    protected abstract IPaymentScenarios Sut { get; }

    private static readonly string ValidIban = "TR" + new string('0', 24);

    [Fact]
    public void Credit_card_charges_two_percent_fee()
        => Assert.Equal(new PaymentResult("CreditCard", 1000m, 20m, 1020m), Sut.PayCreditCard(1000m));

    [Fact]
    public void Credit_card_rejects_amounts_over_the_limit()
        => Assert.Equal("Kart limiti aşıldı", Assert.Throws<PaymentRejectedException>(() => Sut.PayCreditCard(5000.01m)).Message);

    [Fact]
    public void Credit_card_rejects_non_positive_amounts()
        => Assert.Throws<PaymentRejectedException>(() => Sut.PayCreditCard(0m));

    [Theory]
    [InlineData(1, 1000, 20)]     // %2
    [InlineData(2, 1000, 30)]     // %3
    [InlineData(3, 1000, 40)]     // %4
    [InlineData(6, 1000, 70)]     // %7
    public void Installments_add_one_percent_per_extra_installment(int installments, decimal amount, decimal expectedFee)
    {
        var result = Sut.PayInstallmentCreditCard(amount, installments);
        Assert.Equal(expectedFee, result.Fee);
        Assert.Equal(amount + expectedFee, result.Total);
    }

    [Fact]
    public void Fraud_check_rejects_amounts_over_3000_but_normal_card_accepts_them()
    {
        Assert.Equal("Fraud şüphesi", Assert.Throws<PaymentRejectedException>(() => Sut.PayFraudCheckedCreditCard(4000m)).Message);
        Assert.Equal(4000m, Sut.PayCreditCard(4000m).Amount);
        Assert.Equal(3000m, Sut.PayFraudCheckedCreditCard(3000m).Amount);          // sınır değer: 3000 geçer
    }

    [Fact]
    public void Fraud_check_combined_with_installments_applies_both_rules()
    {
        var ok = Sut.PayFraudCheckedInstallmentCreditCard(2000m, 3);
        Assert.Equal(80m, ok.Fee);                                                 // %4
        Assert.Throws<PaymentRejectedException>(() => Sut.PayFraudCheckedInstallmentCreditCard(3500m, 3));
    }

    [Fact]
    public void Bank_transfer_charges_flat_fee()
        => Assert.Equal(new PaymentResult("BankTransfer", 1000m, 5m, 1005m), Sut.PayBankTransfer(1000m, ValidIban));

    [Theory]
    [InlineData("")]
    [InlineData("TR123")]
    [InlineData("XX0000000000000000000000000")]
    public void Bank_transfer_rejects_invalid_iban(string iban)
        => Assert.Equal("Geçersiz IBAN", Assert.Throws<PaymentRejectedException>(() => Sut.PayBankTransfer(100m, iban)).Message);

    [Fact]
    public void Wallet_has_no_fee_and_reduces_balance()
    {
        var (result, balanceAfter) = Sut.PayWallet(100m, 150m);
        Assert.Equal(new PaymentResult("Wallet", 100m, 0m, 100m), result);
        Assert.Equal(50m, balanceAfter);
    }

    [Fact]
    public void Wallet_rejects_when_balance_is_insufficient()
        => Assert.Equal("Yetersiz bakiye", Assert.Throws<PaymentRejectedException>(() => Sut.PayWallet(200m, 150m)).Message);
}

public sealed class Step1_Inheritance_Tests : PaymentBehaviorContract { protected override IPaymentScenarios Sut { get; } = new S1.Step1Scenarios(); }
public sealed class Step2_StrategyFee_Tests : PaymentBehaviorContract { protected override IPaymentScenarios Sut { get; } = new S2.Step2Scenarios(); }
public sealed class Step3_Composition_Tests : PaymentBehaviorContract { protected override IPaymentScenarios Sut { get; } = new Step3Scenarios(); }

/// <summary>Adım 3'ün asıl kazancı: parçalar tek başına test edilebilir ve yeni gereksinim yeni küçük sınıftır.</summary>
public class Step3_ComponentTests
{
    [Fact]
    public void Single_rule_can_be_tested_in_isolation()
    {
        var rule = new MaxAmountRule(3000m, "Fraud şüphesi");
        rule.Check(3000m);
        Assert.Throws<PaymentRejectedException>(() => rule.Check(3000.01m));
    }

    [Fact]
    public void New_requirement_is_a_new_small_class_without_touching_existing_ones()
    {
        // "Kampanya: komisyon yarı yarıya" → mevcut hiçbir sınıfı DEĞİŞTİRMEDEN yeni bir IFeePolicy
        var discounted = new HalfFee(new PercentageFee(0.04m));
        var payment = new Payment("CreditCard", discounted, [new PositiveAmountRule()], new NoSource());
        Assert.Equal(20m, payment.Pay(1000m).Fee);
    }

    private sealed class HalfFee(IFeePolicy inner) : IFeePolicy { public decimal Calculate(decimal amount) => inner.Calculate(amount) / 2; }
}
