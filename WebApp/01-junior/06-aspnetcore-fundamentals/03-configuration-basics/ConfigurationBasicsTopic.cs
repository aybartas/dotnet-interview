using Microsoft.Extensions.Options;

namespace Junior.AspNetCoreFundamentals.ConfigurationBasics;

/// <summary>
/// J6.3 — Configuration &amp; Options basics. Runs its OWN host on http://localhost:5080 (Ctrl+C to stop):
///
///   dotnet run -- J6.3                                       → Production (default); Smtp:Password is optional
///   dotnet run -- J6.3 --environment Development             → also loads appsettings.Development.json + user-secrets
///   dotnet run -- J6.3 --require-password                    → startup FAILS (fail-fast) unless Smtp:Password is provided
///   Smtp__Password=s3cret dotnet run -- J6.3 --require-password    → starts (value comes from an environment variable)
///   dotnet run -- J6.3 --Smtp:Port=70000                     → command line wins, then ValidateOnStart rejects it
///
/// Endpoints: /priority, /priority/live, /hierarchy, /smtp (see README and requests.http).
/// </summary>
public static class ConfigurationBasicsTopic
{
    /// <summary>The id of the user-secrets store (~/.microsoft/usersecrets/&lt;id&gt;/secrets.json). Used with: dotnet user-secrets set ... --id</summary>
    public const string UserSecretsId = "dotnet-interview-j6-3";

    private const string TopicFolder = "01-junior/06-aspnetcore-fundamentals/03-configuration-basics";
    private const string RequirePasswordSwitch = "--require-password";

    public static int Run(string[] args)
    {
        // Our own switch is not a configuration key: take it out before the arguments reach AddCommandLine.
        var requirePassword = args.Contains(RequirePasswordSwitch);
        args = args.Where(a => a != RequirePasswordSwitch).ToArray();

        // The demo sets two process-level environment variables itself (prefix J63_), so /priority does not depend on your machine.
        PriorityDemo.SetEnvironmentVariables();

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:5080");
        builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.WriteIndented = true);   // readable curl output

        var configDir = Path.Combine(builder.Environment.ContentRootPath, TopicFolder);
        AddConfiguration(builder, configDir, args);
        AddOptions(builder, requirePassword);

        var app = builder.Build();
        MapEndpoints(app, configDir);

        PrintBanner(app, requirePassword);

