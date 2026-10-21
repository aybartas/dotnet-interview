using Junior.CSharpFundamentals.TypeSystemMemory.ConstEmbedding;

namespace Junior.CSharpFundamentals.TypeSystemMemory;

public static class TypeSystemMemoryTopic
{
    public static int Run(string[] args)
    {
        // J2.2 — Tip Sistemi & Bellek Modeli: deneyler.
        //   dotnet run -- 2.2            → bölüm listesi
        //   dotnet run -- 2.2 5       → 5. bölüm (boxing)
        //   dotnet run -- 2.2 all     → hepsi
        //
        // Her deney "ne oluyor?" sorusunu çıktıyla cevaplar. Önce çıktıyı TAHMİN edin, sonra çalıştırın.

        if (args is ["const-embedding"])        // const gömülmesi deneyi (adımlar README'de)
        {
            ConstEmbeddingDemo.Run();
            return 0;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("J2.2 Tip Sistemi & Bellek Modeli — bölümler:\n");
            foreach (var s in Sections.All) Console.WriteLine($"  {s.Number,2}. {s.Title}");
            Console.WriteLine("\nÇalıştırmak için: dotnet run -- 2.2 <numara> | all | const-embedding");
            return 0;
        }

        foreach (var section in args[0] == "all" ? Sections.All : Sections.All.Where(s => s.Number.ToString() == args[0]))
        {
            Console.WriteLine($"\n━━━ {section.Number}. {section.Title} ━━━");
            section.Run();
        }
        return 0;
    }
}
