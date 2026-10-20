namespace Junior.CSharpFundamentals.ProgramAnatomy;

// 2/2: "ÜRETİLMİŞ" kısım — gerçekte bu dosya bir kod üretici (ORM/designer/source generator) tarafından yazılırdı
// ve elle düzenlenmemelidir. Partial sayesinde elle yazılan kod, üretilen koddan ayrı dosyada güvenle yaşar.
public partial class Invoice
{
    private const decimal VatRate = 0.20m;

    public decimal Gross => Math.Round(Net * (1 + VatRate), 2);

    partial void OnDescribing() => Console.WriteLine($"[generated] {Number} için açıklama hazırlanıyor");
}
