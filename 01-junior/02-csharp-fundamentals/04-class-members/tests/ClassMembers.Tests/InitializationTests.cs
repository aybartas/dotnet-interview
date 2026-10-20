using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers.Tests;

// Not: Trace static olduğundan bu sınıftaki testler paralel koşmasın diye aynı collection'a alınır.
[Collection("trace")]
public class InitializationTests
{
    private static List<string> StepsFor(Func<object> create)
    {
        _ = new DerivedClass();                 // ısınma: static başlatmaların tamamı bir kez çalışsın (testler arası bağımsızlık)
        Trace.Reset();
        _ = create();
        return [.. Trace.Steps.Select(s => s[(s.IndexOf('.') + 2)..])];   // sıra numarası önekini at
    }

    [Fact]
    public void Instance_order_is_field_initializers_derived_to_base_then_constructor_bodies_base_to_derived()
    {
        var steps = StepsFor(() => new DerivedClass());

        Assert.Equal(
        [
            "Derived instance field initializer",
            "Base  instance field initializer",
            "Base  constructor GÖVDESİ",
            "Derived constructor GÖVDESİ",
        ], steps);
    }

    [Fact]
    public void Static_initialization_does_not_run_again_for_the_second_instance()
    {
        var steps = StepsFor(() => new DerivedClass());
        Assert.DoesNotContain(steps, s => s.Contains("static"));
    }

    [Fact]
    public void Constructor_chaining_this_runs_the_other_constructor_first()
    {
        var steps = StepsFor(() => new DerivedClass("x"));

        Assert.Equal(
        [
            "Derived instance field initializer",
            "Base  instance field initializer",
            "Base  constructor GÖVDESİ",
            "Base  constructor(string) GÖVDESİ (x) — this() zinciri önce çalıştı",
            "Derived constructor(string) GÖVDESİ (x)",
        ], steps);
    }

    [Fact]
    public void Private_constructor_forces_creation_through_the_factory()
    {
        Assert.DoesNotContain(typeof(Connection).GetConstructors(), c => c.GetParameters().Length == 2);   // 2 parametreli ctor public değil
        var a = Connection.Create("db");
        var copy = new Connection(a);
        Assert.Equal("db", copy.Name);
        Assert.NotSame(a, copy);
        Assert.Same(Connection.Shared, Connection.Shared);
    }
}

[CollectionDefinition("trace", DisableParallelization = true)]
public class TraceCollection;
