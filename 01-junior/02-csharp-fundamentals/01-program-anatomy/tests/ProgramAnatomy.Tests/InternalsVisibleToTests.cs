using Junior.CSharpFundamentals.ProgramAnatomy.Library;

namespace Junior.CSharpFundamentals.ProgramAnatomy.Tests;

public class InternalsVisibleToTests
{
    // PriceRules 'internal static class'tır. Normalde başka assembly'den GÖRÜNMEZ; ama Library.csproj'daki
    // <InternalsVisibleTo Include="ProgramAnatomy.Tests" /> bu test assembly'sine kapıyı açar.
    [Fact]
    public void Internal_type_is_reachable_from_friend_assembly()
        => Assert.Equal(120.00m, PriceRules.AddVat(100m));

    [Fact]
    public void Internal_member_is_reachable_from_friend_assembly()
        => Assert.Equal("internal", new Vault().InternalMember);

    [Fact]
    public void Internal_type_is_not_public_for_everybody_else()
    {
        var type = typeof(Vault).Assembly.GetType("Junior.CSharpFundamentals.ProgramAnatomy.Library.PriceRules");
        Assert.NotNull(type);
        Assert.False(type.IsPublic);
    }

    [Fact]
    public void Default_access_is_internal_for_types_and_private_for_members()
        => Assert.Contains("tip varsayılanı: internal, üye varsayılanı: private", Factory.DescribeDefaults());
}
