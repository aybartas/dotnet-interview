using Junior.CollectionsLinq.CollectionsGenerics.Models;

namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section02_Interfaces
{
    public static void Run()
    {
        Console.WriteLine("""
        Arayüz hiyerarşisi (en dar → en geniş yetenek):

          IEnumerable<T>            sadece GEZİNME (foreach, LINQ)          ← en az varsayım
            └ IReadOnlyCollection<T>  + Count
                └ IReadOnlyList<T>      + indeksle okuma  [i]
          ICollection<T>            + Count, Add, Remove, Contains, Clear   ← DEĞİŞTİREBİLİR
            └ IList<T>              + indeksle erişim/atama, Insert, RemoveAt, IndexOf
          IDictionary<K,V> / IReadOnlyDictionary<K,V>   anahtar-değer
          ISet<T> / IReadOnlySet<T>                      küme işlemleri

        Parametre: EN GENEL arayüz (ihtiyacın olan minimum) → çağıran istediği koleksiyonu verebilir.   void Print(IEnumerable<int> items)
        Dönüş    : yeteri kadar özel, ama DEĞİŞTİRİLEMEZ arayüz → API'nin içini sızdırmaz.            IReadOnlyList<Order> GetOrders()
        """);

        var leaky = new LeakyInventory();
        leaky.Items.Clear();                                           // dışarıdan dahili listeyi BOŞALTTIK
        Console.WriteLine($"LeakyInventory: Items.Clear() çağrıldı → Items.Count = {leaky.Items.Count}   ← sınıf kendi durumunu koruyamıyor");

        var safe = new SafeInventory();
        Console.WriteLine($"\nSafeInventory.ItemsView ({safe.ItemsView.GetType().Name}): arayüz yalnızca okumaya izin verir (safe.ItemsView.Add → derlenmez)");
        ((List<string>)safe.ItemsView).Add("HİLE");                    // ama cast ile delinir! (IReadOnlyList = sözleşme, güvenlik duvarı değil)
        Console.WriteLine($"   List<string>'e cast edip Add yapıldı → Count = {safe.ItemsView.Count}   ← sözleşme güvenliği, gerçek koruma DEĞİL");

        var safe2 = new SafeInventory();
        Console.WriteLine($"SafeInventory.ItemsProtected ({safe2.ItemsProtected.GetType().Name}): cast edilse de değiştirilemez:");
        try { ((IList<string>)safe2.ItemsProtected).Add("HİLE"); }
        catch (NotSupportedException) { Console.WriteLine("   IList'e cast + Add → NotSupportedException ✓ (gerçek koruma)"); }

        var snapshot = safe2.Snapshot();
        safe2.Add("kalemlik");
        Console.WriteLine($"Snapshot(): kopya sonraki değişikliği görmez → snapshot {snapshot.Count} eleman, canlı liste {safe2.ItemsView.Count}");

        Console.WriteLine("""

        Neden read-only arayüz döndürülür?  (1) Encapsulation: sınıf invariant'ını (tekrarsızlık, sıralama, doğrulama) tek elden korur.
        (2) Sözleşme netliği: "bu koleksiyonu değiştirme" niyeti tipte okunur. (3) Sonradan iç temsili (List → Array/ImmutableArray) değiştirmek kırıcı olmaz.
        Seviyeler: IReadOnlyList (sözleşme) < AsReadOnly()/ReadOnlyCollection (sarmalayıcı) < Snapshot kopyası < ImmutableList/ImmutableArray (gerçek değişmezlik, → M1).
        Çağıranın gelecekteki değişikliğini görüp görmemesi (canlı görünüm vs anlık görüntü) ayrıca tasarım kararıdır.
        """);
    }
}
