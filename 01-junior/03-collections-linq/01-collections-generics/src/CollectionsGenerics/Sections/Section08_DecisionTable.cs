namespace Junior.CollectionsLinq.CollectionsGenerics;

public static class Section08_DecisionTable
{
    public static void Run() => Console.WriteLine("""
        HANGİ KOLEKSİYON NE ZAMAN?  (Big-O ortalama; teori ve ispat → D2, D3)

        ┌───────────────────────────┬──────────────────────┬────────────┬────────────┬───────────────────────────────────────────────┐
        │ İhtiyaç                   │ Seçim                │ Arama      │ Ekleme     │ Not                                           │
        ├───────────────────────────┼──────────────────────┼────────────┼────────────┼───────────────────────────────────────────────┤
        │ Sıralı liste, indeksle    │ List<T>              │ O(n)       │ O(1)* sona │ VARSAYILAN seçim. Boyut biliniyorsa capacity  │
        │ Sabit boyut, ham hız      │ T[]  (Array)         │ O(n) / [i] │ —          │ Span<T> ile uyumlu; boyut değişmez            │
        │ "Var mı?" / tekrarsız     │ HashSet<T>           │ O(1)       │ O(1)       │ Sıra GARANTİSİ yok; küme işlemleri            │
        │ Anahtar → değer           │ Dictionary<K,V>      │ O(1)       │ O(1)       │ Anahtar sözleşmesi (Equals/GetHashCode)       │
        │ Anahtarla SIRALI gezinme  │ SortedDictionary<K,V>│ O(log n)   │ O(log n)   │ Ekleme/silme çoksa; SortedList: arama hızlı   │
        │ Sıralı + aralık sorgusu   │ SortedSet<T>         │ O(log n)   │ O(log n)   │ GetViewBetween, Min/Max                       │
        │ İlk giren ilk çıkar       │ Queue<T>             │ —          │ O(1)       │ İş kuyruğu, BFS                               │
        │ Son giren ilk çıkar       │ Stack<T>             │ —          │ O(1)       │ Undo, DFS, ifade ayrıştırma                   │
        │ Öncelikli işleme          │ PriorityQueue<E,P>   │ —          │ O(log n)   │ Min-heap; zamanlayıcı, Dijkstra               │
        │ Düğüm ortasına sık ekleme │ LinkedList<T>        │ O(n)       │ O(1)**     │ ** düğüm biliniyorsa; pratikte çoğu zaman List│
        │ Çok thread'li erişim      │ Concurrent*          │ …          │ …          │ → M4                                          │
        │ Değişmez                  │ Immutable* / Frozen* │ …          │ —          │ → M1; FrozenDictionary okuma-ağırlıklı        │
        └───────────────────────────┴──────────────────────┴────────────┴────────────┴───────────────────────────────────────────────┘

        KISA KURALLAR
          • Şüphedeysen List<T>. Anahtarla arıyorsan Dictionary. "Var mı?" soruyorsan HashSet.
          • Public API: parametre IEnumerable<T> (veya en dar arayüz), dönüş IReadOnlyList<T>/IReadOnlyDictionary<K,V>.
          • Çok sayıda eleman ve çok sayıda arama → hash tabanlı; birkaç eleman → List yeterli.
          • Performans iddialarını ÖLÇÜN (Section 5): cache yerelliği nedeniyle küçük n'de List genelde LinkedList'ten ve bazen Dictionary'den hızlıdır.
        """);
}
