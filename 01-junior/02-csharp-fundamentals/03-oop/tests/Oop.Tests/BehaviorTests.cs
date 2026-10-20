namespace Junior.CSharpFundamentals.Oop.Tests;

public class BehaviorTests
{
    private class Base
    {
        public virtual string Hello() => "Base";
        public string Plain() => "Base.Plain";
    }
    private sealed class ViaOverride : Base { public override string Hello() => "Override"; }
    private sealed class ViaNew : Base { public new string Hello() => "New"; public new string Plain() => "New.Plain"; }

    [Fact]
    public void Override_dispatches_on_the_runtime_type()
    {
        Base b = new ViaOverride();
        Assert.Equal("Override", b.Hello());
    }

    [Fact]
    public void New_hides_and_dispatches_on_the_declared_type()
    {
        Base b = new ViaNew();
        Assert.Equal("Base", b.Hello());                    // base referansı → gizlenen metot GÖRÜNMEZ
        Assert.Equal("New", ((ViaNew)b).Hello());           // türeyen referansı → yeni metot
        Assert.Equal("Base.Plain", b.Plain());
    }

    private interface IBird { string Move(); }
    private interface IRobot { string Move(); }
    private sealed class RobotDuck : IBird, IRobot
    {
        string IBird.Move() => "wings";
        string IRobot.Move() => "motor";
    }

    [Fact]
    public void Explicit_interface_implementation_resolves_same_signature_conflict()
    {
        var duck = new RobotDuck();
        Assert.Equal("wings", ((IBird)duck).Move());
        Assert.Equal("motor", ((IRobot)duck).Move());
    }

    private abstract class Shape { protected Shape() => Created = true; public bool Created { get; } public abstract double Area(); }
    private sealed class Sq(double s) : Shape { public override double Area() => s * s; }

    [Fact]
    public void Abstract_class_cannot_be_instantiated_but_its_constructor_runs_for_derived()
    {
        Assert.True(typeof(Shape).IsAbstract);
        Assert.True(new Sq(2).Created);
    }

    [Fact]
    public void Struct_cannot_be_a_base_type_and_derives_from_ValueType()
    {
        Assert.True(typeof(TimeSpan).IsSealed);
        Assert.Equal(typeof(ValueType), typeof(TimeSpan).BaseType);
    }

    private interface ILogger { void Log(string m); void Warn(string m) => Log("W:" + m); }
    private sealed class Spy : ILogger { public List<string> Lines { get; } = []; public void Log(string m) => Lines.Add(m); }

    [Fact]
    public void Interface_default_implementation_is_available_via_the_interface_reference()
    {
        var spy = new Spy();
        ((ILogger)spy).Warn("x");
        Assert.Equal(["W:x"], spy.Lines);
    }

    private sealed class HappyHour(TimeProvider time) { public bool IsActive() => time.GetUtcNow().Hour is >= 17 and < 19; }
    private sealed class FixedTime(DateTimeOffset now) : TimeProvider { public override DateTimeOffset GetUtcNow() => now; }

    [Theory]
    [InlineData(17, true)]
    [InlineData(18, true)]
    [InlineData(19, false)]
    [InlineData(9, false)]
    public void Injecting_time_makes_time_dependent_code_deterministic(int hour, bool expected)
        => Assert.Equal(expected, new HappyHour(new FixedTime(new DateTimeOffset(2026, 1, 1, hour, 0, 0, TimeSpan.Zero))).IsActive());
}
