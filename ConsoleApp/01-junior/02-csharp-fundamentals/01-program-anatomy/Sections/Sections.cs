namespace Junior.CSharpFundamentals.ProgramAnatomy;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1,  "Entry point, top-level statements, exit code", Section01_EntryPoint.Run),
        new(2,  "Namespace ve using türleri",                   Section02_UsingsAndNamespaces.Run),
        new(3,  "static class, static üye, static constructor", Section03_Statics.Run),
        new(4,  "partial class / partial method",               Section04_Partial.Run),
        new(5,  "Statement vs expression, expression-bodied",   Section05_StatementsExpressions.Run),
        new(6,  "Scope, gölgeleme, definite assignment",        Section06_Scope.Run),
        new(7,  "Tip dönüşümleri: cast, is, as, Convert, Parse",Section07_Conversions.Run),
        new(8,  "checked / unchecked ve overflow",              Section08_CheckedUnchecked.Run),
        new(9,  "Operatörler: öncelik ve short-circuit",        Section09_Operators.Run),
        new(10, "nameof, typeof, sizeof, default",              Section10_NameofTypeof.Run),
        new(11, "Preprocessor yönergeleri",                     Section11_Preprocessor.Run),
        new(12, "XML documentation comment",                    Section12_XmlDocs.Run),
        new(13, "CallerMemberName / CallerArgumentExpression",  Section13_CallerInfo.Run),
    ];
}
