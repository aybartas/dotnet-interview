namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public sealed record Section(int Number, string Title, Action Run);

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Delegate, Func / Action / Predicate, method group, multicast", Section01_Delegates.Run),
        new(2, "Lambda expression: expression vs statement lambda",          Section02_Lambdas.Run),
        new(3, "Closure and captured variable — the closure trap in loops ⭐", Section03_Closures.Run),
        new(4, "event: why not a bare delegate? EventHandler<T>",            Section04_Events.Run),
        new(5, "Subscription leak: a forgotten subscription is a memory leak", Section05_SubscriptionLeak.Run),
        new(6, "Local function vs lambda",                                   Section06_LocalFunctions.Run),
        new(7, "Strategy with lambdas: a discount engine",                   Section07_DiscountEngine.Run),
    ];
}
