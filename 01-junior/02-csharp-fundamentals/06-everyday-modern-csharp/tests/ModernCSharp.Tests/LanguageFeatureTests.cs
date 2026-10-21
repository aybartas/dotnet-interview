using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Junior.CSharpFundamentals.ModernCSharp.Tests;

public class LanguageFeatureTests
{
    // ───────── records ─────────
    private record Person(string First, string Last, int Age);
    private sealed record Employee(string First, string Last, int Age, string Dept) : Person(First, Last, Age);
    private readonly record struct Point(int X, int Y);
    private sealed record Basket(string Name, List<int> Items);

    [Fact]
    public void Record_has_value_equality_with_and_deconstruction()
    {
        var a = new Person("A", "B", 1);
        var b = new Person("A", "B", 1);
        Assert.True(a == b);
        Assert.NotSame(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());

        var older = a with { Age = 2 };
        Assert.Equal(1, a.Age);
        Assert.Equal(2, older.Age);

        var (first, _, age) = a;
        Assert.Equal(("A", 1), (first, age));
        Assert.Equal("Person { First = A, Last = B, Age = 1 }", a.ToString());
    }

    [Fact]
    public void Derived_record_is_not_equal_to_base_record_with_same_values()
    {
        Person p = new Employee("A", "B", 1, "IT");
        Assert.False(p.Equals(new Person("A", "B", 1)));
    }

    [Fact]
    public void Record_struct_is_a_value_type_with_value_equality()
    {
        Assert.True(typeof(Point).IsValueType);
        Assert.Equal(new Point(1, 2), new Point(1, 2));
    }

    [Fact]
    public void Record_equality_compares_collections_by_reference()
    {
        Assert.NotEqual(new Basket("b", [1, 2]), new Basket("b", [1, 2]));        // listeler farklı nesne
        var shared = new List<int> { 1 };
        Assert.Equal(new Basket("b", shared), new Basket("b", shared));           // aynı liste nesnesi
    }

    // ───────── init / required ─────────
    private sealed class Settings { public required string Host { get; init; } public int Port { get; init; } = 5432; }

    [Fact]
    public void Required_and_init_members_are_recorded_in_metadata()
    {
        Assert.NotNull(typeof(Settings).GetProperty(nameof(Settings.Host))!.GetCustomAttribute<System.Runtime.CompilerServices.RequiredMemberAttribute>());
        Assert.Null(typeof(Settings).GetProperty(nameof(Settings.Port))!.GetCustomAttribute<System.Runtime.CompilerServices.RequiredMemberAttribute>());
        Assert.Equal(5432, new Settings { Host = "x" }.Port);
    }

    // ───────── pattern matching ─────────
    private sealed record Address(string City);
    private sealed record Customer(string Name, Address? Address);
    private sealed record Order(Customer Customer, decimal Total);

    private static string Classify(object? o) => o switch
    {
        null => "null",
        int n and > 0 => "pozitif",
        int n and < 0 => "negatif",
        int => "sıfır",
        string { Length: 0 } => "boş",
        string => "metin",
        Order { Total: > 1000m, Customer.Address.City: "Ankara" } => "büyük-ankara",
        Order => "sipariş",
        _ => "?",
    };

    [Theory]
    [InlineData(5, "pozitif")]
    [InlineData(-5, "negatif")]
    [InlineData(0, "sıfır")]
    [InlineData("", "boş")]
    [InlineData("x", "metin")]
    public void Relational_logical_and_property_patterns(object input, string expected) => Assert.Equal(expected, Classify(input));

    [Fact]
    public void Nested_property_pattern_and_null_handling()
    {
        Assert.Equal("null", Classify(null));
        Assert.Equal("büyük-ankara", Classify(new Order(new Customer("a", new Address("Ankara")), 1500m)));
        Assert.Equal("sipariş", Classify(new Order(new Customer("a", null), 1500m)));      // Address null → iç içe pattern eşleşmez, patlamaz
    }

    [Fact]
    public void List_patterns()
    {
        int[] arr = [1, 2, 3];
        Assert.True(arr is [1, .., 3]);
        Assert.False(arr is [_, _]);
        Assert.True(arr is [var first, ..] && first == 1);
    }

    [Theory]
    [InlineData(95, "A")]
    [InlineData(90, "A")]
    [InlineData(89, "B")]
    [InlineData(0, "F")]
    public void Switch_expression_with_relational_patterns(int score, string expected)
    {
        static string Grade(int s) => s switch { >= 90 => "A", >= 80 => "B", >= 70 => "C", >= 0 => "F", _ => throw new ArgumentOutOfRangeException(nameof(s)) };
        Assert.Equal(expected, Grade(score));
    }

