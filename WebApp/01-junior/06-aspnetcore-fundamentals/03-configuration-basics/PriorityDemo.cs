namespace Junior.AspNetCoreFundamentals.ConfigurationBasics;

/// <summary>
/// Shows "the last source wins" deterministically: the same key (Demo:Message) is provided by five sources, layered in the REAL default
/// order in an ISOLATED ConfigurationBuilder (so the result does not depend on the reader's machine):
///
///   appsettings.json → appsettings.{Environment}.json → User Secrets → environment variables → command line
///
/// Two layers are simulated (and the output says so): User Secrets (an in-memory provider; the real one reads a file in your user profile)
/// and the command line (a fixed string[] instead of your real args).
/// </summary>
internal static class PriorityDemo
{
    public const string Key = "Demo:Message";

    /// <summary>The demo sets this process-level environment variable itself. Prefix "J63_" is stripped and "__" becomes ":" → key "Demo:Message".</summary>
    public const string EnvVarName = "J63_Demo__Message";
    public const string EnvVarPrefix = "J63_";

    /// <summary>Second variable, used by /hierarchy: the env-var spelling of the key "Logging:LogLevel:Default".</summary>
    public const string HierarchyEnvVarName = "J63_Logging__LogLevel__Default";

    private sealed record Layer(string Source, string Note, bool Active, Action<IConfigurationBuilder> Add);

    public static void SetEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable(EnvVarName, "from environment variable (J63_Demo__Message)");
        Environment.SetEnvironmentVariable(HierarchyEnvVarName, "Warning");
    }

    public static object Describe(string environmentName, string configDir)
    {
        var isDevelopment = environmentName.Equals("Development", StringComparison.OrdinalIgnoreCase);

        Layer[] layers =
        [
            new("appsettings.json",
                "always loaded",
                Active: true,
                b => b.AddJsonFile(Path.Combine(configDir, "appsettings.json"), optional: false)),

            new($"appsettings.{environmentName}.json",
                "loaded for the current environment (optional file)",
                Active: true,
                b => b.AddJsonFile(Path.Combine(configDir, $"appsettings.{environmentName}.json"), optional: true)),

            new("User Secrets (simulated in memory)",
                isDevelopment ? "the real default adds it ONLY in Development; simulated here with an in-memory provider"
                              : "SKIPPED: the real default adds user secrets only in Development",
                Active: isDevelopment,
                b => b.AddInMemoryCollection(new Dictionary<string, string?> { [Key] = "from user-secrets (simulated)" })),

            new($"Environment variable {EnvVarName}",
                $"AddEnvironmentVariables(\"{EnvVarPrefix}\"): prefix stripped, '__' becomes ':'",
                Active: true,
                b => b.AddEnvironmentVariables(EnvVarPrefix)),

            new("Command line (simulated)",
                "AddCommandLine([\"--Demo:Message=...\"]) - a fixed array, not your real args",
                Active: true,
                b => b.AddCommandLine([$"--{Key}=from command line (--Demo:Message=...)"])),
        ];

        var cumulative = new ConfigurationBuilder();
        string? effective = null;
        string? winner = null;
        var steps = new List<object>();
        var order = 0;

        foreach (var layer in layers)
        {
            order++;
            string? own = null;
            if (layer.Active)
            {
                layer.Add(cumulative);
                effective = cumulative.Build()[Key];   // the value after stacking this layer on top of the previous ones

                var alone = new ConfigurationBuilder();
                layer.Add(alone);
                own = alone.Build()[Key];              // what THIS source provides on its own
                if (own is not null) winner = layer.Source;
            }

            steps.Add(new
            {
                order,
                source = layer.Source,
                note = layer.Note,
                providesKey = own is not null,
                valueAfterThisLayer = effective,
            });
        }

        return new
        {
            key = Key,
            environment = environmentName,
            rule = "the LAST source that provides the key wins",
            layers = steps,
            winner = new { source = winner, value = effective },
            defaultOrderOfWebApplicationCreateBuilder = new[]
            {
                "1. appsettings.json",
                "2. appsettings.{Environment}.json",
                "3. User Secrets (Development only, if a user-secrets id is configured)",
                "4. Environment variables (no prefix)",
                "5. Command-line arguments",
            },
        };
    }
}
