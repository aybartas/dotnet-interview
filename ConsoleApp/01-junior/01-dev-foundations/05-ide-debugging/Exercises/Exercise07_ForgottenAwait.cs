namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise07_ForgottenAwait : Exercise
{
    public override int Number => 7;
    public override string Title => "The forgotten await";
    public override string Skill => "Async flow in the debugger: the Tasks window, how F10 behaves at an 'await'";
    public override string Hint => "When execution reaches the line after the SaveAsync call, what is the Task's state? Add 'task.Status' to Watch. (Why is there no compiler warning? Assigning the result to a variable silences CS4014.)";

    public override void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private static async Task RunAsync()
    {
        var repo = new SlowRepository();

        var task = repo.SaveAsync("record-1");        // ← no await!

        Report("number of saved records", expected: 1, actual: repo.Count);
        await task;                                   // awaited later, but the check has already been made
        Console.WriteLine($"  (Count after awaiting = {repo.Count})");
    }

    private sealed class SlowRepository
    {
        public int Count { get; private set; }

        public async Task SaveAsync(string item)
        {
            await Task.Delay(100);                   // simulates database latency
            Count++;
        }
    }
}
