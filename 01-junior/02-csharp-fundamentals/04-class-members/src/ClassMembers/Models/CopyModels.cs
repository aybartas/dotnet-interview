using System.Text.Json;

namespace Junior.CSharpFundamentals.ClassMembers.Models;

public sealed class Address
{
    public string City { get; set; } = "";
    public string Street { get; set; } = "";
}

/// <summary>İç içe nesne (Address) ve koleksiyon (Tags) içeren sipariş modeli: kopyalama stratejilerini göstermek için.</summary>
public sealed class OrderModel
{
    public string Number { get; set; } = "";
    public Address ShippingAddress { get; set; } = new();       // referans tip → paylaşım riski
    public List<string> Tags { get; set; } = [];                // referans tip → paylaşım riski

    public OrderModel() { }

    /// <summary>Copy constructor: DERİN kopya (iç referanslar da yeniden oluşturulur). Açık, hızlı, tip güvenli.</summary>
    public OrderModel(OrderModel other)
    {
        Number = other.Number;
        ShippingAddress = new Address { City = other.ShippingAddress.City, Street = other.ShippingAddress.Street };
        Tags = [.. other.Tags];
    }

    /// <summary>object.MemberwiseClone() SHALLOW'dur: değer tipli alanlar kopyalanır, referans tipli alanlar PAYLAŞILIR.</summary>
    public OrderModel ShallowCopy() => (OrderModel)MemberwiseClone();

    /// <summary>Elle deep copy (copy constructor'a delege).</summary>
    public OrderModel DeepCopy() => new(this);

    /// <summary>JSON round-trip ile deep copy: yazması kolay ama yavaş (serileştir+ayrıştır) ve tip kısıtlı (ctor/properties/dairesel referans).</summary>
    public OrderModel DeepCopyViaJson() => JsonSerializer.Deserialize<OrderModel>(JsonSerializer.Serialize(this))!;
}

/// <summary>record + 'with' de SHALLOW'dur: iç içe referanslar paylaşılır.</summary>
public sealed record OrderRecord(string Number, Address ShippingAddress, List<string> Tags);

/// <summary>ICloneable.Clone() object döner ve shallow mı deep mi olduğunu SÖZLEŞMEDE belirtmez → Microsoft public API'de kullanılmamasını önerir.</summary>
public sealed class LegacyCloneable : ICloneable
{
    public List<int> Items { get; set; } = [1, 2, 3];
    public object Clone() => MemberwiseClone();                  // kullanıcı bunun shallow olduğunu nasıl bilecek?
}
