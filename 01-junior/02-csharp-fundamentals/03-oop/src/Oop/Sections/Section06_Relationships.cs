using Junior.CSharpFundamentals.Oop.Relationships;

namespace Junior.CSharpFundamentals.Oop;

public static class Section06_Relationships
{
    public static void Run()
    {
        Console.WriteLine("""
        ┌──────────────┬────────────────┬──────────────────────────────┬──────────────────────┐
        │ İlişki       │ Anlam          │ Yaşam süresi                 │ Örnek                │
        ├──────────────┼────────────────┼──────────────────────────────┼──────────────────────┤
        │ Inheritance  │ "is-a"         │ —                            │ SavingsAccount:Account│
        │ Association  │ "uses-a"       │ Bağımsız, sahiplik yok       │ Order ↔ Courier      │
        │ Aggregation  │ zayıf "has-a"  │ Parça bütünden UZUN yaşar    │ Team ↔ Employee      │
        │ Composition  │ güçlü "has-a"  │ Parça bütünle BİRLİKTE ölür  │ Order ↔ OrderLine    │
        └──────────────┴────────────────┴──────────────────────────────┴──────────────────────┘
        """);

        Console.WriteLine("AGGREGATION — Team / Employee");
        var ali = new Employee("Ali");                       // çalışan takımdan BAĞIMSIZ oluşturuldu
        var veli = new Employee("Veli");
        var backend = new Team("Backend");
        var platform = new Team("Platform");
        backend.Add(ali); backend.Add(veli);
        platform.Add(veli);                                  // Veli başka takıma TAŞINDI
        Console.WriteLine($"   backend: [{string.Join(", ", backend.Members.Select(m => m.Name))}]  platform: [{string.Join(", ", platform.Members.Select(m => m.Name))}]");
        backend.Disband();
        Console.WriteLine($"   backend dağıtıldı → Ali hâlâ var mı? Evet (Team = {(ali.Team is null ? "yok" : ali.Team.Name)}); Veli: Team = {veli.Team?.Name}");

        Console.WriteLine("\nCOMPOSITION — Order / OrderLine");
        var order = new Order();
        var line = order.AddLine("Kalem", 3, 12.5m);          // satırı SİPARİŞ yaratır
        order.AddLine("Defter", 1, 40m);
        Console.WriteLine($"   sipariş toplamı: {order.Total:N2}; satır sahibi sipariş mi? {ReferenceEquals(line.Owner, order)}");
        Console.WriteLine($"   OrderLine ctor public mi? {typeof(OrderLine).GetConstructors().Length > 0} (dışarıdan tek başına üretilemez)");
        order.Cancel();
        Console.WriteLine($"   sipariş iptal → satır sayısı: {order.Lines.Count}; eski satır yetim mi? {line.IsOrphaned}");
        Console.WriteLine("   Yaşam süresi farkının GC ile kanıtı: tests/Oop.Tests/RelationshipTests.cs (WeakReference)");
    }
}
