namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise08_SharedReference : Exercise
{
    public override int Number => 8;
    public override string Title => "Two 'different' users sharing the same object";
    public override string Skill => "Make Object ID: proving that two variables point to the same reference";
    public override string Hint => "In the Watch window, 'aliceGuest' and 'bobGuest' → right-click → Make Object ID. If both are $1, they are the same object. Where is the list created?";

    public override void Run()
    {
        var aliceGuest = Permissions.For("guest");
        var bobGuest = Permissions.For("guest");

        aliceGuest.Add("write");                         // we only meant to give Alice write permission

        Report("Bob's permissions", expected: "read", actual: string.Join(",", bobGuest));
    }

    private static class Permissions
    {
        private static readonly List<string> GuestDefaults = ["read"];

        public static List<string> For(string role) => role switch
        {
            "admin" => ["read", "write", "delete"],     // a NEW list on every call
            _ => GuestDefaults,                         // the SAME list on every call
        };
    }
}
