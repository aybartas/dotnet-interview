// Code that DELIBERATELY produces warnings. Find the code of each warning (CS86xx) in the IDE / build output and explain WHY it appears,
// then FIX it. Goal: 0 warnings.
//
// Nullable reference types (<Nullable>enable</Nullable>):
//   string   → must never be null (the compiler tracks this)
//   string?  → may be null (you must check before using it)
// These are compile-time hints; they add NO protection at run time. But they catch NullReferenceException without running the code.

namespace Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

public class UserService
{
    // CS8618: Non-nullable 'Name' may be null on constructor exit → either initialize it, make it 'required', or make it string?.
    public string Name { get; set; }

    private readonly Dictionary<int, string> _users = new() { [1] = "alice" };

    // CS8603: Possible null reference return. If TryGetValue fails, 'name' is null.
    public string GetUserName(int id)
    {
        _users.TryGetValue(id, out var name);
        return name;
    }

    // CS8600: Converting a null literal to a non-nullable type.
    public string FirstOrDefaultName()
    {
        string name = null;
        return name ?? "unknown";
    }

    // CS8602: Dereference of a possibly null reference. 'Find' may return null (string?).
    public int NameLength(int id)
    {
        var name = Find(id);
        return name.Length;
    }

    // CS8604: Possible null reference argument. 'Find' returns string? but Print expects string.
    public void ShowUser(int id)
    {
        Print(Find(id));
    }

    private string? Find(int id) => _users.GetValueOrDefault(id);

    private static void Print(string text) => Console.WriteLine(text);
}
