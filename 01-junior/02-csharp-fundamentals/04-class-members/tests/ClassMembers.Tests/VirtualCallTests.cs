using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

public class VirtualCallTests
{
    [Fact]
    public void Virtual_call_in_base_constructor_sees_a_half_constructed_derived_object()
    {
        var derived = new DerivedWithState();

        // Taban ctor sırasında: initializer ile atanan alan DOLU, ctor GÖVDESİNDE atananlar varsayılan (null / 0)
        Assert.Equal("initializer: başlatıcıdan, ctor: null, count: 0", derived.Seen[0]);
        // Ctor bittikten sonra: nesne tamamen kurulu
        Assert.Equal("initializer: başlatıcıdan, ctor: ctor gövdesinden, count: 42", derived.Describe());
    }

    [Fact]
    public void Calling_virtual_after_construction_is_safe()
    {
        var safe = SafeBase.Create<SafeDerived>();
        Assert.Equal("SafeDerived görüyor: hazır", safe.Result);
    }
}
