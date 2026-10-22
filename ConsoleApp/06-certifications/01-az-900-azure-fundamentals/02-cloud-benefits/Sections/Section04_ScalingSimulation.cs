using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudBenefits;

/// <summary>
/// Meeting the same 24-hour load with four strategies: small fixed, large fixed (sized for the peak),
/// manual/scheduled scaling (scalability) and autoscaling (elasticity). Unit: "instance" (e.g. a VM or an App Service instance).
/// On the second day the same load plus an unexpected campaign spike: that is where the real difference of elasticity shows.
/// </summary>
public static class Section04_ScalingSimulation
{
    // Instances required per hour (a business application that is busy during the day)
    private static readonly int[] NormalDay = [2, 2, 2, 2, 2, 3, 4, 6, 8, 9, 10, 10, 9, 9, 10, 10, 9, 8, 7, 6, 5, 4, 3, 2];
    // Same day, with an unannounced campaign between 13:00 and 16:00
    private static readonly int[] SpikeDay = [2, 2, 2, 2, 2, 3, 4, 6, 8, 9, 10, 10, 9, 15, 18, 18, 12, 8, 7, 6, 5, 4, 3, 2];

    /// <summary>Plan: produces the 24-hour capacity array for that day's need.</summary>
    private sealed record Strategy(string Name, string Kind, Func<int[], int[]> Plan);

    private static int[] EveryHour(Func<int, int> capacity) => [.. Enumerable.Range(0, 24).Select(capacity)];

    /// <summary>
    /// The autoscale rule looks at the metric every few minutes: it sets capacity to need + 1 (headroom), but because of the cool-down
    /// it can add at most 4 instances per hour; there is no limit when scaling in. min 2, max 20.
    /// </summary>
    private static int[] Autoscale(int[] need)
    {
        var caps = new int[24];
        var previous = 2;
        for (var h = 0; h < 24; h++)
            previous = caps[h] = Math.Clamp(Math.Min(need[h] + 1, previous + 4), 2, 20);
        return caps;
    }

    private static readonly Strategy[] Strategies =
    [
        new("Fixed 4", "no scaling", _ => EveryHour(_ => 4)),
        new("Fixed 10", "sized for the peak", _ => EveryHour(_ => 10)),
        new("Manual", "scalability: up to 10 at 08:00, back to 4 at 20:00", _ => EveryHour(h => h is >= 8 and < 20 ? 10 : 4)),
        new("Autoscale", "elasticity: need + 1, at most +4 per hour (min 2, max 20)", Autoscale),
    ];

    public static void Run()
    {
        Simulate("Day 1 — the usual load", NormalDay);
        Simulate("Day 2 — unannounced campaign at 13:00", SpikeDay);

        Console.WriteLine("""

        Observations:
          • Small fixed capacity is cheap but insufficient during the day; large fixed capacity wastes most of the money
            and is not enough either at an UNFORESEEN peak.
          • If the load is the same every day, manual/scheduled scaling (scalability) comes close to autoscale; it only misses during the ramp hours.
          • The real difference of elasticity is UNEXPECTED load: autoscale absorbs the campaign peak without human intervention
            and shrinks on its own when the peak is over. The first hour of a sudden jump (cool-down limit) may fall short —
            in practice you close that gap with a minimum instance count, thresholds and scheduled rules for predicted days.

        Vertical vs horizontal:
          • Scale UP/DOWN (vertical): making the same machine bigger/smaller (more CPU/RAM). Simple; but it has an UPPER LIMIT
            (the largest VM size), resizing a VM usually requires a restart; a single machine = a single point of failure.
          • Scale OUT/IN (horizontal): increasing/decreasing the NUMBER of instances (VM Scale Sets, App Service instance count).
            Grows practically without limit and also contributes to HA; the application must be designed stateless.
        """);
    }

    private static void Simulate(string title, int[] need)
    {
        Console.WriteLine($"\n{title} — instance capacity per hour (! = below the need → slowdown/errors):\n");
        var hours = Enumerable.Range(0, 24).ToArray();
        Table.Print(["", .. hours.Select(h => h.ToString("00"))],
        [
            ["Need", .. need.Select(n => n.ToString())],
            .. Strategies.Select(s => (IReadOnlyList<string>)[s.Name, .. s.Plan(need).Select((c, h) => Cell(c, need[h]))]),
        ], indent: " ");

        Console.WriteLine();
        Table.Print(["Strategy", "Instance-hours (cost)", "Hours short", "Idle instance-hours", "What it shows"],
            Strategies.Select(s =>
            {
                var caps = s.Plan(need);
                return (IReadOnlyList<string>)
                [
                    s.Name,
                    caps.Sum().ToString(),
                    hours.Count(h => caps[h] < need[h]).ToString(),
                    hours.Sum(h => Math.Max(0, caps[h] - need[h])).ToString(),
                    s.Kind,
                ];
            }));
        Console.WriteLine($"   Actual need: {need.Sum()} instance-hours.");
    }

    private static string Cell(int capacity, int needed) => capacity < needed ? $"{capacity}!" : capacity.ToString();
}
