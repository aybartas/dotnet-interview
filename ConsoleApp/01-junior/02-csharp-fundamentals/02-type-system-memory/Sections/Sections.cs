namespace Junior.CSharpFundamentals.TypeSystemMemory;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Unified type system: everything derives from object", Section01_UnifiedTypeSystem.Run),
        new(2,  "Value type vs reference type: copy semantics",  Section02_ValueVsReference.Run),
        new(3,  "struct / class / record / record struct",       Section03_StructClassRecord.Run),
        new(4,  "Mutable struct pitfalls",                       Section04_MutableStruct.Run),
        new(5,  "Boxing / unboxing and its cost",                Section05_Boxing.Run),
        new(6,  "string: immutability, interning, culture (Turkish I)", Section06_Strings.Run),
        new(7,  "Nullable value types and nullable reference types", Section07_Nullable.Run),
        new(8,  "var vs dynamic vs object",                      Section08_VarDynamicObject.Run),
        new(9,  "const vs readonly vs static readonly",          Section09_ConstReadonly.Run),
        new(10, "ref, out, in parameters",                       Section10_RefOutIn.Run),
        new(11, "Tuple (ValueTuple) and deconstruction",         Section11_Tuples.Run),
        new(12, "enum, [Flags], Enum.TryParse",                  Section12_Enums.Run),
    ];
}
