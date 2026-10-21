namespace Junior.CSharpFundamentals.ModernCSharp;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Records: record, record struct, with, değer eşitliği",  Section01_Records.Run),
        new(2,  "init-only setter ve required members",                  Section02_InitRequired.Run),
        new(3,  "Pattern matching: type, constant, relational, logical, property", Section03_Patterns.Run),
        new(4,  "switch expression",                                     Section04_SwitchExpression.Run),
        new(5,  "String interpolation ve raw string literal'ler",        Section05_Strings.Run),
        new(6,  "Collection expressions ve spread",                      Section06_CollectionExpressions.Run),
        new(7,  "Primary constructor'lar (class / struct / record)",     Section07_PrimaryConstructors.Run),
        new(8,  "Küçük şekerler: file-scoped namespace, global using, target-typed new, using declaration", Section08_SmallSyntax.Run),
        new(9,  "C# 14: field keyword ve null-conditional assignment",    Section09_Csharp14.Run),
        new(10, "AYNI model, iki stil: C# 7 tarzı vs C# 14 tarzı",       Section10_SideBySide.Run),
    ];
}
