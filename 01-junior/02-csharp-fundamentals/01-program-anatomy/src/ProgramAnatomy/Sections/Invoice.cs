namespace Junior.CSharpFundamentals.ProgramAnatomy;

// 1/2: ELLE yazılan kısım
public partial class Invoice(string number, decimal net)
{
    public string Number { get; } = number;
    public decimal Net { get; } = net;

    public string Describe()
    {
        OnDescribing();                                       // partial metot çağrısı (uygulaması başka dosyada)
        return $"Fatura {Number}: net {Net:N2}, KDV dahil {Gross:N2}";
    }

    // Partial metot bildirimi (C# 9+: erişim belirleyicili partial metotlar için UYGULAMA ZORUNLUDUR)
    partial void OnDescribing();
}
