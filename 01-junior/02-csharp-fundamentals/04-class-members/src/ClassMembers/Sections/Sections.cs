namespace Junior.CSharpFundamentals.ClassMembers;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Property vs field: public field → property refactor ⭐", Section01_Properties.Run),
        new(2,  "Constructor çeşitleri ve BAŞLATMA SIRASI ⭐",             Section02_InitializationOrder.Run),
        new(3,  "Constructor'dan virtual metot çağırmak (CA2214) ⭐",      Section03_VirtualCallInConstructor.Run),
        new(4,  "Eşitlik ve hash: Equals / GetHashCode / IEquatable ⭐",   Section04_EqualityAndHash.Run),
        new(5,  "Extension method'lar ⭐",                                 Section05_ExtensionMethods.Run),
        new(6,  "Operator overloading ve indexer (Money, MoneyBag)",      Section06_OperatorsAndIndexers.Run),
        new(7,  "Shallow copy vs deep copy ⭐",                            Section07_Copying.Run),
        new(8,  "Optional/named parametreler, anonim tipler, nested sınıflar", Section08_Misc.Run),
        new(9,  "Nesne yaşam döngüsü ve IDisposable'a giriş (using)",    Section09_Lifecycle.Run),
    ];
}
