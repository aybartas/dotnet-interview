using Junior.AsyncBasics.Shared;

namespace Junior.AsyncBasics.CancellationBasics;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "CancellationTokenSource / CancellationToken: iptal KOOPERATİFTİR ⭐", Section01_CooperativeModel.Run),
        new(2, "Konvansiyonlar: token son parametre, OperationCanceledException, CancelAfter", Section02_Conventions.Run),
        new(3, "🛠 İptal edilebilir rapor endpoint'i (istemci kopunca iş durur)",      Section03_CancellableEndpoint.Run),
        new(4, "Token'ı aşağı geçirmeyi unutmak",                                     Section04_ForgottenToken.Run),
    ];
}
