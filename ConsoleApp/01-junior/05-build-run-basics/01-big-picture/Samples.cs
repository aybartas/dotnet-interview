using System.Runtime.CompilerServices;

namespace Junior.BuildRunBasics.BigPicture;

/// <summary>IL'ine bakılacak küçük örnek metotlar (Section02, Section04, Section06). Gövdeleri bilerek basit tutuldu.</summary>
public static class Samples
{
    public static int Add(int a, int b) => a + b;

    public static int AddWithLocal(int a, int b)
    {
        var result = a + b;
        return result;
    }

    public static int SumArray(int[] items)
    {
        var sum = 0;
        foreach (var item in items) sum += item;
        return sum;
    }

    public static int SumList(List<int> items)
    {
        var sum = 0;
        foreach (var item in items) sum += item;
        return sum;
    }

    public static string ReadFirstLine(string path)
    {
        using var reader = new StreamReader(path);
        return reader.ReadLine() ?? "";
    }

    public static string Greet(string name, int count) => $"Merhaba {name}, {count}";

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int JitMeOnce(int x) => x * x + 1;
}
