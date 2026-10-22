using System.Runtime.CompilerServices;

namespace Junior.BuildRunBasics.BigPicture;

/// <summary>Small sample methods whose IL we inspect (Section02, Section04, Section06). Bodies are kept simple on purpose.</summary>
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

    public static string Greet(string name, int count) => $"Hello {name}, {count}";

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int JitMeOnce(int x) => x * x + 1;
}
