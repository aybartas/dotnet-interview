namespace Junior.CSharpFundamentals.ClassMembers.Models;

// ═════════ BEFORE: a model with public fields ═════════
// Problems: (1) no validation — Price = -5 is allowed, (2) can't be read-only/set-only-in-ctor,
// (3) converting to a property later is a BINARY BREAKING CHANGE (below), (4) data binding/serialization/EF mostly expect properties.
public class ProductBefore
{
    public string Name = "";
    public decimal Price;
    public int Stock;
}

// ═════════ AFTER: property + validation + invariant protection ═════════
public class ProductAfter
{
    // required (C# 11): this member MUST be assigned when the object is created (via object initializer) — no "half-built object"
    public required string Name { get; init; }            // init: assigned only during creation, read-only afterwards

    // C# 14 'field' keyword: validation in the setter without hand-writing the backing field
    public decimal Price
    {
        get;
        set => field = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative");
    }

    // private setter: can't be changed from outside, state is changed only by the class's own methods
    public int Stock { get; private set; }

    // computed property: no storage; should be cheap and side-effect-free
    public bool InStock => Stock > 0;

    public void AddStock(int quantity)                    // EXPENSIVE/side-effecting work → method (not a property)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Stock += quantity;
    }

    public void Remove(int quantity)
    {
        if (quantity > Stock) throw new InvalidOperationException("Insufficient stock");
        Stock -= quantity;
    }

    // static property: a value shared at the type level
    public static int CreatedCount { get; private set; }
    public ProductAfter() => CreatedCount++;

    // readonly field vs { get; }: a field is a slot in memory; a property is a pair of METHODS (get_X). Prefer properties in public APIs.
    public readonly DateTime CreatedAtField = DateTime.UtcNow;
    public DateTime CreatedAtProperty { get; } = DateTime.UtcNow;
}
