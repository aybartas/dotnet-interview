using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Junior.CSharpFundamentals.ExceptionHandling.Domain;

/// <summary>
/// `throw;` ile `throw ex;` farkını gösteren minimal zincir:  Entry → Middle → ThrowSite (hatanın GERÇEK fırlatıldığı yer).
/// [MethodImpl(NoInlining)]: JIT metotları satır içine alırsa stack trace'te kaybolurlar; deneyin doğru olması için kapatılır.
/// </summary>
public static class Rethrow
{
    public enum Mode { ThrowKeyword, ThrowEx, Wrap, DispatchInfo }

    public static Exception Capture(Mode mode)
    {
        try { Entry(mode); }
        catch (Exception ex) { return ex; }
        throw new InvalidOperationException("beklenen exception fırlatılmadı");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Entry(Mode mode)
    {
        try
        {
            Middle();
        }
        catch (Exception ex)
        {
            switch (mode)
            {
                case Mode.ThrowKeyword:
                    throw;                                              // ✓ stack trace KORUNUR (özgün fırlatma noktası kalır)
                case Mode.ThrowEx:
#pragma warning disable CA2200 // BİLEREK: analyzer CA2200 tam olarak bu hatayı yakalar (derleyici uyarısı: 'Re-throwing caught exception changes stack information')
                    throw ex;                                           // ✗ stack trace burada SIFIRLANIR; hata sanki Entry'de doğmuş gibi görünür
#pragma warning restore CA2200
                case Mode.Wrap:
                    throw new InvalidOperationException("Entry sarmaladı", ex);   // ✓ yeni exception + orijinali InnerException olarak sakla
                case Mode.DispatchInfo:
                    ExceptionDispatchInfo.Capture(ex).Throw();          // ✓ orijinal stack'i yakalayıp BAŞKA YERDEN (ör. başka thread) yeniden fırlatma
                    break;
            }
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Middle() => ThrowSite();

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowSite() => throw new InvalidOperationException("özgün hata");
}
