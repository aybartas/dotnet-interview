// BİLEREK stil/analyzer uyarıları ve BİÇİM hataları içeren dosya.
//   dotnet build                           → IDE0005, IDE0044, IDE0051, IDE1006, CA1861 uyarıları (CA1822 yalnızca IDE'de önerilir)
//   dotnet format --verify-no-changes      → biçim farkları varsa hata verir (CI kapısı olarak kullanılır)
//   dotnet format                          → biçim sorunlarını düzeltir (DOSYAYI DEĞİŞTİRİR; git diff ile görün)
//   dotnet format analyzers --severity warn → analyzer düzeltmelerini uygular
//
// Düzeltince "Bilerek" olan her şeyi temizlemiş olursunuz. Orijinal hâle dönmek için: git checkout -- StyleViolations.cs

using System.Text;          // IDE0005: kullanılmayan using
using System.Net.Http;      // IDE0005 (ImplicitUsings zaten ekliyor → gereksiz)

namespace Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

public class StyleViolations
{
private int counter = 0;                       // IDE0044 (readonly olabilir) + IDE1006 (_camelCase beklenir) + IDE0051 (kullanılmıyor) + girinti yanlış
    private string unusedField = "x";          // IDE0051 + IDE1006

    public bool IsAllowed(string role)
    {
        // CA1861: her çağrıda yeni dizi allocate edilir → static readonly alana çıkarın
        return Array.IndexOf(new[] { "admin", "editor" }, role) >= 0;
    }

    // CA1822: instance verisi kullanmıyor → 'static' olabilir
    public int Double(int x)
    {
        if(x>10)               // biçim: 'if (' ve operatör boşlukları eksik
        {return x*2;}          // biçim: süslü parantez yeri/boşluklar
        else
        {
            return x   *   2;
        }
    }
}
