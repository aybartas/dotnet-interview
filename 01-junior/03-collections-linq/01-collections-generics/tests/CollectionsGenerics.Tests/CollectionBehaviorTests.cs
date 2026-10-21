using System.Collections.ObjectModel;
using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics.Tests;

public class CollectionBehaviorTests
{
    [Fact]
    public void Queue_is_fifo_and_stack_is_lifo()
    {
        var q = new Queue<int>([1, 2, 3]);
        var s = new Stack<int>([1, 2, 3]);
        Assert.Equal(1, q.Dequeue());
        Assert.Equal(3, s.Pop());
    }

    [Fact]
    public void PriorityQueue_dequeues_the_smallest_priority_first()
    {
        var pq = new PriorityQueue<string, int>();
        pq.Enqueue("düşük", 5); pq.Enqueue("acil", 1); pq.Enqueue("normal", 3);

        var order = new List<string>();
        while (pq.TryDequeue(out var item, out _)) order.Add(item);

        Assert.Equal(["acil", "normal", "düşük"], order);
    }

    [Fact]
    public void Sorted_collections_enumerate_in_key_order()
    {
        var sd = new SortedDictionary<string, int> { ["muz"] = 1, ["armut"] = 2, ["elma"] = 3 };
        Assert.Equal(["armut", "elma", "muz"], sd.Keys);

        var ss = new SortedSet<int> { 50, 10, 30, 20, 40 };
        Assert.Equal([20, 30, 40], ss.GetViewBetween(20, 40));
    }

    [Fact]
    public void HashSet_ignores_duplicates_and_supports_set_operations()
    {
        var a = new HashSet<int> { 1, 2, 3 };
        Assert.False(a.Add(2));
        Assert.Equal([3], a.Intersect(new[] { 3, 4 }));
        Assert.True(new HashSet<int> { 1, 2 }.IsSubsetOf(a));
    }

    [Fact]
    public void LinkedList_inserts_after_a_known_node_without_shifting()
    {
        var ll = new LinkedList<int>([1, 2, 4]);
        ll.AddAfter(ll.Find(2)!, 3);
        Assert.Equal([1, 2, 3, 4], ll);
    }

    [Fact]
    public void Dictionary_indexer_throws_for_missing_keys_while_TryGetValue_does_not()
    {
        var d = new Dictionary<string, int> { ["a"] = 1 };
        Assert.Throws<KeyNotFoundException>(() => d["yok"]);
        Assert.False(d.TryGetValue("yok", out var v));
        Assert.Equal(0, v);
        Assert.Equal(-1, d.GetValueOrDefault("yok", -1));
        Assert.False(d.TryAdd("a", 5));
    }

    [Fact]
    public void List_capacity_doubles_when_full()
    {
        var list = new List<int>();
        var capacities = new List<int>();
        for (var i = 0; i < 40; i++)
        {
            list.Add(i);
            if (capacities.Count == 0 || capacities[^1] != list.Capacity) capacities.Add(list.Capacity);
        }
        Assert.Equal([4, 8, 16, 32, 64], capacities);
    }

    [Fact]
    public void Presized_list_never_reallocates()
    {
        var list = new List<int>(100);
        for (var i = 0; i < 100; i++) list.Add(i);
        Assert.Equal(100, list.Capacity);
    }

    [Fact]
    public void Clear_does_not_shrink_capacity_but_TrimExcess_does()
    {
        var list = new List<int>(1000) { 1, 2, 3 };
        list.Clear();
        Assert.Equal(1000, list.Capacity);
        list.TrimExcess();
        Assert.Equal(0, list.Capacity);
    }

    [Fact]
    public void Modifying_a_list_during_foreach_throws()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.Throws<InvalidOperationException>(() => { foreach (var x in list) if (x == 2) list.Remove(x); });
        list.RemoveAll(x => x == 2);
        Assert.Equal([1, 3], list);
    }

    [Fact]
    public void Enumerators_are_independent()
    {
        IEnumerable<int> seq = new List<int> { 1, 2, 3 };
        using var e1 = seq.GetEnumerator();
        using var e2 = seq.GetEnumerator();
        e1.MoveNext(); e1.MoveNext();
        e2.MoveNext();
        Assert.Equal((2, 1), (e1.Current, e2.Current));
    }

    [Fact]
    public void Leaky_api_lets_callers_empty_the_internal_list()
    {
        var leaky = new LeakyInventory();
        leaky.Items.Clear();
        Assert.Empty(leaky.Items);
    }

    [Fact]
    public void IReadOnlyList_is_only_a_contract_it_can_be_cast_away_but_AsReadOnly_cannot()
    {
        var inv = new SafeInventory();
        ((List<string>)inv.ItemsView).Add("hile");                         // sözleşme delindi
        Assert.Equal(3, inv.ItemsView.Count);

        var inv2 = new SafeInventory();
        Assert.IsType<ReadOnlyCollection<string>>(inv2.ItemsProtected);
        Assert.Throws<NotSupportedException>(() => ((IList<string>)inv2.ItemsProtected).Add("hile"));
    }

    [Fact]
    public void Snapshot_does_not_see_later_changes_but_a_live_view_does()
    {
        var inv = new SafeInventory();
        var snapshot = inv.Snapshot();
        var live = inv.ItemsView;
        inv.Add("kalemlik");

        Assert.Equal(2, snapshot.Count);
        Assert.Equal(3, live.Count);
        Assert.False(inv.Add("kalemlik"));                                  // sınıf invariant'ını (tekrarsızlık) koruyor
    }
}
