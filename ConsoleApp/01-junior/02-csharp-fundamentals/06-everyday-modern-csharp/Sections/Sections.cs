namespace Junior.CSharpFundamentals.ModernCSharp;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Records: record, record struct, with, value equality",  Section01_Records.Run),
        new(2,  "init-only setters and required members",                Section02_InitRequired.Run),
        new(3,  "Pattern matching: type, constant, relational, logical, property", Section03_Patterns.Run),
        new(4,  "switch expression",                                     Section04_SwitchExpression.Run),
        new(5,  "String interpolation and raw string literals",          Section05_Strings.Run),
        new(6,  "Collection expressions and spread",                     Section06_CollectionExpressions.Run),
        new(7,  "Primary constructors (class / struct / record)",        Section07_PrimaryConstructors.Run),
        new(8,  "Small sugars: file-scoped namespace, global using, target-typed new, using declaration", Section08_SmallSyntax.Run),
        new(9,  "C# 14: field keyword and null-conditional assignment",  Section09_Csharp14.Run),
        new(10, "The SAME model, two styles: C# 7 style vs C# 14 style", Section10_SideBySide.Run),
    ];
}
