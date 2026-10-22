namespace Junior.AsyncBasics.AsyncAwait;

/// <summary>
/// 🛠 J4.2: "Senkron yazılmış dosya okuma + HTTP çağırma yapan bir uygulamayı adım adım async'e dönüştürme."
/// Aynı iş üç biçimde: dosyayı oku → iki bağımsız HTTP çağrısı yap → sonuçları birleştir.
/// </summary>
public static class ConversionSteps
{
    // ───────── ADIM 1: SENKRON ─────────
    // Çağıran thread her I/O'da BLOKLANIR (dosya, sonra HTTP 1, sonra HTTP 2). UI'da arayüz donar; sunucuda thread boşa bekler.
    public static string Step1_Sync(HttpClient client, string path)
    {
        var text = File.ReadAllText(path);
        var a = Send(client, "/delay/150");
        var b = Send(client, "/delay/150");
        return $"{text.Trim()} | {a} | {b}";

        static string Send(HttpClient c, string url)
        {
            using var response = c.Send(new HttpRequestMessage(HttpMethod.Get, url));      // senkron HTTP (HttpClient.Send) — thread bloklanır
            using var reader = new StreamReader(response.Content.ReadAsStream());
            return reader.ReadToEnd();
        }
    }

    // ───────── ADIM 2: "SAHTE ASYNC" — senkron kodu Task.Run'a sarmak ─────────
    // Çağıran serbest kalır AMA iş bir HAVUZ thread'ini bloklayarak yine senkron çalışır. UI için kabul edilebilir bir geçiş adımıdır,
    // sunucuda (ASP.NET Core) hiçbir şey kazandırmaz: bir thread'i başka bir thread ile değiştirdik.
    public static Task<string> Step2_FakeAsync(HttpClient client, string path) =>
        Task.Run(() => Step1_Sync(client, path));

    // ───────── ADIM 3: ASYNC ALL THE WAY ─────────
    // Gerçek async I/O API'leri; zincirdeki HER metot async ve await edilir; bağımsız iki çağrı aynı anda başlatılır.
    public static async Task<string> Step3_AsyncAllTheWay(HttpClient client, string path, CancellationToken ct = default)
    {
        var text = await File.ReadAllTextAsync(path, ct);
        var a = client.GetStringAsync("/delay/150", ct);                                   // başlat (await ETME)
        var b = client.GetStringAsync("/delay/150", ct);                                   // başlat — iki çağrı aynı anda yolda
        var results = await Task.WhenAll(a, b);                                            // birlikte bekle: ~150 ms (adım 1'de 300 ms)
        return $"{text.Trim()} | {results[0]} | {results[1]}";
    }
}
