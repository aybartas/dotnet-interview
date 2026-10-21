using System.Reflection;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class Section09_ConstReadonly
{
    public sealed class Settings
    {
        public const int MaxItems = 100;                              // const: derleme zamanı sabiti, ÇAĞIRANLARA GÖMÜLÜR
        public static readonly DateTime StartedAt = DateTime.Now;     // static readonly: çalışma zamanında bir kez atanır
        public readonly Guid InstanceId = Guid.NewGuid();             // readonly (örnek alanı): constructor'da/bildirimde atanır
    }

    public static void Run()
    {
        var t = typeof(Settings);
        Console.WriteLine("Reflection ile fark:");
        foreach (var name in new[] { nameof(Settings.MaxItems), nameof(Settings.StartedAt), nameof(Settings.InstanceId) })
        {
            var f = t.GetField(name)!;
            Console.WriteLine($"   {name,-10} → IsLiteral(const)={f.IsLiteral,-5} IsInitOnly(readonly)={f.IsInitOnly,-5} IsStatic={f.IsStatic}");
        }

        Console.WriteLine("""

        const                : yalnızca derleme zamanında bilinen ilkel/string/enum/null değerler; implicit static; DEĞER, kullanıldığı her yere (başka assembly dahil) KOPYALANIR (IL'e literal olarak yazılır).
        static readonly      : çalışma zamanında hesaplanabilir (DateTime.Now, new List<>()); bir kez atanır; başka assembly'ler DEĞERİ çalışırken OKUR.
        readonly (instance)  : her nesne için kendi değeri; yalnızca constructor/initializer'da atanır.

        ⚠ VERSİYONLAMA TUZAĞI: Kütüphanedeki `public const int MaxRetries = 3` değerini 5 yapıp YALNIZCA kütüphaneyi yeniden derleyip dağıtırsanız,
          onu kullanan uygulama yeniden derlenmediği sürece HÂLÂ 3'ü kullanır (değer onun IL'ine gömülmüştür).
          Kanıt için: dotnet run -- 2.2 const-embedding (adımlar README 3.9)
          Kural: değişme ihtimali olan ve public olan değer için `public static readonly` (veya property) kullanın; const'ı gerçekten değişmeyen şeyler için ayırın (Math.PI, sabit alan sayıları).
        (readonly ≠ immutable: `readonly List<int> L` alanı yeniden atanamaz ama listenin İÇİ değiştirilebilir.)
        """);

        var readonlyList = new List<int> { 1 };
        var holder = new ReadonlyHolder(readonlyList);
        holder.Items.Add(2);                                      // izinli: referans sabit, nesne mutable
        Console.WriteLine($"readonly List içine eleman eklendi → [{string.Join(",", holder.Items)}]");
    }

    private sealed class ReadonlyHolder(List<int> items) { public readonly List<int> Items = items; }
}
