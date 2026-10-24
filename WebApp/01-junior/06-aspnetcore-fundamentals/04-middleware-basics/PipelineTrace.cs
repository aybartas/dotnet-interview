using System.Text;

namespace Junior.AspNetCoreFundamentals.MiddlewareBasics;

/// <summary>
/// Teaching aid: records the order in which middlewares run, with indentation that shows the "nested rings".
///
///     → A            ← the request enters ring A
///       → B          ← ... and goes deeper into ring B
///         · endpoint
///       ← B          ← the response comes back out through B
///     ← A
///
/// The middlewares call <see cref="Enter"/> / <see cref="Leave"/>; if no trace was started for the request
/// (every non-experiment route), the calls do nothing.
/// </summary>
public static class PipelineTrace
{
    private const string ItemsKey = "demo.trace";

    private sealed class State
    {
        public List<string> Lines { get; } = [];
        public int Depth;
    }

    public static void Start(HttpContext ctx) => ctx.Items[ItemsKey] = new State();

    public static void Enter(HttpContext ctx, string name, string? note = null)
    {
        if (ctx.Items[ItemsKey] is not State s) return;
        s.Lines.Add($"{Indent(s.Depth)}→ {name}{(note is null ? "" : $"  ({note})")}");
        s.Depth++;
    }

    public static void Leave(HttpContext ctx, string name)
    {
        if (ctx.Items[ItemsKey] is not State s) return;
        s.Depth--;
        s.Lines.Add($"{Indent(s.Depth)}← {name}");
    }

    public static void Note(HttpContext ctx, string text)
    {
        if (ctx.Items[ItemsKey] is State s) s.Lines.Add($"{Indent(s.Depth)}· {text}");
    }

    public static string Render(HttpContext ctx) =>
        ctx.Items[ItemsKey] is State s ? string.Join('\n', s.Lines) : "(no trace)";

    private static string Indent(int depth) => new(' ', depth * 2);
}

/// <summary>
/// Harness (NOT part of the experiment): the outermost ring of the two experiment branches. A response whose body has started
/// can no longer be changed, but the "← after" lines are only produced while the pipeline unwinds — so this middleware
/// buffers whatever the inner pipeline writes, waits for the unwinding to finish and only then writes the complete report.
/// If an exception escapes the whole pipeline it is recorded in the trace and re-thrown (Kestrel then answers with an empty 500).
/// </summary>
public sealed class TraceReportMiddleware(RequestDelegate next, string label)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        PipelineTrace.Start(ctx);
        var realBody = ctx.Response.Body;
        await using var buffer = new MemoryStream();
        ctx.Response.Body = buffer;                                  // inner middlewares write here
        try
        {
            await next(ctx);
        }
        catch (Exception ex)
        {
            PipelineTrace.Note(ctx, $"✗ UNHANDLED {ex.GetType().Name} escaped the whole pipeline: \"{ex.Message}\"");
            ctx.Response.Body = realBody;
            throw;                                                   // nobody caught it: the host answers 500 with an empty body
        }

        ctx.Response.Body = realBody;
        var report = new StringBuilder()
            .AppendLine($"{label} — {ctx.Request.Method} {ctx.Request.PathBase}{ctx.Request.Path}{ctx.Request.QueryString}")
            .AppendLine($"status: {ctx.Response.StatusCode}")
            .AppendLine()
            .AppendLine(PipelineTrace.Render(ctx))
            .AppendLine()
            .AppendLine("body written by the pipeline:")
            .Append("  ").Append(Encoding.UTF8.GetString(buffer.ToArray()).ReplaceLineEndings("\n  ").TrimEnd());

        ctx.Response.ContentType = "text/plain; charset=utf-8";
        await ctx.Response.WriteAsync(report.ToString());           // headers set via OnStarting are applied right here
    }
}
