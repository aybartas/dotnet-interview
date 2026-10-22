using Certification.AzureFundamentals.CloudBenefits;
using Certification.AzureFundamentals.CloudComputing;
using Certification.AzureFundamentals.CloudServiceTypes;
using Certification.AzureFundamentals.CostManagement;
using Certification.AzureFundamentals.Storage;
using Junior.AsyncBasics.AsyncAwait;
using Junior.AsyncBasics.CancellationBasics;
using Junior.AsyncBasics.Concepts;
using Junior.BuildRunBasics.BigPicture;
using Junior.BuildRunBasics.BinObj;
using Junior.BuildRunBasics.RunBuildPublish;
using Junior.CollectionsLinq.CollectionsGenerics;
using Junior.CollectionsLinq.DelegatesLambdasEvents;
using Junior.CollectionsLinq.LinqBasics;
using Junior.CSharpFundamentals.ClassMembers;
using Junior.CSharpFundamentals.ExceptionHandling;
using Junior.CSharpFundamentals.ModernCSharp;
using Junior.CSharpFundamentals.Oop;
using Junior.CSharpFundamentals.ProgramAnatomy;
using Junior.CSharpFundamentals.TypeSystemMemory;
using Junior.DevFoundations.IdeDebugging.Exercises;
using Junior.DevFoundations.NetworkFundamentals;

// Every console topic runs from this single file. In Visual Studio press F5 → the topic menu opens; pick a topic,
// then type a section number, "all", or a topic-specific command.
// This file ITSELF is a "top-level statements" example (see J2.1 Section01): we did not write a class or Main; the compiler generated them.
// `args` are the command-line arguments, `return <int>` is the process exit code, and using `await` makes Main `async Task<int>` (see J4).
//
// J = Junior, C = Certification. Topics that need an HTTP server live in the WebApp project.
// Without the menu (terminal):  dotnet run -- J3.2 4   ·   dotnet run -- C1.2 serial 99.95 99.99

Topic[] topics =
[
    // ── Junior ──
    new("J1.2", "Network Fundamentals (TCP / UDP / Sockets)",              NetworkFundamentalsTopic.Run),
    new("J1.5", "IDE, Debugging & Code Quality",                           IdeDebuggingTopic.Run),
    new("J2.1", "Program Anatomy",                                         ProgramAnatomyTopic.Run),
    new("J2.2", "Type System & Memory Model",                              TypeSystemMemoryTopic.Run),
    new("J2.3", "OOP Fundamentals",                                        OopTopic.Run),
    new("J2.4", "Class Members & Object Semantics",                        ClassMembersTopic.Run),
    new("J2.5", "Exception Handling",                                      ExceptionHandlingTopic.Run),
    new("J2.6", "Everyday Modern C#",                                      ModernCSharpTopic.Run),
    new("J3.1", "Collections & Generics",                                  CollectionsGenericsTopic.Run),
    new("J3.2", "Delegates, Lambdas & Events",                             DelegatesLambdasEventsTopic.Run),
    new("J3.3", "LINQ Basics",                                             LinqBasicsTopic.Run),
    new("J4.1", "Core Concepts (concurrency, parallelism, I/O vs CPU)",    ConceptsTopic.Run),
    new("J4.2", "async/await in Practice",                                 AsyncAwaitTopic.Run),
    new("J4.3", "Cancellation Basics",                                     CancellationBasicsTopic.Run),
    new("J5.1", "C# → IL → Native: The Big Picture",                       BigPictureTopic.Run),
    new("J5.2", "bin/ and obj/",                                           BinObjTopic.Run),
    new("J5.3", "run vs build vs publish",                                 RunBuildPublishTopic.Run),

    // ── Certification: AZ-900 (only topics with code are listed; the others are README + quiz only) ──
    new("C1.1",  "Cloud Computing, Cloud Models and Pricing",              CloudComputingTopic.Run),
    new("C1.2",  "Benefits of the Cloud (HA, scale, SLA…)",                CloudBenefitsTopic.Run),
    new("C1.3",  "Cloud Service Types: IaaS, PaaS, SaaS, Serverless",      CloudServiceTypesTopic.Run),
    new("C1.8",  "Azure Storage: Blob, Files, Data Transfer and Migration", StorageTopic.Run),
    new("C1.11", "Cost Management in Azure",                               CostManagementTopic.Run),
];

Topic? Find(string id) => topics.FirstOrDefault(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

void PrintTopics()
{
    Console.WriteLine("\nTopics:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id,-5} {t.Title}");
    Console.WriteLine("\n  (README-only topics, no code: J1.1 Git, J1.4 .NET CLI, C1.4–C1.7, C1.9–C1.10, C1.12–C1.15)");
}

// Without the menu: dotnet run -- J3.2 4
if (args.Length > 0)
{
    if (Find(args[0]) is not { } direct) { PrintTopics(); return 1; }
    return await direct.Run(args[1..]);
}

// Menu: topic → (section number | all | topic-specific command) → repeat
Console.WriteLine("Note: demos about static initialization, JIT and first calls only look right the first time the process runs; restart the program for a clean result.");
while (true)
{
    PrintTopics();
    Console.Write("\nTopic (e.g. J3.2; empty = quit): ");
    var id = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(id)) return 0;
    if (Find(id) is not { } topic) { Console.WriteLine($"Unknown topic: {id}"); continue; }

    await topic.Run([]);                             // prints the section / command list
    while (true)
    {
        Console.Write($"\n{topic.Id} › section number, all, or a command (empty = back to topics): ");
        var line = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(line)) break;
        await topic.Run(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }
}

/// <summary>A topic: Id (e.g. "J3.2"), title and entry point (receives the remaining arguments, returns an exit code). A second constructor accepts synchronous topics.</summary>
public sealed record Topic(string Id, string Title, Func<string[], Task<int>> Run)
{
    public Topic(string id, string title, Func<string[], int> run) : this(id, title, args => Task.FromResult(run(args))) { }
}
