using System.Collections.Concurrent;

namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>One log entry as a provider sees it: the formatted text AND the structured state behind it.</summary>
public sealed record CapturedLogEntry(
    string Category,
    string Level,
    string Message,
    string? Template,
    IReadOnlyDictionary<string, object?> Properties,
    IReadOnlyList<string> Scopes,
    string? ExceptionType);

/// <summary>
/// A tiny in-memory <see cref="ILoggerProvider"/> that keeps the last entries of this topic's own categories.
/// It exists to make structured logging VISIBLE: <c>ILogger</c> hands every provider the message template and the
/// named values as <c>state</c> (<c>IReadOnlyList&lt;KeyValuePair&lt;string, object?&gt;&gt;</c>); the console provider
/// just formats that state to text, a JSON/Seq/Elastic provider keeps the properties as queryable fields.
/// The alias lets configuration target it: <c>Logging:Capture:LogLevel:Default</c>.
/// </summary>
[ProviderAlias("Capture")]
public sealed class CapturingLoggerProvider : ILoggerProvider, ISupportExternalScope
{
    private const string CapturedPrefix = "Junior.AspNetCoreFundamentals.LoggingBasics";
    private const int MaxEntries = 200;

    private readonly ConcurrentQueue<CapturedLogEntry> _entries = new();
    private IExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

    // The LoggerFactory gives every provider that implements ISupportExternalScope the shared scope store.
    public void SetScopeProvider(IExternalScopeProvider scopeProvider) => _scopeProvider = scopeProvider;

    public ILogger CreateLogger(string categoryName) => new CapturingLogger(categoryName, this);

    public void Clear() => _entries.Clear();

    public IReadOnlyList<CapturedLogEntry> Snapshot() => [.. _entries];

    public void Dispose() { }

    private sealed class CapturingLogger(string category, CapturingLoggerProvider owner) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => owner._scopeProvider.Push(state);

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None && category.StartsWith(CapturedPrefix, StringComparison.Ordinal);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            // THE point of structured logging: the state is a list of name/value pairs, not just a string.
            var properties = new Dictionary<string, object?>();
            string? template = null;
            if (state is IReadOnlyList<KeyValuePair<string, object?>> pairs)
            {
                foreach (var (key, value) in pairs)
                {
                    if (key == "{OriginalFormat}") template = value as string;
                    else properties[key] = value;
                }
            }

            var scopes = new List<string>();
            owner._scopeProvider.ForEachScope((scope, list) => list.Add(DescribeScope(scope)), scopes);

            owner._entries.Enqueue(new CapturedLogEntry(category, logLevel.ToString(), formatter(state, exception), template, properties, scopes, exception?.GetType().Name));
            while (owner._entries.Count > MaxEntries) owner._entries.TryDequeue(out _);
        }

        private static string DescribeScope(object? scope) => scope switch
        {
            IEnumerable<KeyValuePair<string, object?>> kv => string.Join(", ", kv.Select(p => $"{p.Key}={p.Value}")),
            _ => scope?.ToString() ?? "(null)"
        };
    }
}