    [Fact]
    public void Switch_expression_without_a_match_throws_at_runtime()
    {
#pragma warning disable CS8509 // BİLEREK kapsamlı olmayan switch (derleyici uyarısını gösterir)
        static int F(int x) => x switch { 1 => 10, 2 => 20 };
#pragma warning restore CS8509
        Assert.Throws<SwitchExpressionException>(() => F(3));
    }

    // ───────── strings ─────────
    [Fact]
    public void Raw_string_literals_need_no_escaping_and_strip_indentation()
    {
        var json = """
            { "path": "C:\temp", "q": "a \"b\"" }
            """;
        Assert.Equal("{ \"path\": \"C:\\temp\", \"q\": \"a \\\"b\\\"\" }", json);
    }

    [Fact]
    public void Dollar_dollar_raw_strings_keep_single_braces_literal()
    {
        var name = "Ayşe";
        var body = $$"""{ "user": "{{name}}" }""";
        Assert.Equal("{ \"user\": \"Ayşe\" }", body);
    }

    [Fact]
    public void Interpolation_supports_alignment_and_format_specifiers()
        => Assert.Equal("[   42]|[00FF]", FormattableString.Invariant($"[{42,5}]|[{255:X4}]"));

    // ───────── collection expressions ─────────
    [Fact]
    public void Collection_expressions_adapt_to_the_target_type_and_spread()
    {
        int[] array = [1, 2, 3];
        List<int> list = [1, 2, 3];
        ImmutableArray<int> immutable = [1, 2, 3];
        HashSet<int> set = [1, 2, 2, 3];
        IEnumerable<int> seq = [1, 2, 3];

        Assert.Equal(3, array.Length);
        Assert.Equal(3, list.Count);
        Assert.Equal(3, immutable.Length);
        Assert.Equal(3, set.Count);
        Assert.Equal(6, seq.Sum());

        int[] merged = [.. array, 4, .. list];
        Assert.Equal([1, 2, 3, 4, 1, 2, 3], merged);
        List<string> empty = [];
        Assert.Empty(empty);
    }

    // ───────── primary constructors ─────────
    private sealed class Greeter(string prefix) { public string Greet(string n) => $"{prefix}, {n}!"; }
    private sealed class Counter(int start) { public int Value => start; public void Inc() => start++; }

    [Fact]
    public void Class_primary_constructor_parameter_is_not_a_property_but_record_parameter_is()
    {
        Assert.Null(typeof(Greeter).GetProperty("prefix"));
        Assert.NotNull(typeof(Person).GetProperty("First"));
        Assert.Equal("Hi, Ali!", new Greeter("Hi").Greet("Ali"));
    }

    [Fact]
    public void Captured_primary_constructor_parameter_is_mutable_state()
    {
        var c = new Counter(10);
        c.Inc(); c.Inc();
        Assert.Equal(12, c.Value);
    }

    // ───────── C# 14 ─────────
    private sealed class Product
    {
        public string Name { get; set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value)); } = "";
        public decimal Price { get; set => field = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value)); }
    }

    private sealed class Holder { public string Name { get; set; } = ""; public int Count { get; set; } }

    [Fact]
    public void Field_keyword_validates_without_a_declared_backing_field()
    {
        var p = new Product { Name = "  Kalem  ", Price = 5m };
        Assert.Equal("Kalem", p.Name);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.Price = -1m);
        Assert.Throws<ArgumentNullException>(() => p.Name = null!);
        Assert.Contains(typeof(Product).GetFields(BindingFlags.Instance | BindingFlags.NonPublic), f => f.Name == "<Name>k__BackingField");
    }

    [Fact]
    public void Null_conditional_assignment_skips_both_assignment_and_right_side_when_null()
    {
        Holder? none = null;
        var evaluated = false;
        string Rhs() { evaluated = true; return "x"; }

        none?.Name = Rhs();
        Assert.False(evaluated);                                  // sağ taraf DEĞERLENDİRİLMEDİ

        var some = new Holder();
        some?.Name = Rhs();
        some?.Count += 5;
        Assert.True(evaluated);
        Assert.Equal("x", some?.Name);
        Assert.Equal(5, some?.Count);
    }

    // ───────── küçük şekerler ─────────
    [Fact]
    public void Target_typed_new_and_lambda_default_parameters()
    {
        Dictionary<string, List<int>> map = new() { ["a"] = [1, 2] };
        Assert.Equal(2, map["a"].Count);

        var add = (int x, int y = 10) => x + y;
        Assert.Equal(11, add(1));
        Assert.Equal(3, add(1, 2));
    }

    private sealed class Res(List<string> log, string name) : IDisposable { public void Dispose() => log.Add(name); }

    [Fact]
    public void Using_declarations_dispose_in_reverse_order_at_scope_end()
    {
        var log = new List<string>();
        void Scope()
        {
            using var a = new Res(log, "A");
            using var b = new Res(log, "B");
            log.Add("body");
        }
        Scope();
        Assert.Equal(["body", "B", "A"], log);
    }
}
