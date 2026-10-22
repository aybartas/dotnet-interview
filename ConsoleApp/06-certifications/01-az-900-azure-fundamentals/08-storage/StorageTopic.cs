using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class StorageTopic
{
    public static int Run(string[] args) =>
        SectionRunner.Run("1.8", "Azure Storage: Blob, Files, Taşıma ve Migration", Sections.All, args);
}
