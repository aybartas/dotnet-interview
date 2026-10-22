using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Storage account and services: Blob, Files, Queue, Table, Disks", Section01_StorageServices.Run),
        new(2, "Redundancy: LRS, ZRS, GRS, GZRS, RA-* — which failure does it survive? ⭐", Section02_Redundancy.Run),
        new(3, "Blob access tiers: Hot, Cool, Cold, Archive ⭐",                 Section03_AccessTiers.Run),
        new(4, "Choosing a transfer and migration tool: AzCopy, Storage Explorer, File Sync, Migrate, Data Box", Section04_MovingData.Run),
    ];
}
