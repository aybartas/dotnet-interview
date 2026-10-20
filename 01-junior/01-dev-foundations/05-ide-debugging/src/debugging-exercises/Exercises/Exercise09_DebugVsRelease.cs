using System.Diagnostics;

namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise09_DebugVsRelease : Exercise
{
    public override int Number => 9;
    public override string Title => "Debug'da doğru, Release'de farklı sonuç";
    public override string Skill => "Yapılandırmalar (Debug vs Release), [Conditional], '#if DEBUG', optimizasyon etkisi";
    public override string Hint => "Aynı egzersizi 'dotnet run -- 9' ve 'dotnet run -c Release -- 9' ile çalıştırın. Debug.Assert içindeki ifadenin yan etkisi Release'de ne oluyor?";

    private static int _nextId;

    public override void Run()
    {
        _nextId = 0;

        Debug.Assert(NextId() > 0, "Id pozitif olmalı");   // Debug.Assert çağrısı Release derlemesinde TAMAMEN silinir
        var id = NextId();

        Report("ikinci NextId() değeri", expected: 2, actual: id);
#if DEBUG
        Console.WriteLine("  (Derleme: DEBUG — Debug.Assert ve içindeki ifade çalıştı)");
#else
        Console.WriteLine("  (Derleme: RELEASE — Debug.Assert çağrısı yok edildi)");
#endif
    }

    private static int NextId() => ++_nextId;
}
