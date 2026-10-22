using Junior.CSharpFundamentals.ClassMembers.Models;

namespace Junior.CSharpFundamentals.ClassMembers;

public static class Section07_Copying
{
    public static void Run()
    {
        OrderModel Create() => new() { Number = "A-1", ShippingAddress = new Address { City = "Ankara", Street = "Atatürk Cd." }, Tags = ["acil"] };

        Console.WriteLine("1) Atama (=) KOPYA DEĞİLDİR: aynı nesne");
        var original = Create();
        var alias = original;
        alias.Number = "DEĞİŞTİ";
        Console.WriteLine($"   alias.Number değişti → original.Number = {original.Number}");

        Console.WriteLine("\n2) MemberwiseClone → SHALLOW: iç referanslar PAYLAŞILIR");
        original = Create();
        var shallow = original.ShallowCopy();
        shallow.Number = "S-1";                                          // string/değer: bağımsız
        shallow.ShippingAddress.City = "İstanbul";                       // iç nesne: PAYLAŞILIYOR
        shallow.Tags.Add("hediye");                                      // iç liste: PAYLAŞILIYOR
        Console.WriteLine($"   shallow'u değiştirdim → original.Number = {original.Number} (bozulmadı), original.City = {original.ShippingAddress.City} (BOZULDU!), original.Tags = [{string.Join(",", original.Tags)}] (BOZULDU!)");
        Console.WriteLine($"   ReferenceEquals(original.ShippingAddress, shallow.ShippingAddress) = {ReferenceEquals(original.ShippingAddress, shallow.ShippingAddress)}");

        Console.WriteLine("\n3) Deep copy yolları (hepsi orijinali KORUR)");
        foreach (var (name, copy) in new (string, Func<OrderModel, OrderModel>)[]
                 {
                     ("copy constructor / elle  ", o => new OrderModel(o)),
                     ("DeepCopy() (ctor'a delege)", o => o.DeepCopy()),
                     ("JSON round-trip          ", o => o.DeepCopyViaJson()),
                 })
        {
            original = Create();
            var deep = copy(original);
            deep.ShippingAddress.City = "İzmir";
            deep.Tags.Add("hediye");
            Console.WriteLine($"   {name} → original.City = {original.ShippingAddress.City}, original.Tags = [{string.Join(",", original.Tags)}]  (bozulmadı)");
        }

        Console.WriteLine("\n4) record + 'with' de SHALLOW'dur (bilinmesi gereken tuzak)");
        var rec = new OrderRecord("R-1", new Address { City = "Ankara" }, ["acil"]);
        var recCopy = rec with { Number = "R-2" };
        recCopy.ShippingAddress.City = "Bursa";
        recCopy.Tags.Add("hediye");
        Console.WriteLine($"   with ile kopyalanan değiştirildi → rec.ShippingAddress.City = {rec.ShippingAddress.City} (BOZULDU), rec.Tags = [{string.Join(",", rec.Tags)}] (BOZULDU)");
        Console.WriteLine("   Çözüm: iç içe tipleri de immutable yapın (record / ImmutableList) ya da with sırasında yeni iç nesne verin: rec with { ShippingAddress = rec.ShippingAddress with { City = \"Bursa\" } }");

        Console.WriteLine("\n5) ICloneable neden önerilmez?");
        object cloned = new LegacyCloneable().Clone();                   // object döner → cast gerekir
        Console.WriteLine($"   Clone() tipi: {cloned.GetType().Name} (cast gerekti); shallow mı deep mi? Sözleşme söylemiyor → kullanıcı tahmin etmek zorunda.");

        Console.WriteLine("""

        Özet: shallow = yalnızca üst düzey alanlar kopyalanır (iç referanslar paylaşılır) · deep = tüm nesne grafiği yeniden oluşturulur.
        Deep copy yolları: (1) copy constructor/elle — açık ve hızlı, (2) JSON round-trip — kolay ama yavaş/tip kısıtlı/dairesel referansta hata,
        (3) immutable tasarım — kopyalama ihtiyacını AZALTIR (paylaşım güvenli olur). BinaryFormatter ile deep copy ARTIK KULLANILMAZ (güvenlik).
        """);
    }
}
