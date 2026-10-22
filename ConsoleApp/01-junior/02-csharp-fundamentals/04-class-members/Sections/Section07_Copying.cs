using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section07_Copying
{
    public static void Run()
    {
        OrderModel Create() => new() { Number = "A-1", ShippingAddress = new Address { City = "London", Street = "Main St." }, Tags = ["urgent"] };

        Console.WriteLine("1) Assignment (=) is NOT a copy: it is the same object");
        var original = Create();
        var alias = original;
        alias.Number = "CHANGED";
        Console.WriteLine($"   alias.Number changed → original.Number = {original.Number}");

        Console.WriteLine("\n2) MemberwiseClone → SHALLOW: inner references are SHARED");
        original = Create();
        var shallow = original.ShallowCopy();
        shallow.Number = "S-1";                                          // string/value: independent
        shallow.ShippingAddress.City = "Paris";                          // inner object: SHARED
        shallow.Tags.Add("gift");                                        // inner list: SHARED
        Console.WriteLine($"   I changed the shallow copy → original.Number = {original.Number} (unaffected), original.City = {original.ShippingAddress.City} (CORRUPTED!), original.Tags = [{string.Join(",", original.Tags)}] (CORRUPTED!)");
        Console.WriteLine($"   ReferenceEquals(original.ShippingAddress, shallow.ShippingAddress) = {ReferenceEquals(original.ShippingAddress, shallow.ShippingAddress)}");

        Console.WriteLine("\n3) Deep copy approaches (all PRESERVE the original)");
        foreach (var (name, copy) in new (string, Func<OrderModel, OrderModel>)[]
                 {
                     ("copy constructor / manual    ", o => new OrderModel(o)),
                     ("DeepCopy() (delegates to ctor)", o => o.DeepCopy()),
                     ("JSON round-trip              ", o => o.DeepCopyViaJson()),
                 })
        {
            original = Create();
            var deep = copy(original);
            deep.ShippingAddress.City = "Berlin";
            deep.Tags.Add("gift");
            Console.WriteLine($"   {name} → original.City = {original.ShippingAddress.City}, original.Tags = [{string.Join(",", original.Tags)}]  (unaffected)");
        }

        Console.WriteLine("\n4) record + 'with' is also SHALLOW (a trap worth knowing)");
        var rec = new OrderRecord("R-1", new Address { City = "London" }, ["urgent"]);
        var recCopy = rec with { Number = "R-2" };
        recCopy.ShippingAddress.City = "Rome";
        recCopy.Tags.Add("gift");
        Console.WriteLine($"   the copy made with 'with' was changed → rec.ShippingAddress.City = {rec.ShippingAddress.City} (CORRUPTED), rec.Tags = [{string.Join(",", rec.Tags)}] (CORRUPTED)");
        Console.WriteLine("   Fix: make nested types immutable too (record / ImmutableList) or supply a new inner object in the with: rec with { ShippingAddress = rec.ShippingAddress with { City = \"Rome\" } }");

        Console.WriteLine("\n5) Why is ICloneable discouraged?");
        object cloned = new LegacyCloneable().Clone();                   // returns object → a cast is needed
        Console.WriteLine($"   Clone() type: {cloned.GetType().Name} (a cast was needed); shallow or deep? The contract doesn't say → the user has to guess.");

        Console.WriteLine("""

        Summary: shallow = only top-level fields are copied (inner references are shared) · deep = the whole object graph is recreated.
        Deep copy approaches: (1) copy constructor/manual — explicit and fast, (2) JSON round-trip — easy but slow/type-restricted/fails on circular references,
        (3) immutable design — REDUCES the need for copying (sharing becomes safe). Deep copy via BinaryFormatter is NO LONGER USED (security).
        """);
    }
}
