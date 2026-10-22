using System.Linq.Expressions;

namespace Junior.CollectionsLinq.LinqBasics;

public static class Section07_EnumerableVsQueryable
{
    public static void Run()
    {
        var numbers = Enumerable.Range(1, 10).ToList();

        Console.WriteLine("IEnumerable<T>.Where(Func<T,bool>)         → the lambda compiles to a DELEGATE and runs IN MEMORY (LINQ to Objects)");
        Console.WriteLine("IQueryable<T>.Where(Expression<Func<T,bool>>) → the lambda becomes an EXPRESSION TREE (data); the PROVIDER (EF Core → SQL) interprets it\n");

        // IEnumerable
        IEnumerable<int> inMemory = numbers.Where(n => n > 7);
        Console.WriteLine($"   IEnumerable: {string.Join(",", inMemory)}  (type: {inMemory.GetType().Name})");

        // IQueryable (here the in-memory provider: EnumerableQuery). But the TREE is accessible:
        IQueryable<int> queryable = numbers.AsQueryable().Where(n => n > 7).OrderBy(n => n);
        Console.WriteLine($"   IQueryable : {string.Join(",", queryable)}  (type: {queryable.GetType().Name})");
        Console.WriteLine($"   queryable.Expression = {queryable.Expression}");
        Console.WriteLine("   ↑ The query is held as DATA: a provider can walk this tree and generate SQL (EF Core: SELECT … WHERE n > 7 ORDER BY n).");

        Console.WriteLine("\n   The same lambda compiles to two different things:");
        Func<int, bool> asDelegate = n => n > 7;
        Expression<Func<int, bool>> asExpression = n => n > 7;
        Console.WriteLine($"   Func<int,bool>              → executable code; cannot be inspected");
        Console.WriteLine($"   Expression<Func<int,bool>>  → body tree: {asExpression.Body} (NodeType={asExpression.Body.NodeType}, Left={((BinaryExpression)asExpression.Body).Left}, Right={((BinaryExpression)asExpression.Body).Right}); can be converted to a delegate with Compile(): {asExpression.Compile()(9)}");
        _ = asDelegate;

        Console.WriteLine("\n⭐ THE MOST IMPORTANT TRAP — which side it runs on:");
        Console.WriteLine("   db.Products.Where(p => p.Price > 100).ToList();                 // IQueryable: the filter is in SQL (WHERE) → the DB returns only the matches");
        Console.WriteLine("   db.Products.AsEnumerable().Where(p => p.Price > 100).ToList(); // AsEnumerable()/ToList() FIRST: the WHOLE table is pulled into memory, the filter runs in C#");
        Console.WriteLine("   db.Products.Where(p => MyMethod(p.Name))...                      // a C# method inside IQueryable that CANNOT be translated to SQL → runtime error (or client-side evaluation)");

        Console.WriteLine("""

        ┌────────────────────────┬────────────────────────────────┬──────────────────────────────────────────┐
        │                        │ IEnumerable<T>                 │ IQueryable<T>                            │
        ├────────────────────────┼────────────────────────────────┼──────────────────────────────────────────┤
        │ Namespace / class      │ System.Linq.Enumerable         │ System.Linq.Queryable                    │
        │ Operator parameter     │ Func<…> (delegate)             │ Expression<Func<…>> (expression tree)    │
        │ Where does it run?     │ In memory (in-process)         │ In the provider (SQL server, remote)     │
        │ Deferred?              │ Yes (iterator)                 │ Yes (runs when the query is enumerated)  │
        │ Typical source         │ List, array, yield             │ EF Core DbSet, in-memory AsQueryable     │
        │ What breaks the rule   │ Expensive work is repeated     │ Untranslatable code; early ToList()      │
        └────────────────────────┴────────────────────────────────┴──────────────────────────────────────────┘
        Rule: filtering/sorting/paging close to the data source (DB) belongs on IQueryable; in-memory operations (the ones that can't be translated to SQL) come AFTER ToList()/AsEnumerable().
        Do NOT LEAK IQueryable out across layers (where/when the query runs becomes unclear; DbContext lifetime) → a repository returns a materialized collection.
        Deep dive: expression trees, IQueryProvider, EF Core translation → M1.3, J9.4.
        """);
    }
}
