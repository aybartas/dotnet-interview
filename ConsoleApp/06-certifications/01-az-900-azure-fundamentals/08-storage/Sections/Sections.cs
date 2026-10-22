using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "Storage account ve servisler: Blob, Files, Queue, Table, Disks", Section01_StorageServices.Run),
        new(2, "Redundancy: LRS, ZRS, GRS, GZRS, RA-* — hangi arızaya dayanır? ⭐", Section02_Redundancy.Run),
        new(3, "Blob access tier'ları: Hot, Cool, Cold, Archive ⭐",            Section03_AccessTiers.Run),
        new(4, "Taşıma ve migration aracı seçimi: AzCopy, Storage Explorer, File Sync, Migrate, Data Box", Section04_MovingData.Run),
    ];
}
