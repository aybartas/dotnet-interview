namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Delegate, Func / Action / Predicate, method group, multicast", Section01_Delegates.Run),
        new(2, "Lambda expression: expression vs statement lambda",          Section02_Lambdas.Run),
        new(3, "Closure ve captured variable — döngüde closure tuzağı ⭐",   Section03_Closures.Run),
        new(4, "event: neden çıplak delegate değil? EventHandler<T>",        Section04_Events.Run),
        new(5, "Abonelik sızıntısı: unutulan abonelik memory leak",          Section05_SubscriptionLeak.Run),
        new(6, "Local function vs lambda",                                   Section06_LocalFunctions.Run),
        new(7, "Lambda ile strateji: indirim motoru",                        Section07_DiscountEngine.Run),
    ];
}
