using System.Runtime.CompilerServices;
using Junior.CollectionsLinq.DelegatesLambdasEvents.Models;

namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section05_SubscriptionLeak
{
    public static void Run()
    {
        Console.WriteLine("The publisher holds a REFERENCE to the subscriber (event = a delegate chain; each element's Target is the subscriber object).");
        Console.WriteLine("If the publisher lives long and the subscriber does NOT unsubscribe, the subscriber (even if it is 'garbage') cannot be collected by the GC → MEMORY LEAK.\n");

        var publisher = new LongLivedPublisher();

        var (weakLeaked, weakFixed) = CreateSubscribers(publisher);

        Console.WriteLine($"   subscribers created: publisher's subscriber count = {publisher.SubscriberCount}");
        ForceGc();
        Console.WriteLine($"   references dropped, GC ran:");
        Console.WriteLine($"     • subscriber that did NOT unsubscribe (leak)  still alive? {weakLeaked.IsAlive}   ← the publisher holds it → LEAK");
        Console.WriteLine($"     • subscriber that left via Dispose            still alive? {weakFixed.IsAlive}");
        Console.WriteLine($"   publisher's subscriber count = {publisher.SubscriberCount} (the leaked subscriber stayed in the list; it keeps running on every Tick!)");

        publisher.RaiseTick();
        GC.KeepAlive(publisher);

        Console.WriteLine("""

        Solutions:
          • The subscriber leaves with `-=` when done (in IDisposable.Dispose; guaranteed with using).
          • If the publisher and subscriber have the SAME lifetime there is no problem (e.g. a form and its own button). The problem: a long-lived publisher (static event, singleton service) + a short-lived subscriber (page, view-model, request-scoped object).
          • Weak event patterns (WeakReference / WeakEventManager) do not tie the publisher to the subscriber's lifetime.
          • If you subscribe with a lambda you need the SAME delegate object for -= (keep the lambda in a variable; a new lambda is a different object).
          • static events are the riskiest: they live until the application shuts down. Generally avoid them.
        Symptom: memory grows over time, handlers of closed screens still run ("ghost" behavior). Diagnosis: a memory profiler (dotMemory, dotnet-gcdump) → the "publisher → subscriber" retention path. Deep dive → M1.6, S2.5.
        """);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (WeakReference leaked, WeakReference fixedOne) CreateSubscribers(LongLivedPublisher publisher)
    {
        var leaked = new ShortLivedSubscriber(publisher);            // we do NOT Dispose
        var fixedOne = new ShortLivedSubscriber(publisher);
        fixedOne.Dispose();                                          // did -=
        return (new WeakReference(leaked), new WeakReference(fixedOne));
    }

    private static void ForceGc()
    {
        for (var i = 0; i < 3; i++) { GC.Collect(); GC.WaitForPendingFinalizers(); }
    }
}
