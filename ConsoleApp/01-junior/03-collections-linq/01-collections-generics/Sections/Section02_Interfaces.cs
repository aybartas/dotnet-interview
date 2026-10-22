using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section02_Interfaces
{
    public static void Run()
    {
        Console.WriteLine("""
        Interface hierarchy (narrowest → widest capability):

          IEnumerable<T>            ENUMERATION only (foreach, LINQ)          ← fewest assumptions
            └ IReadOnlyCollection<T>  + Count
                └ IReadOnlyList<T>      + read by index  [i]
          ICollection<T>            + Count, Add, Remove, Contains, Clear   ← MODIFIABLE
            └ IList<T>              + index get/set, Insert, RemoveAt, IndexOf
          IDictionary<K,V> / IReadOnlyDictionary<K,V>   key-value
          ISet<T> / IReadOnlySet<T>                      set operations

        Parameter: the MOST GENERAL interface (the minimum you need) → the caller can pass whichever collection they like.   void Print(IEnumerable<int> items)
        Return   : specific enough, but a NON-MODIFIABLE interface → does not leak the API's internals.                      IReadOnlyList<Order> GetOrders()
        """);

        var leaky = new LeakyInventory();
        leaky.Items.Clear();                                           // we EMPTIED the internal list from the outside
        Console.WriteLine($"LeakyInventory: Items.Clear() was called → Items.Count = {leaky.Items.Count}   ← the class cannot protect its own state");

        var safe = new SafeInventory();
        Console.WriteLine($"\nSafeInventory.ItemsView ({safe.ItemsView.GetType().Name}): the interface only allows reading (safe.ItemsView.Add → does not compile)");
        ((List<string>)safe.ItemsView).Add("CHEAT");                   // but a cast breaks through! (IReadOnlyList = a contract, not a firewall)
        Console.WriteLine($"   Cast to List<string> and Add → Count = {safe.ItemsView.Count}   ← contract safety, NOT real protection");

        var safe2 = new SafeInventory();
        Console.WriteLine($"SafeInventory.ItemsProtected ({safe2.ItemsProtected.GetType().Name}): cannot be modified even when cast:");
        try { ((IList<string>)safe2.ItemsProtected).Add("CHEAT"); }
        catch (NotSupportedException) { Console.WriteLine("   cast to IList + Add → NotSupportedException ✓ (real protection)"); }

        var snapshot = safe2.Snapshot();
        safe2.Add("pencil case");
        Console.WriteLine($"Snapshot(): the copy does not see later changes → snapshot has {snapshot.Count} items, live list has {safe2.ItemsView.Count}");

        Console.WriteLine("""

        Why return a read-only interface?  (1) Encapsulation: the class protects its invariant (uniqueness, ordering, validation) in one place.
        (2) Contract clarity: the "do not modify this collection" intent is readable in the type. (3) Changing the internal representation later (List → Array/ImmutableArray) is not a breaking change.
        Levels: IReadOnlyList (contract) < AsReadOnly()/ReadOnlyCollection (wrapper) < Snapshot copy < ImmutableList/ImmutableArray (true immutability, → M1).
        Whether the caller sees future changes (live view vs snapshot) is a separate design decision.
        """);
    }
}
