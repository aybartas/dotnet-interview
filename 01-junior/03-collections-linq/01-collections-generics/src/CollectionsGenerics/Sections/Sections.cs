namespace Junior.CollectionsLinq.CollectionsGenerics;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Koleksiyon turu: List, Dictionary, HashSet, Queue, Stack, LinkedList, Sorted*, PriorityQueue", Section01_Tour.Run),
        new(2, "Arayüz hiyerarşisi ve public API'de read-only arayüz",   Section02_Interfaces.Run),
        new(3, "Array vs List<T>: kapasite büyümesi",                    Section03_Capacity.Run),
        new(4, "foreach altında IEnumerator<T>",                         Section04_Enumeration.Run),
        new(5, "Arama süresi: List.Contains vs HashSet vs Dictionary ⭐", Section05_LookupBenchmark.Run),
        new(6, "Dictionary key sözleşmesi: bozuk GetHashCode/Equals ⭐",  Section06_KeyContract.Run),
        new(7, "Generics: neden, constraint'ler, Repository<T>",         Section07_Generics.Run),
        new(8, "Hangi koleksiyon ne zaman? (karar tablosu)",             Section08_DecisionTable.Run),
    ];
}
