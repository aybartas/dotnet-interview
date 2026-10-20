namespace Junior.CSharpFundamentals.Oop;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Dört ilke — örnekle",                              Section01_FourPrinciples.Run),
        new(2, "Compile-time vs runtime polimorfizm",              Section02_Polymorphism.Run),
        new(3, "abstract class vs interface, explicit implementation", Section03_AbstractVsInterface.Run),
        new(4, "override vs new (method hiding) ⭐",               Section04_NewVsOverride.Run),
        new(5, "Upcasting / downcasting, is / as / pattern",       Section05_Casting.Run),
        new(6, "Aggregation vs composition (yaşam süresi)",        Section06_Relationships.Run),
        new(7, "Coupling & cohesion",                              Section07_CouplingCohesion.Run),
        new(8, "struct'ın OOP kısıtları, static ve test edilebilirlik", Section08_StructAndStatic.Run),
        new(9, "Inheritance → composition refactor (3 adım, aynı testler)", Section09_PaymentRefactoring.Run),
    ];
}
