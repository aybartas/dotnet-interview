// BİLEREK uyarı üreten kod. Her uyarının kodunu (CS86xx) IDE'de/derleme çıktısında bulup NEDEN çıktığını açıklayın,
// sonra DÜZELTİN. Hedef: 0 uyarı.
//
// Nullable referans tipleri (<Nullable>enable</Nullable>):
//   string   → asla null olmamalı (derleyici bunu takip eder)
//   string?  → null olabilir (kullanmadan önce kontrol etmelisin)
// Bunlar derleme zamanı ipuçlarıdır; çalışma zamanında bir koruma EKLEMEZ. Ama NullReferenceException'ı kodu çalıştırmadan yakalar.

namespace Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

public class UserService
{
    // CS8618: Non-nullable 'Name' constructor çıkışında null olabilir → ya başlatın, ya 'required' yapın, ya string? yapın.
    public string Name { get; set; }

    private readonly Dictionary<int, string> _users = new() { [1] = "ayse" };

    // CS8603: Olası null referans döndürme. TryGetValue başarısız olursa 'name' null'dır.
    public string GetUserName(int id)
    {
        _users.TryGetValue(id, out var name);
        return name;
    }

    // CS8600: null literal'i non-nullable türe dönüştürme.
    public string FirstOrDefaultName()
    {
        string name = null;
        return name ?? "bilinmiyor";
    }

    // CS8602: Olası null referansın başvurusunu kaldırma. 'Find' null dönebilir (string?).
    public int NameLength(int id)
    {
        var name = Find(id);
        return name.Length;
    }

    // CS8604: Olası null başvuru argümanı. 'Find' string? döner ama Print string bekler.
    public void ShowUser(int id)
    {
        Print(Find(id));
    }

    private string? Find(int id) => _users.GetValueOrDefault(id);

    private static void Print(string text) => Console.WriteLine(text);
}