        try
        {
            app.Run();                 // ValidateOnStart runs here, while the host starts - BEFORE the first request is served
            return 0;
        }
        catch (OptionsValidationException ex)
        {
            // Fail-fast: a misconfigured app must not start at all (instead of failing at the first request in production).
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSTARTUP FAILED (fail-fast): the configuration is invalid, the app will not start.");
            Console.ResetColor();
            foreach (var failure in ex.Failures) Console.WriteLine($"  - {failure}");
            if (ex.Failures.Any(f => f.Contains("Password")))
                Console.WriteLine($"\nHow to fix: set the Smtp__Password environment variable, or: dotnet user-secrets set \"Smtp:Password\" \"...\" --id {UserSecretsId}");
            else
                Console.WriteLine("\nHow to fix: correct the value in the source that provides it (the highest-priority source wins - see /priority).");
            return 1;
        }
    }

    // ───────────────────────── Configuration sources ─────────────────────────

    /// <summary>
    /// WebApplication.CreateBuilder has ALREADY added: (host settings) → environment variables → command line.
    /// This project has no appsettings.json at the content root, so we add our topic-local JSON files ourselves.
    /// A source added LATER wins - therefore the environment variables and the command line are added once more at the end,
    /// otherwise our JSON files would override them (the opposite of the real default order).
    /// </summary>
    private static void AddConfiguration(WebApplicationBuilder builder, string configDir, string[] args)
    {
        var config = builder.Configuration;
        var env = builder.Environment;

        config.AddJsonFile(Path.Combine(configDir, "appsettings.json"), optional: false);
        config.AddJsonFile(Path.Combine(configDir, $"appsettings.{env.EnvironmentName}.json"), optional: true);

        // The real default does this for you in Development when the project has a <UserSecretsId>. This project has none
        // (we do not touch the csproj), so the id is given in code; the secrets are kept OUTSIDE the repo, in your user profile.
        if (env.IsDevelopment())
            config.AddUserSecrets(UserSecretsId, reloadOnChange: false);

        config.AddEnvironmentVariables();
        config.AddCommandLine(args);
    }

    // ───────────────────────── Options pattern ─────────────────────────

    private static void AddOptions(WebApplicationBuilder builder, bool requirePassword)
    {
        builder.Services
            .AddOptions<SmtpOptions>()
            .Bind(builder.Configuration.GetSection(SmtpOptions.SectionName))   // section "Smtp" → SmtpOptions
            .ValidateDataAnnotations()                                         // [Required], [Range], [EmailAddress] ...
            .Validate(o => !requirePassword || !string.IsNullOrWhiteSpace(o.Password),   // an extra, conditional rule
                      "Smtp:Password is required (--require-password) but no source provided it.")
            .ValidateOnStart();                                                // validate at startup, not at first use

        // Short alternative without validation:  builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
        // IOptionsSnapshot<T> (per request, scoped) and IOptionsMonitor<T> (singleton, change notifications) → M3.
    }

    // ───────────────────────── Endpoints ─────────────────────────

    private static void MapEndpoints(WebApplication app, string configDir)
    {
        app.MapGet("/", () => new
        {
            topic = "J6.3 Configuration & Options basics",
            endpoints = new[] { "/priority", "/priority/live", "/hierarchy", "/smtp" },
        });

        // Same key, five sources, isolated builder → which one wins?
        app.MapGet("/priority", (IWebHostEnvironment env) => PriorityDemo.Describe(env.EnvironmentName, configDir));

        // The REAL configuration of this host: the same question, answered by the providers in the actual IConfigurationRoot.
        app.MapGet("/priority/live", (IConfiguration configuration) =>
        {
            var root = (IConfigurationRoot)configuration;
            var providers = root.Providers.Select((p, i) => $"{i + 1,2}. {p}").ToList();

            // IConfigurationRoot.Providers is in registration order; the lookup runs from the LAST one backwards (this is exactly what root[key] does).
            var winner = root.Providers.Reverse().FirstOrDefault(p => p.TryGet(PriorityDemo.Key, out _));

            return new
            {
                key = PriorityDemo.Key,
                value = root[PriorityDemo.Key],
                winningProvider = winner?.ToString(),
                providers,
                hint = "try: dotnet run -- J6.3 --Demo:Message=hello  (the command-line provider becomes the winner)",
            };
        });

        // Hierarchical keys: ':' in code ↔ '__' in environment variables; GetSection / GetValue<T> / Get<T> / GetChildren.
        app.MapGet("/hierarchy", (IConfiguration config) =>
        {
            var logging = config.GetSection("Logging");
            var logLevel = logging.GetSection("LogLevel");                   // the same as config.GetSection("Logging:LogLevel")

            // The env-var spelling of a key: ':' → '__'  (':' is not allowed in the names of environment variables on some shells/OSes).
            var keys = logging.AsEnumerable(makePathsRelative: false)
                .Where(kv => kv.Value is not null)
                .Select(kv => new { key = kv.Key, environmentVariable = kv.Key.Replace(":", "__"), value = kv.Value })
                .ToList();

            // Env var demo: the JSON says Information; the variable (set by this demo) overrides it - in an isolated builder.
            var layered = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(configDir, "appsettings.json"))
                .AddEnvironmentVariables(PriorityDemo.EnvVarPrefix);

            var json = new ConfigurationBuilder().AddJsonFile(Path.Combine(configDir, "appsettings.json")).Build();
            var withEnv = layered.Build();

            return new
            {
                keysUnderLogging = keys,
                indexer = new
                {
                    fullKey = config["Logging:LogLevel:Default"],
                    caseInsensitive = config["LOGGING:loglevel:DEFAULT"],         // keys are case-insensitive
                    viaGetSection = logLevel["Default"],
                    missingKey = config["Logging:Nope"],                          // missing key → null (no exception)
                },
                getValue = new
                {
                    port = config.GetValue<int>("Smtp:Port"),                     // typed conversion
                    useSsl = config.GetValue<bool>("Smtp:UseSsl"),
                    missingWithDefault = config.GetValue("Smtp:Timeout", 30),    // default when the key is absent
                    missingWithoutDefault = config.GetValue<int>("Smtp:Timeout"), // → default(int) = 0 !
                },
                getSectionGetT = config.GetSection("Smtp").Get<SmtpOptions>()?.ToMasked(),   // bind the whole section to an object
                getChildren = new
                {
                    logLevelChildren = logLevel.GetChildren().Select(c => new { c.Key, c.Path, c.Value }).ToList(),
                    endpointsArray = config.GetSection("Endpoints").GetChildren()
                        .Select(c => new { c.Key, name = c["Name"], url = c["Url"] }).ToList(),     // array items: keys "0", "1"
                    arrayKeyExample = "Endpoints:1:Name = " + config["Endpoints:1:Name"] + "   (env var: Endpoints__1__Name)",
                },
                environmentVariableOverride = new
                {
                    key = "Logging:LogLevel:Default",
                    environmentVariable = PriorityDemo.HierarchyEnvVarName,
                    note = "AddEnvironmentVariables(\"J63_\") strips the prefix, then '__' becomes ':'",
                    valueInJsonOnly = json["Logging:LogLevel:Default"],
                    valueWithEnvironmentVariable = withEnv["Logging:LogLevel:Default"],
                },
            };
        });

        // Options pattern: IOptions<T> is injected; the password is MASKED in the response.
        app.MapGet("/smtp", (IOptions<SmtpOptions> options) => options.Value.ToMasked());
    }

    // ───────────────────────── Console ─────────────────────────

    private static void PrintBanner(WebApplication app, bool requirePassword)
    {
        var root = (IConfigurationRoot)app.Configuration;
        Console.WriteLine("J6.3 Configuration & Options - http://localhost:5080  (stop: Ctrl+C)");
        Console.WriteLine($"Environment: {app.Environment.EnvironmentName}   require-password: {requirePassword}");
        Console.WriteLine("Configuration providers (registration order - the LAST one wins):");
        var i = 0;
        foreach (var p in root.Providers) Console.WriteLine($"  {++i,2}. {p}");
        Console.WriteLine("Endpoints: /priority  /priority/live  /hierarchy  /smtp\n");
    }
}
