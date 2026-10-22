namespace Junior.CollectionsLinq.CollectionsGenerics;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Collection tour: List, Dictionary, HashSet, Queue, Stack, LinkedList, Sorted*, PriorityQueue", Section01_Tour.Run),
        new(2, "Interface hierarchy and read-only interfaces in public APIs", Section02_Interfaces.Run),
        new(3, "Array vs List<T>: capacity growth",                           Section03_Capacity.Run),
        new(4, "IEnumerator<T> under foreach",                                Section04_Enumeration.Run),
        new(5, "Lookup time: List.Contains vs HashSet vs Dictionary ⭐",       Section05_LookupBenchmark.Run),
        new(6, "Dictionary key contract: broken GetHashCode/Equals ⭐",        Section06_KeyContract.Run),
        new(7, "Generics: why, constraints, Repository<T>",                   Section07_Generics.Run),
        new(8, "Which collection when? (decision table)",                     Section08_DecisionTable.Run),
    ];
}
