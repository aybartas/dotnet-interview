using System.Reflection;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics.Tests;

public class KeyContractTests
{
    private static bool Found<T>(Func<T> store, Func<T> find) where T : notnull
        => new Dictionary<T, string> { [store()] = "x" }.ContainsKey(find());

    [Fact]
    public void Consistent_Equals_and_GetHashCode_finds_the_key()
        => Assert.True(Found(() => new GoodKey(1), () => new GoodKey(1)));

    [Fact]
    public void Overriding_only_Equals_breaks_lookup()
        => Assert.False(Found(() => new EqualsOnlyKey(1), () => new EqualsOnlyKey(1)));

    [Fact]
    public void Overriding_only_GetHashCode_breaks_lookup()
        => Assert.False(Found(() => new HashOnlyKey(1), () => new HashOnlyKey(1)));

    private sealed class CountingKey(int id, bool constantHash) : IEquatable<CountingKey>
    {
        public static int EqualsCalls;
        public int Id { get; } = id;
        public bool Equals(CountingKey? other) { EqualsCalls++; return other is not null && other.Id == Id; }
        public override bool Equals(object? obj) => Equals(obj as CountingKey);
        public override int GetHashCode() => constantHash ? 1 : Id.GetHashCode();
    }

    [Fact]
    public void Constant_hash_degrades_lookups_from_constant_time_to_linear_scans()
    {
        const int n = 500;
        long CallsForAllLookups(bool constantHash)
        {
            var dict = Enumerable.Range(0, n).ToDictionary(i => new CountingKey(i, constantHash), i => i);
            var probes = Enumerable.Range(0, n).Select(i => new CountingKey(i, constantHash)).ToArray();
            CountingKey.EqualsCalls = 0;
            foreach (var p in probes) _ = dict[p];
            return CountingKey.EqualsCalls;
        }

        var good = CallsForAllLookups(constantHash: false);
        var bad = CallsForAllLookups(constantHash: true);

        Assert.True(good <= n * 2, $"iyi hash: ~{n} Equals çağrısı beklenir, gerçek {good}");
        Assert.True(bad > n * (n / 4), $"sabit hash: O(n²) Equals çağrısı beklenir, gerçek {bad}");    // ~n²/2
    }

    [Fact]
    public void Mutable_key_becomes_unreachable_after_its_hash_changes()
    {
        var key = new MutableKey("a");
        var map = new Dictionary<MutableKey, int> { [key] = 1 };
        key.Name = "b";

        Assert.False(map.ContainsKey(key));
        Assert.False(map.Remove(key));
        Assert.Single(map);
    }

    [Fact]
    public void String_keys_are_case_sensitive_unless_a_comparer_is_supplied()
    {
        Assert.False(new Dictionary<string, int> { ["Elma"] = 1 }.ContainsKey("ELMA"));
        Assert.True(new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Elma"] = 1 }.ContainsKey("ELMA"));
        Assert.True(new Dictionary<string, int>(new CaseInsensitiveComparer()) { ["Elma"] = 1 }.ContainsKey("eLMA"));
    }
}

public class GenericsTests
{
    [Fact]
    public void Repository_stores_retrieves_and_rejects_duplicates()
    {
        var repo = new InMemoryRepository<Product, int>();
        repo.Add(new Product(1, "Kalem", 12.5m));
        repo.Add(new Product(2, "Defter", 40m));

        Assert.Equal("Defter", repo.Get(2).Name);
        Assert.Null(repo.Find(99));
        Assert.Throws<KeyNotFoundException>(() => repo.Get(99));
        Assert.Throws<InvalidOperationException>(() => repo.Add(new Product(1, "Kopya", 1m)));
        Assert.Equal(["Defter"], repo.Where(p => p.Price > 20).Select(p => p.Name));
    }

    [Fact]
    public void Repository_works_with_a_different_entity_and_key_type_without_changes()
    {
        var repo = new InMemoryRepository<Customer, Guid>();
        var c = new Customer(Guid.NewGuid(), "Ayşe");
        repo.Add(c);
        Assert.Same(c, repo.Find(c.Id));
        Assert.True(repo.Remove(c.Id));
        Assert.Equal(0, repo.Count);
    }

    [Fact]
    public void GetAll_returns_a_snapshot_not_the_internal_store()
    {
        var repo = new InMemoryRepository<Product, int>();
        repo.Add(new Product(1, "a", 1m));
        var all = repo.GetAll();
        repo.Add(new Product(2, "b", 2m));
        Assert.Single(all);
    }

    [Fact]
    public void Update_requires_an_existing_entity()
    {
        var repo = new InMemoryRepository<Product, int>();
        Assert.Throws<KeyNotFoundException>(() => repo.Update(new Product(1, "x", 1m)));
        repo.Add(new Product(1, "x", 1m));
        repo.Update(new Product(1, "y", 2m));
        Assert.Equal("y", repo.Get(1).Name);
    }

    [Fact]
    public void Max_works_for_any_IComparable()
    {
        Assert.Equal(9, GenericUtils.Max([3, 9, 2]));
        Assert.Equal("z", GenericUtils.Max(["b", "z", "a"]));
        Assert.Equal(2.5m, GenericUtils.Max([1.5m, 2.5m]));
        Assert.Throws<InvalidOperationException>(() => GenericUtils.Max(Array.Empty<int>()));
    }

    [Fact]
    public void Struct_constraint_returns_nullable_and_new_constraint_creates_instances()
    {
        Assert.Null(GenericUtils.FirstOrNull(Array.Empty<int>()));
        Assert.Equal(5, GenericUtils.FirstOrNull([5, 6]));
        Assert.IsType<List<int>>(GenericUtils.CreateDefault<List<int>>());
    }

    [Fact]
    public void Constraints_are_recorded_as_generic_parameter_attributes()
    {
        GenericParameterAttributes Attrs(string method) => typeof(GenericUtils).GetMethod(method)!.GetGenericArguments()[0].GenericParameterAttributes;

        Assert.True(Attrs(nameof(GenericUtils.OrNull)).HasFlag(GenericParameterAttributes.ReferenceTypeConstraint));
        Assert.True(Attrs(nameof(GenericUtils.FirstOrNull)).HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint));
        Assert.True(Attrs(nameof(GenericUtils.CreateDefault)).HasFlag(GenericParameterAttributes.DefaultConstructorConstraint));
        Assert.Contains(typeof(GenericUtils).GetMethod(nameof(GenericUtils.Max))!.GetGenericArguments()[0].GetGenericParameterConstraints(),
            c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IComparable<>));
    }

    [Fact]
    public void Swap_exchanges_values()
    {
        int a = 1, b = 2;
        GenericUtils.Swap(ref a, ref b);
        Assert.Equal((2, 1), (a, b));
    }

    [Fact]
    public void CountBy_counts_occurrences()
        => Assert.Equal(new Dictionary<string, int> { ["a"] = 2, ["b"] = 1 }, GenericUtils.CountBy(["a", "b", "a"]));
}
