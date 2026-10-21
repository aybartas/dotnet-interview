using System.Runtime.CompilerServices;
using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section05_SubscriptionLeak
{
    public static void Run()
    {
        Console.WriteLine("Yayıncı, aboneye bir REFERANS tutar (event = delegate zinciri; her elemanın Target'ı abone nesnesi).");
        Console.WriteLine("Yayıncı uzun yaşıyor ve abone abonelikten ÇIKMAZSA, abone ('çöp' olsa bile) GC tarafından toplanamaz → MEMORY LEAK.\n");

        var publisher = new LongLivedPublisher();

        var (weakLeaked, weakFixed) = CreateSubscribers(publisher);

        Console.WriteLine($"   abone oluşturuldu: yayıncının abone sayısı = {publisher.SubscriberCount}");
        ForceGc();
        Console.WriteLine($"   referanslar bırakıldı, GC çalıştı:");
        Console.WriteLine($"     • aboneliğinden ÇIKMAYAN abone (leak)  hâlâ yaşıyor mu? {weakLeaked.IsAlive}   ← yayıncı tutuyor → SIZINTI");
        Console.WriteLine($"     • Dispose ile ayrılan abone            hâlâ yaşıyor mu? {weakFixed.IsAlive}");
        Console.WriteLine($"   yayıncının abone sayısı = {publisher.SubscriberCount} (sızan abone listede kaldı; her Tick'te çalışmaya devam eder!)");

        publisher.RaiseTick();
        GC.KeepAlive(publisher);

        Console.WriteLine("""

        Çözümler:
          • Abone, işi bitince `-=` ile ayrılır (IDisposable.Dispose'ta; using ile garanti).
          • Yayıncı ve abone AYNI ömürdeyse sorun yok (ör. form ve kendi butonu). Sorun: uzun ömürlü yayıncı (static event, singleton servis) + kısa ömürlü abone (sayfa, view-model, istek kapsamlı nesne).
          • Zayıf olay desenleri (WeakReference / WeakEventManager) yayıncıyı abonenin ömrüne bağlamaz.
          • Lambda ile abone olursanız -= için AYNI delegate nesnesine ihtiyacınız var (lambda'yı bir değişkende tutun; yeni bir lambda farklı nesnedir).
          • static event'ler en riskli olanlardır: uygulama kapanana dek yaşar. Genellikle kaçının.
        Belirti: bellek zamanla artar, kapatılmış ekranların handler'ları hâlâ çalışır ("hayalet" davranış). Teşhis: bellek profilleyici (dotMemory, dotnet-gcdump) → "yayıncı → abone" tutma yolu (retention path). Derin hali → M1.6, S2.5.
        """);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference leaked, WeakReference fixedOne) CreateSubscribers(LongLivedPublisher publisher)
    {
        var leaked = new ShortLivedSubscriber(publisher);            // Dispose ETMİYORUZ
        var fixedOne = new ShortLivedSubscriber(publisher);
        fixedOne.Dispose();                                          // -= yaptı
        return (new WeakReference(leaked), new WeakReference(fixedOne));
    }

    private static void ForceGc()
    {
        for (var i = 0; i < 3; i++) { GC.Collect(); GC.WaitForPendingFinalizers(); }
    }
}
