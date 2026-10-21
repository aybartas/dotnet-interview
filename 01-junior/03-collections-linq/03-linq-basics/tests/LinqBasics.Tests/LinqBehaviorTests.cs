using System.Linq.Expressions;

namespace Junior.CollectionsLinq.LinqBasics.Tests;

public class LinqBehaviorTests
{
    // ───────── Ertelenmiş çalışma (deferred execution) ─────────

    [Fact]
    public void Query_is_not_executed_until_it_is_enumerated()
    {
        var calls = 0;
        var query = new[] { 1, 2, 3 }.Where(n => { calls++; return n > 1; });
        Assert.Equal(0, calls);                       // tanımlandı ama çalışmadı

        _ = query.ToList();
        Assert.Equal(3, calls);
    }

    [Fact]
    public void Deferred_query_sees_items_added_after_it_was_defined()
    {
        var list = new List<int> { 1, 2, 3 };
        var evens = list.Where(n => n % 2 == 0);       // henüz çalışmadı
        list.Add(4);
        Assert.Equal([2, 4], evens);                    // 4'ü de görür
    }

    [Fact]
    public void ToList_takes_a_snapshot_and_ignores_later_changes()
    {
        var list = new List<int> { 1, 2, 3 };
        var snapshot = list.Where(n => n % 2 == 0).ToList();
        list.Add(4);
        Assert.Equal([2], snapshot);
    }

    [Fact]
    public void Each_enumeration_of_a_deferred_query_runs_it_again()
    {
        var calls = 0;
        var query = Enumerable.Range(1, 3).Select(n => { calls++; return n; });
        _ = query.Count();
        _ = query.Sum();
        Assert.Equal(6, calls);                         // 2 kez × 3 eleman
    }

    [Fact]
    public void Query_captures_variables_by_reference_so_the_threshold_used_is_the_one_at_enumeration_time()
    {
        var threshold = 2;
        var query = new[] { 1, 2, 3, 4 }.Where(n => n > threshold);
        threshold = 3;
        Assert.Equal([4], query);
    }

    [Fact]
    public void Select_with_side_effect_does_nothing_without_enumeration()
    {
        var log = new List<int>();
        var lazy = new[] { 1, 2, 3 }.Select(n => { log.Add(n); return n; });
        Assert.Empty(log);
        lazy.ToList();
        Assert.Equal([1, 2, 3], log);
    }

    [Fact]
    public void Streaming_operators_pull_one_element_at_a_time_and_Take_stops_early()
    {
        var produced = new List<int>();
        IEnumerable<int> Source() { for (var i = 1; i <= 100; i++) { produced.Add(i); yield return i; } }

        var first3 = Source().Where(n => n % 2 == 1).Take(3).ToList();
        Assert.Equal([1, 3, 5], first3);
        Assert.Equal([1, 2, 3, 4, 5], produced);        // 6..100 hiç üretilmedi
    }

    [Fact]
    public void OrderBy_is_buffering_it_consumes_the_whole_source_before_returning_the_first_item()
    {
        var produced = 0;
        IEnumerable<int> Source() { foreach (var n in new[] { 3, 1, 2 }) { produced++; yield return n; } }

        var first = Source().OrderBy(n => n).First();
        Assert.Equal(1, first);
        Assert.Equal(3, produced);
    }

    [Fact]
    public void OrderBy_is_stable_equal_keys_keep_their_original_order()
    {
        string[] words = ["bb", "a", "cc", "d", "ee"];
        Assert.Equal(["a", "d", "bb", "cc", "ee"], words.OrderBy(w => w.Length));
    }

    // ───────── First / Single ─────────

    [Fact]
    public void First_throws_on_empty_FirstOrDefault_returns_default()
    {
        int[] empty = [];
        Assert.Throws<InvalidOperationException>(() => empty.First());
        Assert.Equal(0, empty.FirstOrDefault());
        Assert.Equal(-1, empty.FirstOrDefault(-1));      // .NET 6+ özel varsayılan
    }

