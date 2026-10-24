namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>Structured logging (message template + named placeholders) vs string interpolation.</summary>
public sealed class StructuredLoggingDemo(ILogger<StructuredLoggingDemo> logger)
{
    public void LogBothWays(int userId, string city)
    {
        // GOOD: a constant template with named placeholders. The provider receives the template AND the values:
        // UserId=42, City="Izmir" become separate, queryable fields (WHERE UserId = 42).
        logger.LogInformation("User {UserId} logged in from {City}", userId, city);

        // BAD: the string is built BEFORE the logger is called. The template is different for every user (so you cannot
        // group "the same event"), there are no properties, and the formatting cost is paid even if Information is off.
#pragma warning disable CA2254 // deliberate anti-pattern: non-constant template
        logger.LogInformation($"User {userId} logged in from {city}");
#pragma warning restore CA2254
    }

    /// <summary>Scopes: everything logged inside the using block carries the CorrelationId, without passing it around.</summary>
    public void LogInsideScope(string correlationId, OrderService orders)
    {
        using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
        {
            logger.LogInformation("Handling the request");
            orders.PlaceOrder(orderId: 1001, itemCount: 2);   // another class, same scope
            logger.LogInformation("Request handled");
        }
        logger.LogInformation("Outside the scope - no CorrelationId here");
    }
}
