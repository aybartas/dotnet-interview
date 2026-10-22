namespace Junior.CSharpFundamentals.ProgramAnatomy;

// 2/2: the "GENERATED" part — in reality this file would be written by a code generator (ORM/designer/source generator)
// and should not be edited by hand. Thanks to partial, the hand-written code lives safely in a separate file from the generated code.
public partial class Invoice
{
    private const decimal VatRate = 0.20m;

    public decimal Gross => Math.Round(Net * (1 + VatRate), 2);

    partial void OnDescribing() => Console.WriteLine($"[generated] preparing description for {Number}");
}
