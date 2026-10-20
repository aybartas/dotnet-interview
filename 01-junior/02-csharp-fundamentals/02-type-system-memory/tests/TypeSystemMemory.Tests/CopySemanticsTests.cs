namespace Junior.CSharpFundamentals.TypeSystemMemory.Tests;

public class CopySemanticsTests
{
    private struct PointS { public int X; }
    private sealed class PointC { public int X; }
    private struct Counter { public int Value; public void Increment() => Value++; }

    [Fact]
    public void Assigning_a_struct_copies_the_value()
    {
        var a = new PointS { X = 1 };
        var b = a;
        b.X = 99;
        Assert.Equal(1, a.X);
    }

    [Fact]
    public void Assigning_a_class_copies_the_reference()
    {
        var a = new PointC { X = 1 };
        var b = a;
        b.X = 99;
        Assert.Equal(99, a.X);
        Assert.Same(a, b);
    }

    [Fact]
    public void Reassigning_a_reference_parameter_does_not_affect_the_caller()
    {
        var p = new PointC { X = 1 };
        static void Reassign(PointC c) => c = new PointC { X = -1 };
        Reassign(p);
        Assert.Equal(1, p.X);
    }

    [Fact]
    public void List_indexer_returns_a_copy_of_a_struct_so_the_original_is_unchanged()
    {
        var list = new List<Counter> { new() };
        var copy = list[0];
        copy.Increment();
        Assert.Equal(0, list[0].Value);

        list[0] = copy;                        // kopyala-değiştir-geri ata
        Assert.Equal(1, list[0].Value);
    }

    [Fact]
    public void Array_elements_can_be_mutated_in_place()
    {
        var array = new[] { new Counter() };
        array[0].Increment();
        Assert.Equal(1, array[0].Value);
    }

    private sealed class Holder { public readonly Counter C = new(); public void Bump() => C.Increment(); }

    [Fact]
    public void Mutating_a_readonly_struct_field_works_on_a_defensive_copy()
    {
        var h = new Holder();
        h.Bump();
        Assert.Equal(0, h.C.Value);
    }

    [Fact]
    public void Record_has_value_equality_and_class_does_not()
    {
        Assert.Equal(new Rec("a", 1), new Rec("a", 1));
        Assert.NotEqual(new Cls("a", 1), new Cls("a", 1));
    }

    private sealed record Rec(string Name, int Age);
    private sealed class Cls(string name, int age) { public string Name { get; } = name; public int Age { get; } = age; }
}
