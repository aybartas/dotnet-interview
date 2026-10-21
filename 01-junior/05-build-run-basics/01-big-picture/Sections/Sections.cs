using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BigPicture;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "C# kodu nasıl .exe oluyor? ⭐ — .dll, apphost, runtime",       Section01_DllAndApphost.Run),
        new(2, "🛠 IL'e bakmak: metotlar yığın makinesi komutlarıdır",         Section02_ReadingIl.Run),
        new(3, "JIT: IL, metodun ilk çağrısında native koda çevrilir",         Section03_Jit.Run),
        new(4, "Debug vs Release farkı",                                       Section04_DebugVsRelease.Run),
        new(5, "Compile-time error vs runtime exception",                      Section05_CompileVsRuntime.Run),
        new(6, "🛠 foreach ve using'in 'lowered' hali (sharplab.io mantığı)",  Section06_Lowering.Run),
    ];
}
