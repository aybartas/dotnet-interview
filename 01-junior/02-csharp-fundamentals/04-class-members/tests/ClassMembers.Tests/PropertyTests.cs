using System.Reflection;
using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

public class PropertyTests
{
    [Fact]
    public void Public_field_model_accepts_invalid_state()
    {
        var p = new ProductBefore { Name = "Kalem", Price = -5, Stock = -100 };
        Assert.True(p.Price < 0 && p.Stock < 0);               // invariant korunmuyor
    }

    [Fact]
    public void Property_model_rejects_negative_price()
    {
        var p = new ProductAfter { Name = "Kalem", Price = 10m };
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => p.Price = -1m);
        Assert.Equal("value", ex.ParamName);
        Assert.Equal(10m, p.Price);                            // başarısız set eski değeri BOZMAZ
    }

    [Fact]
    public void Stock_can_only_change_through_methods_that_protect_the_invariant()
    {
        var p = new ProductAfter { Name = "Kalem" };
        Assert.False(p.InStock);

        p.AddStock(3);
        Assert.True(p.InStock);

        Assert.Throws<InvalidOperationException>(() => p.Remove(10));
        Assert.Equal(3, p.Stock);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.AddStock(0));

        var setter = typeof(ProductAfter).GetProperty(nameof(ProductAfter.Stock))!.SetMethod!;
        Assert.True(setter.IsPrivate);                         // dışarıdan atanamaz
    }

    [Fact]
    public void Init_only_property_is_marked_with_IsExternalInit_modifier()
    {
        var setter = typeof(ProductAfter).GetProperty(nameof(ProductAfter.Name))!.SetMethod!;
        var modifiers = setter.ReturnParameter.GetRequiredCustomModifiers();
        Assert.Contains(modifiers, m => m.FullName == "System.Runtime.CompilerServices.IsExternalInit");
    }

    [Fact]
    public void Required_member_is_recorded_in_metadata()
    {
        var prop = typeof(ProductAfter).GetProperty(nameof(ProductAfter.Name))!;
        Assert.NotNull(prop.GetCustomAttribute<System.Runtime.CompilerServices.RequiredMemberAttribute>());
    }

    [Fact]
    public void Changing_field_to_property_changes_the_member_kind_which_is_a_binary_breaking_change()
    {
        Assert.NotNull(typeof(ProductBefore).GetField("Name"));          // IL: ldfld
        Assert.Null(typeof(ProductAfter).GetField("Name"));              // artık alan yok → eski derlemeler MissingFieldException alır
        Assert.NotNull(typeof(ProductAfter).GetProperty("Name"));        // IL: callvirt get_Name
    }

    [Fact]
    public void Static_property_counts_across_instances()
    {
        var before = ProductAfter.CreatedCount;
        _ = new ProductAfter { Name = "a" };
        _ = new ProductAfter { Name = "b" };
        Assert.Equal(before + 2, ProductAfter.CreatedCount);
    }
}
