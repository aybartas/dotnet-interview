using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Section01_StorageServices
{
    public static void Run()
    {
        Console.WriteLine("A storage account = a unique namespace + one endpoint per service:\n");
        Table.Print(["Service", "What it stores", "Endpoint", "Typical use"],
        [
            ["Blob", "Objects: unstructured data", "https://<name>.blob.core.windows.net", "Images, video, backups, logs, static website"],
            ["Azure Files", "SMB/NFS file share", "https://<name>.file.core.windows.net", "Shared folder, lift-and-shift application"],
            ["Queue", "Simple message queue (message ≤ 64 KB)", "https://<name>.queue.core.windows.net", "Asynchronous work between components"],
            ["Table", "NoSQL key-value (schemaless)", "https://<name>.table.core.windows.net", "Simple structured data, metadata"],
            ["Disks", "Block storage attached to a VM (managed disk)", "— (resource attached to a VM)", "VM operating system and data disks"],
        ]);

        Console.WriteLine("\nStorage account types:\n");
        Table.Print(["Type", "Supports", "Redundancy", "When?"],
        [
            ["Standard general-purpose v2", "Blob, Files, Queue, Table", "LRS, ZRS, GRS, RA-GRS, GZRS, RA-GZRS", "Most scenarios (the default choice)"],
            ["Premium block blobs", "Block/append blob", "LRS, ZRS", "Low latency, high transaction rate"],
            ["Premium file shares", "Azure Files", "LRS, ZRS", "Enterprise, high-performance file share"],
            ["Premium page blobs", "Page blob", "LRS, ZRS", "High-performance page blob scenarios"],
        ]);

        Console.WriteLine("""

        Rules:
          • A storage account name is unique ACROSS ALL OF AZURE (it is part of the URL): 3-24 characters, lowercase letters and digits only.
          • Blob hierarchy: storage account → container → blob. Blob types: block (files), append (logs), page (VM disks).
          • Premium options are SSD-based and do not offer geo (GRS/GZRS) redundancy.
        """);
    }
}
