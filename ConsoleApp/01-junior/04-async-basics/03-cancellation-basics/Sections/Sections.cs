using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "CancellationTokenSource / CancellationToken: cancellation is COOPERATIVE ⭐", Section01_CooperativeModel.Run),
        new(2, "Conventions: token as the last parameter, OperationCanceledException, CancelAfter", Section02_Conventions.Run),
        new(3, "🛠 A cancellable report endpoint (the work stops when the client disconnects)", Section03_CancellableEndpoint.Run),
        new(4, "Forgetting to pass the token down",                                         Section04_ForgottenToken.Run),
    ];
}
