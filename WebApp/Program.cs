using Junior.AspNetCoreFundamentals.ConfigurationBasics;
using Junior.AspNetCoreFundamentals.DiBasics;
using Junior.AspNetCoreFundamentals.Environments;
using Junior.AspNetCoreFundamentals.LoggingBasics;
using Junior.AspNetCoreFundamentals.MiddlewareBasics;
using Junior.AspNetCoreFundamentals.ProgramHost;
using Junior.DevFoundations.HowTheWebWorks;

// Topics that need an HTTP server run in this app. ONE topic runs at a time (so topics never disturb each other's
// middleware, routes or configuration): pick it from the menu (F5 in Visual Studio) or pass it on the command line.
// Every topic builds and runs its own host; the arguments after the topic id go straight to WebApplication.CreateBuilder(args),
// so topic-specific switches such as `--https` or `--environment Production` just work. Stop the server with Ctrl+C.
//
//   dotnet run                                    → menu
//   dotnet run -- J6.2                            → run J6.2 on http://localhost:5080
//   dotnet run -- J6.6 --environment Production   → arguments are forwarded to the topic

WebTopic[] topics =
[
    new("J1.3", "How the Web Works (Kestrel)",          HowTheWebWorksTopic.Run),
    new("J6.1", "Program.cs & Host",                    ProgramHostTopic.Run),
    new("J6.2", "Dependency Injection Basics",          DiBasicsTopic.Run),
    new("J6.3", "Configuration & Options Basics",       ConfigurationBasicsTopic.Run),
    new("J6.4", "Middleware Pipeline Basics",           MiddlewareBasicsTopic.Run),
    new("J6.5", "Logging Basics",                       LoggingBasicsTopic.Run),
    new("J6.6", "Environments & Error Pages",           EnvironmentsTopic.Run),
];

WebTopic? Find(string id) => topics.FirstOrDefault(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

void PrintTopics()
{
    Console.WriteLine("\nWeb topics:\n");
    foreach (var t in topics) Console.WriteLine($"  {t.Id,-5} {t.Title}");
}

if (args.Length > 0)
{
    if (Find(args[0]) is not { } direct) { PrintTopics(); return 1; }
    return direct.Run(args[1..]);
}

PrintTopics();
Console.Write("\nTopic (e.g. J6.2; empty = quit): ");
var id = Console.ReadLine()?.Trim();
if (string.IsNullOrEmpty(id)) return 0;
if (Find(id) is not { } topic) { Console.WriteLine($"Unknown topic: {id}"); return 1; }

Console.Write("Arguments for the topic (optional, e.g. --https or --environment Production): ");
var extra = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
Console.WriteLine($"\nStarting {topic.Id} on http://localhost:5080 — stop with Ctrl+C.\n");
return topic.Run(extra);

/// <summary>A web topic: Id (e.g. "J6.2"), title and entry point. Run builds and runs the topic's own host and blocks until the app stops.</summary>
public sealed record WebTopic(string Id, string Title, Func<string[], int> Run);
