using Junior.CSharpFundamentals.ExceptionHandling.Domain;

namespace Junior.CSharpFundamentals.ExceptionHandling;

public static class Section02_CustomExceptions
{
    public static void Run()
    {
        Console.WriteLine("Standart hiyerarşi (kısaltılmış):");
        Console.WriteLine("   Exception");
        Console.WriteLine("   ├─ SystemException ── NullReferenceException, InvalidCastException, IndexOutOfRangeException, OutOfMemoryException…");
        Console.WriteLine("   │   └─ ArgumentException ── ArgumentNullException, ArgumentOutOfRangeException");
        Console.WriteLine("   ├─ InvalidOperationException ── ObjectDisposedException");
        Console.WriteLine("   ├─ NotSupportedException, NotImplementedException");
        Console.WriteLine("   ├─ IOException ── FileNotFoundException, DirectoryNotFoundException");
        Console.WriteLine("   └─ (kendi domain exception'larınız: DomainException → InsufficientFundsException, OrderNotFoundException …)");

        Console.WriteLine("\nHazır tipi doğru seç:");
        Console.WriteLine("   ArgumentNullException       → argüman null (ThrowIfNull)      ArgumentOutOfRangeException → değer aralık dışı");
        Console.WriteLine("   InvalidOperationException   → nesne BU durumda bu işlemi yapamaz   NotSupportedException        → işlem hiç desteklenmiyor");

        Console.WriteLine("\nCustom domain exception: ek VERİ taşır, çağıran tipe göre karar verir:");
        try { Withdraw("ACC-1", balance: 100m, amount: 250m); }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"   catch InsufficientFundsException → kullanıcıya: 'Eksik tutar: {ex.Shortfall:N2}'  (Code={ex.Code})  ← mesajı ayrıştırmaya gerek yok");
        }

        Console.WriteLine("\nKök tip sayesinde üst katman TEK catch ile tüm iş kuralı hatalarını ayırt eder:");
        foreach (var action in new Action[]
                 {
                     () => throw new OrderNotFoundException(42),
                     () => throw new ValidationFailedException(new Dictionary<string, string> { ["email"] = "geçersiz", ["yas"] = "negatif" }),
                     () => throw new NullReferenceException("beklenmeyen"),
                 })
        {
            try { action(); }
            catch (DomainException ex) { Console.WriteLine($"   [{ex.Code}] → 4xx'e çevrilir: {ex.Message}"); }
            catch (Exception ex) { Console.WriteLine($"   [BEKLENMEYEN] → 500'e çevrilir ve LOGLANIR: {ex.GetType().Name}"); }
        }

        Console.WriteLine("\nAltyapı hatasını çevirirken InnerException'ı KORU:");
        try { LoadOrder(1); }
        catch (OrderRepositoryException ex) { Console.WriteLine($"   {ex.Message}  ← iç neden: {ex.InnerException!.GetType().Name}: {ex.InnerException.Message}"); }

        Console.WriteLine("""

        Çok sayıda custom exception tipi üretmeyin: çağıranın AYRI davranacağı durumlar için tip, gerisi için hazır tipler.
        Hangi katman neyi fırlatır?  Domain → DomainException'lar · Altyapı (repository) → kendi tipini sarar · Sınır (API/middleware) → HTTP koduna çevirir.
        """);
    }

    private static void Withdraw(string accountId, decimal balance, decimal amount)
    {
        if (amount > balance) throw new InsufficientFundsException(accountId, balance, amount);
    }

    private static void LoadOrder(int id)
    {
        try { throw new TimeoutException("veritabanı zaman aşımı"); }
        catch (TimeoutException ex)
        {
            throw new OrderRepositoryException($"Sipariş {id} yüklenemedi", ex);       // sarmala, orijinali inner olarak sakla
        }
    }
}
