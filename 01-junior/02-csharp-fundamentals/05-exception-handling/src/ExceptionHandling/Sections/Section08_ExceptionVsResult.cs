using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section08_ExceptionVsResult
{
    public static void Run()
    {
        Console.WriteLine("Aynı iş kuralı iki biçimde:\n");

        Console.WriteLine("A) Exception: çağıran try/catch yazmak ZORUNDA DEĞİL (unutabilir) ve imza hatayı göstermez");
        try { _ = AccountService.WithdrawOrThrow(balance: 100m, amount: 250m); }
        catch (InsufficientFundsException ex) { Console.WriteLine($"   yakalandı: {ex.Message}"); }

        Console.WriteLine("\nB) Result: imza 'başarısız olabilir' der; çağıran iki durumu da ele almak zorunda (Match)");
        var ok = AccountService.TryWithdraw(100m, 40m);
        var fail = AccountService.TryWithdraw(100m, 250m);
        Console.WriteLine($"   {ok.Match(v => $"kalan bakiye {v:N2}", e => $"hata: {e}")}");
        Console.WriteLine($"   {fail.Match(v => $"kalan bakiye {v:N2}", e => $"hata: {e}")}");

        Console.WriteLine("""

        Hangisi ne zaman?
        ┌────────────────────────────┬──────────────────────────────────┬─────────────────────────────────────────┐
        │                            │ Exception                        │ Result / Try-deseni / null              │
        ├────────────────────────────┼──────────────────────────────────┼─────────────────────────────────────────┤
        │ Hata türü                  │ Beklenmeyen, programcı/ortam hatası│ BEKLENEN iş sonucu (doğrulama, kayıt yok)│
        │ Sık mı gerçekleşir?        │ Nadir                            │ Normal akışın parçası olabilir          │
        │ Performans                 │ Fırlatma pahalı                  │ Ucuz (sıradan dönüş değeri)             │
        │ Çağıranı zorlama           │ Hayır (unutulabilir)             │ Evet (tipten ele alması gerekir)        │
        │ Derin çağrı zincirinde     │ Otomatik yukarı yayılır          │ Her seviyede taşıma gerekir (Map/Bind)  │
        │ Örnek                      │ DB bağlantısı koptu, bug, null   │ Parse, "kullanıcı bulunamadı", yetersiz bakiye│
        └────────────────────────────┴──────────────────────────────────┴─────────────────────────────────────────┘
        Pratik: framework/altyapı ve gerçekten istisnai durumlar → exception; iş kuralı sonuçları → Result ya da Try deseni (int.TryParse).
        İkisi bir arada da kullanılır: domain Result döner, en dış katman (API) gerekirse HTTP koduna çevirir; beklenmeyen hata exception olarak kalır.
        Result pattern, ROP, OneOf/ErrorOr kütüphaneleri ve tartışması → M11.6.
        """);
    }
}
