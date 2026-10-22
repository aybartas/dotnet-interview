using System.Globalization;
using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// SLA percentage ↔ allowed downtime, and composite SLA calculation.
/// As in Microsoft's SLA calculations, a month = 730 hours (365 days / 12).
/// </summary>
public static class SlaCalculator
{
    private const double HoursPerYear = 365 * 24;
    private const double HoursPerMonth = HoursPerYear / 12;   // 730
    private const double HoursPerWeek = 7 * 24;
    private const double HoursPerDay = 24;

    /// <summary>Components in series: all must be up → probabilities are multiplied.</summary>
    public static double Serial(IEnumerable<double> slas) => slas.Aggregate(1.0, (acc, s) => acc * s);

    /// <summary>Redundant (parallel) components: one being up is enough → 1 − (probability that all are down at the same time).</summary>
    public static double Parallel(IEnumerable<double> slas) => 1 - slas.Aggregate(1.0, (acc, s) => acc * (1 - s));

    public static TimeSpan DowntimePerDay(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerDay);
    public static TimeSpan DowntimePerWeek(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerWeek);
    public static TimeSpan DowntimePerMonth(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerMonth);
    public static TimeSpan DowntimePerYear(double sla) => TimeSpan.FromHours((1 - sla) * HoursPerYear);

    /// <summary>0.9995 → "99.95%" (no needless zeros, at most 5 decimals).</summary>
    public static string Percent(double sla) => (sla * 100).ToString("0.#####", CultureInfo.InvariantCulture) + "%";

    /// <summary>Formats a duration readably: "21 min 54 s", "8 h 45 min", "3.65 days".</summary>
    public static string Human(TimeSpan t) => t switch
    {
        { TotalSeconds: < 1 } => "< 1 s",
        { TotalMinutes: < 1 } => $"{t.Seconds} s",
        { TotalHours: < 1 } => $"{t.Minutes} min {t.Seconds} s",
        { TotalDays: < 1 } => $"{(int)t.TotalHours} h {t.Minutes} min",
        _ => t.TotalDays.ToString("0.##", CultureInfo.InvariantCulture) + " days",
    };

    public static IReadOnlyList<string> DowntimeRow(double sla) =>
    [
        Percent(sla),
        Human(DowntimePerDay(sla)),
        Human(DowntimePerWeek(sla)),
        Human(DowntimePerMonth(sla)),
        Human(DowntimePerYear(sla)),
    ];

    public static readonly string[] DowntimeHeaders = ["SLA", "Daily", "Weekly", "Monthly", "Yearly"];

    // ── Command-line modes: downtime / serial / parallel ────────────────────────────────────────

    public static int Downtime(string text)
    {
        if (!TryParse(text, out var sla)) return Fail(text);
        Console.WriteLine($"Maximum allowed downtime for a {Percent(sla)} SLA:\n");
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
            Console.Error.WriteLine("A composite SLA needs at least two values, e.g. dotnet run -- C1.2 serial 99.95 99.99");
            return 1;
        }

        var serial = mode == "serial";
        var result = serial ? Serial(slas) : Parallel(slas);
        var formula = serial
            ? string.Join(" × ", slas.Select(Percent))
            : "1 − " + string.Join(" × ", slas.Select(s => $"(1 − {Percent(s)})"));

        Console.WriteLine($"{(serial ? "Serial (all required)" : "Parallel / redundant (one is enough)")}: {formula} = {Percent(result)}\n");
        Table.Print(DowntimeHeaders, [.. slas.Select(DowntimeRow), DowntimeRow(result)]);
        Console.WriteLine(serial
            ? "\n   The last row is the composite SLA: every component added to the chain LOWERS the SLA (below even the weakest component)."
            : "\n   The last row is the composite SLA: redundancy RAISES the SLA — assuming failures are independent and failover works.");
        return 0;
    }

    /// <summary>"99.95", "99,95" or "99.95%" are accepted; interpreted as a percentage.</summary>
    private static bool TryParse(string text, out double sla)
    {
        var ok = double.TryParse(text.Trim('%').Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var percent);
        sla = percent / 100;
        return ok && percent is > 0 and <= 100;
    }

    private static int Fail(string text)
    {
        Console.Error.WriteLine($"Invalid SLA: '{text}'. Give it as a percentage, e.g. 99.95");
        return 1;
    }
}
