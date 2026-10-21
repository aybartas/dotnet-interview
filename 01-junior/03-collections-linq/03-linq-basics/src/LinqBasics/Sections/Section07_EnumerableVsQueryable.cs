using System.Linq.Expressions;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section07_EnumerableVsQueryable
{
    public static void Run()
    {
        var numbers = Enumerable.Range(1, 10).ToList();

        Console.WriteLine("IEnumerable<T>.Where(Func<T,bool>)         → lambda DELEGATE'e derlenir, BELLEKTE (LINQ to Objects) çalışır");
        Console.WriteLine("IQueryable<T>.Where(Expression<Func<T,bool>>) → lambda İFADE AĞACI'na (veri) çevrilir; SAĞLAYICI (EF Core → SQL) onu yorumlar\n");

        // IEnumerable
        IEnumerable<int> inMemory = numbers.Where(n => n > 7);
        Console.WriteLine($"   IEnumerable: {string.Join(",", inMemory)}  (tip: {inMemory.GetType().Name})");

        // IQueryable (burada bellek-içi sağlayıcı: EnumerableQuery). Ama AĞAÇ erişilebilir:
        IQueryable<int> queryable = numbers.AsQueryable().Where(n => n > 7).OrderBy(n => n);
        Console.WriteLine($"   IQueryable : {string.Join(",", queryable)}  (tip: {queryable.GetType().Name})");
        Console.WriteLine($"   queryable.Expression = {queryable.Expression}");
        Console.WriteLine("   ↑ Sorgu bir VERİ olarak tutuluyor: sağlayıcı bu ağacı gezip SQL üretebilir (EF Core: SELECT … WHERE n > 7 ORDER BY n).");

        Console.WriteLine("\n   Aynı lambda iki farklı şey olarak derlenir:");
        Func<int, bool> asDelegate = n => n > 7;
        Expression<Func<int, bool>> asExpression = n => n > 7;
        Console.WriteLine($"   Func<int,bool>              → çalıştırılabilir kod; incelenemez");
        Console.WriteLine($"   Expression<Func<int,bool>>  → gövde ağacı: {asExpression.Body} (NodeType={asExpression.Body.NodeType}, Left={((BinaryExpression)asExpression.Body).Left}, Right={((BinaryExpression)asExpression.Body).Right}); Compile() ile delegate'e çevrilebilir: {asExpression.Compile()(9)}");
        _ = asDelegate;

        Console.WriteLine("\n⭐ EN ÖNEMLİ TUZAK — hangi tarafta çalıştığı:");
        Console.WriteLine("   db.Products.Where(p => p.Price > 100).ToList();                 // IQueryable: filtre SQL'de (WHERE) → DB sadece eşleşenleri döner");
        Console.WriteLine("   db.Products.AsEnumerable().Where(p => p.Price > 100).ToList(); // AsEnumerable()/ToList() ÖNCE: TÜM tablo belleğe çekilir, filtre C#'ta");
        Console.WriteLine("   db.Products.Where(p => MyMethod(p.Name))...                      // IQueryable içinde SQL'e ÇEVRİLEMEYEN C# metodu → çalışma zamanı hatası (veya istemci tarafı değerlendirme)");

        Console.WriteLine("""

        ┌────────────────────────┬────────────────────────────────┬──────────────────────────────────────────┐
        │                        │ IEnumerable<T>                 │ IQueryable<T>                            │
        ├────────────────────────┼────────────────────────────────┼──────────────────────────────────────────┤
        │ Namespace / sınıf      │ System.Linq.Enumerable         │ System.Linq.Queryable                    │
        │ Operatör parametresi   │ Func<…> (delegate)             │ Expression<Func<…>> (ifade ağacı)        │
        │ Nerede çalışır?        │ Bellekte (process içi)         │ Sağlayıcıda (SQL sunucusu, uzak kaynak)  │
        │ Ertelenmiş mi?         │ Evet (iterator)                │ Evet (sorgu enumerate edilince çalışır)  │
        │ Tipik kaynak           │ List, array, yield             │ EF Core DbSet, OData, bellek AsQueryable │
        │ Kuralı bozan şey       │ Pahalı iş yinelenir            │ SQL'e çevrilemeyen ifade; erken ToList() │
        └────────────────────────┴────────────────────────────────┴──────────────────────────────────────────┘
        Kural: veri kaynağına (DB) yakın filtreleme/sıralama/sayfalama IQueryable üzerinde; bellek-içi işlemler (SQL'e çevrilemeyenler) ToList()/AsEnumerable()'dan SONRA.
        IQueryable'ı katmanlar arasında dışarı SIZDIRMAYIN (sorgu nerede/ne zaman çalışır belirsizleşir; DbContext ömrü) → repository sonuç olarak materialize edilmiş koleksiyon döner.
        Derin hali: ifade ağaçları, IQueryProvider, EF Core çeviri → M1.3, J9.4.
        """);
    }
}
