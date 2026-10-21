namespace Junior.CollectionsLinq.DelegatesLambdasEvents.Tests;

public class DelegateAndClosureTests
{
    private delegate int BinaryOperation(int a, int b);
    private static int Add(int a, int b) => a + b;

    [Fact]
    public void Method_group_converts_to_a_delegate()
    {
        BinaryOperation op = Add;
        Func<int, int, int> func = Add;
        Assert.Equal(5, op(2, 3));
        Assert.Equal(5, func(2, 3));
        Assert.Equal(nameof(Add), op.Method.Name);
    }

    [Fact]
    public void Predicate_is_equivalent_to_Func_of_bool()
    {
        Predicate<int> p = n => n % 2 == 0;
        Func<int, bool> f = n => n % 2 == 0;
        Assert.Equal(new List<int> { 1, 2, 3, 4 }.FindAll(p), new List<int> { 1, 2, 3, 4 }.Where(f));
    }

    [Fact]
    public void Multicast_delegate_runs_all_subscribers_in_order_but_returns_only_the_last_value()
    {
        var log = new List<string>();
        Action<string> chain = s => log.Add("1:" + s);
        chain += s => log.Add("2:" + s);
        chain("x");
        Assert.Equal(["1:x", "2:x"], log);
        Assert.Equal(2, chain.GetInvocationList().Length);

        Func<int> values = () => 1;
        values += () => 2;
        values += () => 3;
        Assert.Equal(3, values());
    }

    [Fact]
    public void Delegates_are_immutable_combining_creates_a_new_instance()
    {
        Action a = () => { };
        var before = a;
        a += () => { };
        Assert.NotSame(before, a);
    }

    [Fact]
    public void Null_delegate_needs_null_conditional_invoke()
    {
        Action<string>? none = null;
        none?.Invoke("x");                                            // sorun yok
        Assert.Throws<NullReferenceException>(() => none!("x"));
    }

    [Fact]
    public void Removing_a_subscriber_requires_the_same_delegate_target_and_method()
    {
        var count = 0;
        Action handler = () => count++;
        Action? chain = handler;
        chain += () => count += 10;
        chain -= handler;
        chain?.Invoke();
        Assert.Equal(10, count);
    }

    // ───────── Closure ─────────

    [Fact]
    public void Lambda_reads_the_captured_variable_when_it_runs_not_when_it_was_created()
    {
        var message = "ilk";
        Func<string> read = () => message;
        message = "sonra";
        Assert.Equal("sonra", read());
    }

    [Fact]
    public void Lambda_can_modify_the_captured_variable()
    {
        var counter = 0;
        Action inc = () => counter++;
        inc(); inc();
        Assert.Equal(2, counter);
    }

    [Fact]
    public void For_loop_closure_trap_all_lambdas_share_one_variable()
    {
        var funcs = new List<Func<int>>();
        for (var i = 0; i < 3; i++) funcs.Add(() => i);
        Assert.Equal([3, 3, 3], funcs.Select(f => f()));
    }

    [Fact]
    public void Copying_the_loop_variable_fixes_the_trap()
    {
        var funcs = new List<Func<int>>();
        for (var i = 0; i < 3; i++) { var copy = i; funcs.Add(() => copy); }
        Assert.Equal([0, 1, 2], funcs.Select(f => f()));
    }

    [Fact]
    public void Foreach_variable_is_fresh_per_iteration_so_there_is_no_trap()
    {
        var funcs = new List<Func<int>>();
        foreach (var n in new[] { 10, 20, 30 }) funcs.Add(() => n);
        Assert.Equal([10, 20, 30], funcs.Select(f => f()));
    }

    [Fact]
    public void Capturing_lambda_has_a_compiler_generated_display_class_target_while_static_lambda_does_not_capture()
    {
        var captured = 5;
        Func<int> capturing = () => captured;
        Func<int, int> nonCapturing = static x => x * 2;

        Assert.Contains("DisplayClass", capturing.Target!.GetType().Name);
        Assert.DoesNotContain("DisplayClass", nonCapturing.Target?.GetType().Name ?? "");
    }

    [Fact]
    public void Expression_lambda_is_data_and_func_lambda_is_code()
    {
        System.Linq.Expressions.Expression<Func<int, bool>> expr = x => x > 5;
        Assert.Equal(System.Linq.Expressions.ExpressionType.GreaterThan, expr.Body.NodeType);
        Assert.True(expr.Compile()(7));
        Assert.False(expr.Compile()(3));
    }

    // ───────── Local function ─────────

    private static IEnumerable<int> BadRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        for (var i = 0; i < count; i++) yield return i;
    }

    private static IEnumerable<int> GoodRange(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        return Iterator();
        IEnumerable<int> Iterator() { for (var i = 0; i < count; i++) yield return i; }
    }

    [Fact]
    public void Iterator_method_defers_argument_validation_but_a_local_function_iterator_does_not()
    {
        var lazy = BadRange(-1);                                       // hata YOK (gövde ertelendi)
        Assert.Throws<ArgumentOutOfRangeException>(() => lazy.ToList());   // ilk MoveNext'te patlar
        Assert.Throws<ArgumentOutOfRangeException>(() => GoodRange(-1));   // anında
    }

    [Fact]
    public void Local_functions_can_recurse_and_be_called_before_their_declaration()
    {
        Assert.Equal(120, Fact(5));
        Assert.Equal(42, Twice(21));
        static int Fact(int n) => n <= 1 ? 1 : n * Fact(n - 1);
        int Twice(int x) => x * 2;
    }
}