    [Fact]
    public void Single_throws_when_there_are_zero_or_more_than_one_matches()
    {
        Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().Single());
        Assert.Throws<InvalidOperationException>(() => new[] { 1, 2 }.Single());
        Assert.Equal(7, new[] { 7 }.Single());
    }

    [Fact]
    public void SingleOrDefault_returns_default_for_zero_but_still_throws_for_more_than_one()
    {
        Assert.Equal(0, Array.Empty<int>().SingleOrDefault());
        Assert.Throws<InvalidOperationException>(() => new[] { 1, 2 }.SingleOrDefault());
    }

    [Fact]
    public void First_stops_at_the_first_match_while_Single_must_check_the_whole_sequence()
    {
        var visited = 0;
        var data = Enumerable.Range(1, 10).Select(n => { visited++; return n; });

        _ = data.First(n => n > 2);
        Assert.Equal(3, visited);

        visited = 0;
        Assert.Throws<InvalidOperationException>(() => data.Single(n => n > 2));
        Assert.Equal(4, visited);                         // ikinci eşleşmeyi görünce durur
    }

    [Fact]
    public void FirstOrDefault_on_reference_types_returns_null_so_the_result_must_be_checked()
    {
        string[] names = ["Ali", "Ayşe"];
        Assert.Null(names.FirstOrDefault(n => n.StartsWith('Z')));
    }

    // ───────── Any / Count / çoklu enumerasyon ─────────

    [Fact]
    public void Any_short_circuits_while_Count_walks_the_whole_sequence()
    {
        var visited = 0;
        IEnumerable<int> data = Numbers();
        IEnumerable<int> Numbers() { for (var i = 1; i <= 1000; i++) { visited++; yield return i; } }

        Assert.True(data.Any());
        Assert.Equal(1, visited);

        visited = 0;
        Assert.Equal(1000, data.Count());
        Assert.Equal(1000, visited);
    }

    [Fact]
    public void Count_on_ICollection_is_O1_and_does_not_enumerate()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.True(list.TryGetNonEnumeratedCount(out var count));
        Assert.Equal(3, count);

        var lazy = list.Where(n => n > 1);
        Assert.False(lazy.TryGetNonEnumeratedCount(out _));
    }

    [Fact]
    public void Multiple_enumeration_of_a_lazy_sequence_runs_the_source_each_time()
    {
        var sourceCalls = 0;
        IEnumerable<int> Source() { sourceCalls++; yield return 1; yield return 2; }

        var lazy = Source();
        if (lazy.Any()) _ = lazy.Count();                 // 2 enumerasyon
        Assert.Equal(2, sourceCalls);

        sourceCalls = 0;
        var materialized = Source().ToList();             // tek enumerasyon
        if (materialized.Any()) _ = materialized.Count;
        Assert.Equal(1, sourceCalls);
    }

    // ───────── .NET 9/10 operatörleri ─────────

    [Fact]
    public void CountBy_groups_and_counts_without_materializing_groups()
    {
        string[] words = ["elma", "armut", "erik", "ayva", "kiraz"];
        var counts = words.CountBy(w => w[0]).ToDictionary();
        Assert.Equal(2, counts['e']);
        Assert.Equal(2, counts['a']);
        Assert.Equal(1, counts['k']);
    }

    [Fact]
    public void AggregateBy_sums_per_key()
    {
        (string Key, int Value)[] data = [("a", 1), ("b", 2), ("a", 3)];
        var sums = data.AggregateBy(x => x.Key, 0, (acc, x) => acc + x.Value).ToDictionary();
        Assert.Equal(4, sums["a"]);
        Assert.Equal(2, sums["b"]);
    }

    [Fact]
    public void Index_returns_position_and_value_pairs()
    {
        var pairs = new[] { "x", "y" }.Index().ToList();
        Assert.Equal([(0, "x"), (1, "y")], pairs);
    }

    [Fact]
    public void LeftJoin_keeps_unmatched_left_rows_with_default_right_and_RightJoin_the_opposite()
    {
        int[] left = [1, 2, 3];
        int[] right = [2, 3, 4];

        var l = left.LeftJoin(right, x => x, y => y, (x, y) => (x, y)).ToList();
        Assert.Equal([(1, 0), (2, 2), (3, 3)], l);

        var r = left.RightJoin(right, x => x, y => y, (x, y) => (x, y)).ToList();
        Assert.Equal([(2, 2), (3, 3), (0, 4)], r);
    }

    [Fact]
    public void Chunk_splits_into_arrays_with_a_shorter_last_chunk()
    {
        var chunks = Enumerable.Range(1, 7).Chunk(3).ToList();
        Assert.Equal(3, chunks.Count);
        Assert.Equal([7], chunks[^1]);
    }

    [Fact]
    public void MinBy_and_MaxBy_return_the_element_not_the_key()
    {
        string[] words = ["kedi", "fil", "aslan"];
        Assert.Equal("aslan", words.MaxBy(w => w.Length));
        Assert.Equal("fil", words.MinBy(w => w.Length));
    }

    [Fact]
    public void Order_sorts_without_a_key_selector()
    {
        Assert.Equal([1, 2, 3], new[] { 3, 1, 2 }.Order());
        Assert.Equal([3, 2, 1], new[] { 3, 1, 2 }.OrderDescending());
    }

    // ───────── IEnumerable vs IQueryable ─────────

    [Fact]
    public void Queryable_builds_an_expression_tree_instead_of_running_delegates()
    {
        var queryable = new List<int> { 5, 9, 12 }.AsQueryable().Where(n => n > 7).OrderBy(n => n);

        Assert.IsAssignableFrom<IQueryable<int>>(queryable);
        Assert.Equal(ExpressionType.Call, queryable.Expression.NodeType);
        Assert.Contains("Where", queryable.Expression.ToString());
        Assert.Equal([9, 12], queryable);
    }

    [Fact]
    public void Enumerable_Where_takes_a_delegate_and_Queryable_Where_takes_an_expression()
    {
        Assert.Equal(typeof(Func<,>), typeof(Enumerable).GetMethods().First(m => m.Name == "Where")
            .GetParameters()[1].ParameterType.GetGenericTypeDefinition());
        Assert.Equal(typeof(Expression<>), typeof(Queryable).GetMethods().First(m => m.Name == "Where")
            .GetParameters()[1].ParameterType.GetGenericTypeDefinition());
    }

    [Fact]
    public void AsEnumerable_switches_to_in_memory_evaluation()
    {
        var inMemory = new List<int> { 1, 2, 3 }.AsQueryable().AsEnumerable().Where(n => n > 1);
        Assert.IsNotAssignableFrom<IQueryable<int>>(inMemory);   // artık Enumerable.Where (delegate)
        Assert.Equal([2, 3], inMemory);
    }
}
