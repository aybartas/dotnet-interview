namespace Junior.CSharpFundamentals.ProgramAnatomy;

// 1/2: the HAND-WRITTEN part
public partial class Invoice(string number, decimal net)
{
    public string Number { get; } = number;
    public decimal Net { get; } = net;

    public string Describe()
    {
        OnDescribing();                                       // partial method call (implementation is in another file)
        return $"Invoice {Number}: net {Net:N2}, gross (incl. VAT) {Gross:N2}";
    }

    // Partial method declaration (C# 9+: for partial methods with an access modifier the IMPLEMENTATION IS REQUIRED)
    partial void OnDescribing();
}
