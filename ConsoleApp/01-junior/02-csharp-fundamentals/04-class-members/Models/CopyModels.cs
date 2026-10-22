using System.Text.Json;

namespace Junior.CSharpFundamentals.ClassMembers.Models;

public sealed class Address
{
    public string City { get; set; } = "";
    public string Street { get; set; } = "";
}

/// <summary>An order model with a nested object (Address) and a collection (Tags): to demonstrate copy strategies.</summary>
public sealed class OrderModel
{
    public string Number { get; set; } = "";
    public Address ShippingAddress { get; set; } = new();       // reference type → sharing risk
    public List<string> Tags { get; set; } = [];                // reference type → sharing risk

    public OrderModel() { }

    /// <summary>Copy constructor: DEEP copy (inner references are recreated too). Explicit, fast, type-safe.</summary>
    public OrderModel(OrderModel other)
    {
        Number = other.Number;
        ShippingAddress = new Address { City = other.ShippingAddress.City, Street = other.ShippingAddress.Street };
        Tags = [.. other.Tags];
    }

    /// <summary>object.MemberwiseClone() is SHALLOW: value-type fields are copied, reference-type fields are SHARED.</summary>
    public OrderModel ShallowCopy() => (OrderModel)MemberwiseClone();

    /// <summary>Manual deep copy (delegates to the copy constructor).</summary>
    public OrderModel DeepCopy() => new(this);

    /// <summary>Deep copy via JSON round-trip: easy to write but slow (serialize+parse) and type-restricted (ctor/properties/circular references).</summary>
    public OrderModel DeepCopyViaJson() => JsonSerializer.Deserialize<OrderModel>(JsonSerializer.Serialize(this))!;
}

/// <summary>record + 'with' is also SHALLOW: nested references are shared.</summary>
public sealed record OrderRecord(string Number, Address ShippingAddress, List<string> Tags);

/// <summary>ICloneable.Clone() returns object and does NOT state in the CONTRACT whether it is shallow or deep → Microsoft recommends not using it in public APIs.</summary>
public sealed class LegacyCloneable : ICloneable
{
    public List<int> Items { get; set; } = [1, 2, 3];
    public object Clone() => MemberwiseClone();                  // how will the user know this is shallow?
}
