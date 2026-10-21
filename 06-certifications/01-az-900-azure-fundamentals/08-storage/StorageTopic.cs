using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class StorageTopic
{
    public static int Run(string[] args)
    {
        var code = SectionRunner.Run("1.8", "Azure Storage: Blob, Files, Taşıma ve Migration", Sections.All, args,
            "4. bölüm Azurite emülatörü ister: modül klasöründe docker compose up -d  (gerçek hesap için AZURE_STORAGE_CONNECTION_STRING)");
        return code != 0 ? code : Section04_BlobDemo.Failed ? 2 : 0;
    }
}
