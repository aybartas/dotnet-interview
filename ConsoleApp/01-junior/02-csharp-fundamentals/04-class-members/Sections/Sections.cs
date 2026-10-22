namespace Junior.CSharpFundamentals.ClassMembers;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Property vs field: public field → property refactor ⭐", Section01_Properties.Run),
        new(2,  "Constructor kinds and INITIALIZATION ORDER ⭐",           Section02_InitializationOrder.Run),
        new(3,  "Calling a virtual method from a constructor (CA2214) ⭐", Section03_VirtualCallInConstructor.Run),
        new(4,  "Equality and hash: Equals / GetHashCode / IEquatable ⭐", Section04_EqualityAndHash.Run),
        new(5,  "Extension methods ⭐",                                    Section05_ExtensionMethods.Run),
        new(6,  "Operator overloading and indexers (Money, MoneyBag)",    Section06_OperatorsAndIndexers.Run),
        new(7,  "Shallow copy vs deep copy ⭐",                            Section07_Copying.Run),
        new(8,  "Optional/named parameters, anonymous types, nested classes", Section08_Misc.Run),
        new(9,  "Object lifecycle and intro to IDisposable (using)",      Section09_Lifecycle.Run),
    ];
}
