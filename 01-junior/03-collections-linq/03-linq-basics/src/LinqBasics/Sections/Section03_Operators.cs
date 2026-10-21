using Junior.CollectionsLinq.LinqBasics.Data;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section03_Operators
{
    public static void Run()
    {
        var d = new ShopData();
        string J<T>(IEnumerable<T> xs) => string.Join(", ", xs);

        Console.WriteLine("— Süzme ve projeksiyon —");
        Console.WriteLine($"   Where(Price > 200)            : {J(d.Products.Where(p => p.Price > 200).Select(p => p.Name))}");
        Console.WriteLine($"   Select(adı büyük)             : {J(d.Products.Take(3).Select(p => p.Name.ToUpperInvariant()))}");
        Console.WriteLine($"   Select((p, index) => …)       : {J(d.Products.Take(3).Select((p, i) => $"{i + 1}.{p.Name}"))}   (indeksli overload)");
        Console.WriteLine($"   SelectMany (düzleştirme)      : {J(d.Orders.Take(2).SelectMany(o => d.Items.Where(i => i.OrderId == o.Id), (o, i) => $"#{o.Id}:{i.ProductId}"))}   (sipariş → kalemler → TEK düz liste; Select kullansaydık liste-listesi olurdu)");

        Console.WriteLine("\n— Sıralama —");
        Console.WriteLine($"   OrderBy(Category).ThenByDescending(Price): {J(d.Products.OrderBy(p => p.Category, StringComparer.Ordinal).ThenByDescending(p => p.Price).Select(p => $"{p.Category}:{p.Name}"))}");
        Console.WriteLine("   OrderBy KARARLI (stable) sıralar: eşit anahtarlı elemanlar özgün sırasını korur. ThenBy ikincil anahtar için; ikinci OrderBy birincisini EZER.");

        Console.WriteLine("\n— Gruplama —");
        foreach (var g in d.Products.GroupBy(p => p.Category).OrderBy(g => g.Key, StringComparer.Ordinal))
            Console.WriteLine($"   GroupBy(Category) → {g.Key,-10}: {J(g.Select(p => p.Name))}  (Count={g.Count()}, Ortalama fiyat={g.Average(p => p.Price):N1})");
        Console.WriteLine("   GroupBy bir IGrouping<TKey,TElement> dizisi döner: grup = anahtar (Key) + elemanlar. SQL'in GROUP BY'ından farkı: grubun TÜM elemanlarına erişirsiniz.");

        Console.WriteLine("\n— Birleştirme (Join) —");
        var joined = d.Orders.Join(d.Customers, o => o.CustomerId, c => c.Id, (o, c) => $"#{o.Id}→{c.Name.Split(' ')[0]}").Take(4);
        Console.WriteLine($"   Join (inner)                  : {J(joined)}   (eşleşmeyen satırlar sonuçta YOK)");
        var groupJoin = d.Customers.GroupJoin(d.Orders, c => c.Id, o => o.CustomerId, (c, os) => $"{c.Name.Split(' ')[0]}={os.Count()}");
        Console.WriteLine($"   GroupJoin (sol dış benzeri)   : {J(groupJoin)}   (eşleşmesi olmayan müşteri de 0 ile görünür)");

        Console.WriteLine("\n— Tekrar kaldırma —");
        Console.WriteLine($"   Select(City).Distinct()       : {J(d.Customers.Select(c => c.City).Distinct())}");
        Console.WriteLine($"   DistinctBy(c => c.City)       : {J(d.Customers.DistinctBy(c => c.City).Select(c => c.Name.Split(' ')[0]))}   (şehir başına İLK müşteri)");

        Console.WriteLine("\n— Aggregate operatörleri —");
        var prices = d.Products.Select(p => p.Price);
        Console.WriteLine($"   Count={prices.Count()}  Sum={prices.Sum():N2}  Average={prices.Average():N2}  Min={prices.Min():N2}  Max={prices.Max():N2}");
        Console.WriteLine($"   MinBy/MaxBy (nesneyi döner)   : {d.Products.MinBy(p => p.Price)!.Name} / {d.Products.MaxBy(p => p.Price)!.Name}   (Min/Max değeri, MinBy/MaxBy elemanı verir)");
        Console.WriteLine($"   Any(Price > 900)={d.Products.Any(p => p.Price > 900)}  All(Price > 10)={d.Products.All(p => p.Price > 10)}  Contains(40m)={prices.Contains(40m)}");
        Console.WriteLine($"   Aggregate (elle katlama)      : {d.Products.Take(4).Select(p => p.Name).Aggregate((acc, n) => acc + " | " + n)}");

        Console.WriteLine("\n— Parçalama ve dönüştürme —");
        Console.WriteLine($"   Take(3) / Skip(8)             : {J(d.Products.Take(3).Select(p => p.Id))} / {J(d.Products.Skip(8).Select(p => p.Id))}   ·  TakeWhile/SkipWhile/Take(^2..) da var");
        Console.WriteLine($"   Chunk(4)                      : {J(d.Products.Select(p => p.Id).Chunk(4).Select(c => $"[{J(c)}]"))}");
        Console.WriteLine($"   Zip (iki diziyi eşle)         : {J(d.Products.Take(3).Select(p => p.Name).Zip(d.Products.Take(3).Select(p => p.Price), (n, p) => $"{n}={p}"))}");
        var dict = d.Products.ToDictionary(p => p.Id, p => p.Name);
        Console.WriteLine($"   ToDictionary(Id → Ad)         : dict[3] = {dict[3]}   (anahtar TEKİL olmalı; tekrar → ArgumentException)");
        var lookup = d.Products.ToLookup(p => p.Category, p => p.Name);
        Console.WriteLine($"   ToLookup(Category → Ad)       : lookup[\"Ev\"] = [{J(lookup["Ev"])}]; lookup[\"Yok\"] boş = {!lookup["Yok"].Any()}   (Dictionary'den farkı: anahtar tekrarı serbest, eksik anahtar exception ATMAZ, boş dizi döner)");

        Console.WriteLine("\n— Kümeler —");
        int[] a = [1, 2, 3, 4], b = [3, 4, 5];
        Console.WriteLine($"   Union={J(a.Union(b))} · Intersect={J(a.Intersect(b))} · Except={J(a.Except(b))} · Concat={J(a.Concat(b))} (Concat tekrarları korur, Union atar)");
    }
}
