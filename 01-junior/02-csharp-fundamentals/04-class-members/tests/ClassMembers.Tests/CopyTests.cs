using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

public class CopyTests
{
    private static OrderModel Create() => new()
    {
        Number = "A-1",
        ShippingAddress = new Address { City = "Ankara", Street = "Atatürk Cd." },
        Tags = ["acil"],
    };

    [Fact]
    public void Assignment_is_not_a_copy()
    {
        var o = Create();
        var alias = o;
        alias.Number = "X";
        Assert.Equal("X", o.Number);
    }

    [Fact]
    public void MemberwiseClone_is_shallow_so_changing_the_copy_corrupts_the_original()
    {
        var original = Create();
        var shallow = original.ShallowCopy();

        shallow.Number = "S-1";                              // string: bağımsız
        shallow.ShippingAddress.City = "İstanbul";           // iç nesne: PAYLAŞILIYOR
        shallow.Tags.Add("hediye");                          // iç liste: PAYLAŞILIYOR

        Assert.Equal("A-1", original.Number);
        Assert.Equal("İstanbul", original.ShippingAddress.City);          // orijinal BOZULDU
        Assert.Equal(["acil", "hediye"], original.Tags);                    // orijinal BOZULDU
        Assert.Same(original.ShippingAddress, shallow.ShippingAddress);
    }

    public static TheoryData<string, Func<OrderModel, OrderModel>> DeepCopiers => new()
    {
        { "copy constructor", o => new OrderModel(o) },
        { "DeepCopy()", o => o.DeepCopy() },
        { "JSON round-trip", o => o.DeepCopyViaJson() },
    };

    [Theory]
    [MemberData(nameof(DeepCopiers))]
    public void Deep_copy_leaves_the_original_untouched(string _, Func<OrderModel, OrderModel> copy)
    {
        var original = Create();
        var deep = copy(original);

        deep.ShippingAddress.City = "İzmir";
        deep.Tags.Add("hediye");
        deep.Number = "D-1";

        Assert.Equal("Ankara", original.ShippingAddress.City);
        Assert.Equal(["acil"], original.Tags);
        Assert.Equal("A-1", original.Number);
        Assert.NotSame(original.ShippingAddress, deep.ShippingAddress);
    }

    [Fact]
    public void Record_with_expression_is_also_shallow()
    {
        var rec = new OrderRecord("R-1", new Address { City = "Ankara" }, ["acil"]);
        var copy = rec with { Number = "R-2" };

        copy.ShippingAddress.City = "Bursa";
        copy.Tags.Add("hediye");

        Assert.Equal("R-1", rec.Number);                                    // değer alanı bağımsız
        Assert.Equal("Bursa", rec.ShippingAddress.City);                    // paylaşılan iç nesne bozuldu
        Assert.Equal(["acil", "hediye"], rec.Tags);
    }

    [Fact]
    public void Record_with_can_avoid_sharing_by_providing_a_new_inner_object()
    {
        var rec = new OrderRecord("R-1", new Address { City = "Ankara" }, ["acil"]);
        var copy = rec with { ShippingAddress = new Address { City = "Bursa" }, Tags = [.. rec.Tags] };

        Assert.Equal("Ankara", rec.ShippingAddress.City);
        Assert.NotSame(rec.Tags, copy.Tags);
    }

    [Fact]
    public void ICloneable_returns_object_and_the_contract_does_not_say_shallow_or_deep()
    {
        var original = new LegacyCloneable();
        var clone = (LegacyCloneable)original.Clone();                      // cast gerekli
        Assert.Same(original.Items, clone.Items);                           // gerçekte shallow — ama bunu sözleşmeden anlayamazsınız
    }
}
