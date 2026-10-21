using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// SLA yüzdesi ↔ izin verilen kesinti süresi ve composite SLA hesabı.
/// Microsoft'un SLA hesaplarındaki gibi ay = 730 saat (365 gün / 12) alınır.
/// </summary>
public static class SlaCalculator
{
    private const double HoursPerYear = 365 * 24;
    private const double HoursPerMonth = HoursPerYear / 12;   // 730
    private const double HoursPerWeek = 7 * 24;
    private const double HoursPerDay = 24;

    /// <summary>Seri bağlı bileşenler: hepsi ayakta olmalı → olasılıklar çarpılır.</summary>
    public static double Serial(IEnumerable<double> slas) => slas.Aggregate(1.0, (acc, s) => acc * s);

    /// <summary>Yedekli (paralel) bileşenler: biri ayakta yeterli → 1 − (hepsinin aynı anda düşme olasılığı).</summary>
    public static double Parallel(IEnumerable<double> slas) => 1 - slas.Aggregate(1.0, (acc, s) => acc * (1 - s));

    public static TimeSpan DowntimePerDay(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerDay);
    public static TimeSpan DowntimePerWeek(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerWeek);
    public static TimeSpan DowntimePerMonth(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerMonth);
    public static TimeSpan DowntimePerYear(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerYear);

    /// <summary>0,9995 → "%99,95" (gereksiz sıfırlar olmadan, en fazla 5 ondalık).</summary>
    public static string Percent(double sla) => "%" + (sla * 100).ToString("0.#####", CultureInfo.CurrentCulture);

    /// <summary>Süreyi okunur yazar: "21 dk 54 sn", "8 sa 45 dk", "3,65 gün".</summary>
    public static string Human(TimeSpan t) => t switch
    {
        { TotalSeconds: < 1 } => "< 1 sn",
        { TotalMinutes: < 1 } => $"{t.Seconds} sn",
        { TotalHours: < 1 } => $"{t.Minutes} dk {t.Seconds} sn",
        { TotalDays: < 1 } => $"{(int)t.TotalHours} sa {t.Minutes} dk",
        _ => $"{t.TotalDays:0.##} gün",
    };

    public static IReadOnlyList<string> DowntimeRow(double sla) =>
    [
        Percent(sla),
        Human(DowntimePerDay(sla)),
        Human(DowntimePerWeek(sla)),
        Human(DowntimePerMonth(sla)),
        Human(DowntimePerYear(sla)),
    ];

    public static readonly string[] DowntimeHeaders = ["SLA", "Günlük", "Haftalık", "Aylık", "Yıllık"];

    // ── Komut satırı modları: kesinti / seri / paralel ──────────────────────────────────────────

    public static int Downtime(string text)
    {
        if (!TryParse(text, out var sla)) return Fail(text);
        Console.WriteLine($"{Percent(sla)} SLA için izin verilen en fazla kesinti:\n");
        Table.Print(DowntimeHeaders, [DowntimeRow(sla)]);
        return 0;
    }

    public static int Composite(string mode, string[] values)
    {
        var slas = new List<double>();
        foreach (var v in values)
        {
            if (!TryParse(v, out var s)) return Fail(v);
            slas.Add(s);
        }
        if (slas.Count < 2)
        {
            Console.Error.WriteLine("Composite SLA için en az iki değer gerekir, ör. dotnet run -- 1.2 seri 99.95 99.99");
            return 1;
        }

        var serial = mode == "seri";
        var result = serial ? Serial(slas) : Parallel(slas);
        var formula = serial
            ? string.Join(" × ", slas.Select(Percent))
            : "1 − " + string.Join(" × ", slas.Select(s => $"(1 − {Percent(s)})"));

        Console.WriteLine($"{(serial ? "Seri (hepsi gerekli)" : "Paralel / yedekli (biri yeterli)")}: {formula} = {Percent(result)}\n");
        Table.Print(DowntimeHeaders, [.. slas.Select(DowntimeRow), DowntimeRow(result)]);
        Console.WriteLine(serial
            ? "\n   Son satır bileşik SLA: zincire her bileşen eklendikçe SLA DÜŞER (en zayıf bileşenin de altına iner)."
            : "\n   Son satır bileşik SLA: yedeklilik SLA'yı YÜKSELTİR — arızaların bağımsız olduğu ve failover'ın çalıştığı varsayımıyla.");
        return 0;
    }

    /// <summary>"99.95", "99,95" veya "%99,95" kabul edilir; yüzde olarak yorumlanır.</summary>
    private static bool TryParse(string text, out double sla)
    {
        var ok = double.TryParse(text.TrimStart('%').Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent);
        sla = percent / 100;
        return ok && percent is > 0 and <= 100;
    }

    private static int Fail(string text)
    {
        Console.Error.WriteLine($"Geçersiz SLA: '{text}'. Yüzde olarak yaz, ör. 99.95");
        return 1;
    }
}
