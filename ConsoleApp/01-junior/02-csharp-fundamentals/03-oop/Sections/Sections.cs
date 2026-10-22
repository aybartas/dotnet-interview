namespace Junior.CSharpFundamentals.Oop;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Four principles — by example",                     Section01_FourPrinciples.Run),
        new(2, "Compile-time vs runtime polymorphism",             Section02_Polymorphism.Run),
        new(3, "abstract class vs interface, explicit implementation", Section03_AbstractVsInterface.Run),
        new(4, "override vs new (method hiding) ⭐",               Section04_NewVsOverride.Run),
        new(5, "Upcasting / downcasting, is / as / pattern",       Section05_Casting.Run),
        new(6, "Aggregation vs composition (lifetime)",            Section06_Relationships.Run),
        new(7, "Coupling & cohesion",                              Section07_CouplingCohesion.Run),
        new(8, "OOP limits of struct, static and testability",     Section08_StructAndStatic.Run),
        new(9, "Inheritance → composition refactor (3 steps, same scenarios)", Section09_PaymentRefactoring.Run),
    ];
}
