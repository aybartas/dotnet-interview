using Junior.BuildRunBasics.Shared;

namespace Junior.BuildRunBasics.BigPicture;

public static class Sections
{
    public static IReadOnlyList<Section> All { get; } =
    [
        new(1, "How does C# code become an .exe? ⭐ — .dll, apphost, runtime", Section01_DllAndApphost.Run),
        new(2, "🛠 Reading IL: methods are stack-machine instructions",         Section02_ReadingIl.Run),
        new(3, "JIT: IL is compiled to native code on a method's first call",  Section03_Jit.Run),
        new(4, "Debug vs Release",                                             Section04_DebugVsRelease.Run),
        new(5, "Compile-time error vs runtime exception",                      Section05_CompileVsRuntime.Run),
        new(6, "🛠 The 'lowered' form of foreach and using (sharplab.io idea)", Section06_Lowering.Run),
    ];
}
