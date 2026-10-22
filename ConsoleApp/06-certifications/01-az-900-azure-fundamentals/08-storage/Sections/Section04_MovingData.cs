using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.Storage;

public static class Section04_MovingData
{
    public static void Run()
    {
        Table.Print(["Need (phrase in the scenario)", "Tool", "How?"],
        [
            ["\"Copy blobs/files from the command line, scripted\"", "AzCopy", "azcopy copy / sync; copy between accounts and clouds"],
            ["\"Manage blobs/files/queues with a graphical interface\"", "Azure Storage Explorer", "Desktop application (AzCopy under the hood)"],
            ["\"On-prem Windows file server synced with Azure Files, frequent files kept locally\"", "Azure File Sync", "Sync agent + cloud tiering"],
            ["\"Managed SMB/NFS share in the cloud\"", "Azure Files", "Mount the share (net use / mount)"],
            ["\"Discover servers, size them, estimate cost, migrate\"", "Azure Migrate", "Central hub: discovery & assessment, migration"],
            ["\"Terabytes of data, network slow/expensive/absent\"", "Azure Data Box", "Microsoft ships a physical device, you load the data and ship it back"],
        ]);

        Console.WriteLine("""

        Over the network or by device? Rough math: 40 TB over a 100 Mbps line ≈ 40e12 × 8 / 100e6 s ≈ 37 days (even if the whole line is dedicated to this).
        For such a transfer Data Box is much faster with a few days of shipping; Data Box also supports export (Azure → on-prem).

        Commonly confused pairs:
          • AzCopy (command line, one-off/scripted copy) ↔ Storage Explorer (graphical tool) ↔ File Sync (continuous, two-way sync).
          • Azure Migrate (migration PLATFORM: discovery + assessment + migration) ↔ Data Box (a device for PHYSICALLY moving data).
        """);
    }
}
