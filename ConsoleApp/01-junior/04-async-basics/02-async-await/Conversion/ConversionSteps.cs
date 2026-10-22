namespace Junior.AsyncBasics.AsyncAwait;

/// <summary>
/// 🛠 J4.2: "Converting an application that reads a file + makes HTTP calls, written synchronously, to async step by step."
/// The same job in three forms: read the file → make two independent HTTP calls → combine the results.
/// </summary>
public static class ConversionSteps
{
    // ───────── STEP 1: SYNCHRONOUS ─────────
    // The calling thread is BLOCKED on every I/O (file, then HTTP 1, then HTTP 2). In a UI the interface freezes; on a server the thread waits for nothing.
    public static string Step1_Sync(HttpClient client, string path)
    {
        var text = File.ReadAllText(path);
        var a = Send(client, "/delay/150");
        var b = Send(client, "/delay/150");
        return $"{text.Trim()} | {a} | {b}";

        static string Send(HttpClient c, string url)
        {
            using var response = c.Send(new HttpRequestMessage(HttpMethod.Get, url));      // synchronous HTTP (HttpClient.Send) — the thread is blocked
            using var reader = new StreamReader(response.Content.ReadAsStream());
            return reader.ReadToEnd();
        }
    }

    // ───────── STEP 2: "FAKE ASYNC" — wrapping synchronous code in Task.Run ─────────
    // The caller is freed BUT the work still runs synchronously by blocking a POOL thread. It is an acceptable transition step for a UI,
    // but gains nothing on a server (ASP.NET Core): we just swapped one thread for another.
    public static Task<string> Step2_FakeAsync(HttpClient client, string path) =>
        Task.Run(() => Step1_Sync(client, path));

    // ───────── STEP 3: ASYNC ALL THE WAY ─────────
    // Real async I/O APIs; EVERY method in the chain is async and awaited; the two independent calls are started at the same time.
    public static async Task<string> Step3_AsyncAllTheWay(HttpClient client, string path, CancellationToken ct = default)
    {
        var text = await File.ReadAllTextAsync(path, ct);
        var a = client.GetStringAsync("/delay/150", ct);                                   // start (do NOT await)
        var b = client.GetStringAsync("/delay/150", ct);                                   // start — both calls are in flight at the same time
        var results = await Task.WhenAll(a, b);                                            // await together: ~150 ms (300 ms in step 1)
        return $"{text.Trim()} | {results[0]} | {results[1]}";
    }
}
