namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>
/// Writes one log line at EVERY level. <c>ILogger&lt;OrderService&gt;</c> makes the category name
/// <c>Junior.AspNetCoreFundamentals.LoggingBasics.OrderService</c> (the full type name) - that string is what
/// the "Logging:LogLevel" rules in appsettings.json are matched against.
/// </summary>
public sealed class OrderService(ILogger<OrderService> logger)
{
    public void PlaceOrder(int orderId, int itemCount)
    {
        logger.LogTrace("Entering PlaceOrder({OrderId})", orderId);                                       // very detailed, may contain sensitive data - never in production
        logger.LogDebug("Calculating tax and discounts for order {OrderId}", orderId);                     // developer diagnostics
        logger.LogInformation("Order {OrderId} placed with {ItemCount} items", orderId, itemCount);        // normal business flow
        logger.LogWarning("Stock for order {OrderId} is running low, {Remaining} left", orderId, 3);       // unexpected but handled
        logger.LogError("Payment gateway rejected order {OrderId}: {Reason}", orderId, "card_declined");   // the operation failed
        logger.LogCritical("Order database is unreachable, order {OrderId} cannot be saved", orderId);     // the application / a vital resource is down

        // Cost of a level: arguments are evaluated BEFORE the call. When the argument itself is expensive,
        // guard it, otherwise the work is done even if Debug is switched off.
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Basket snapshot for order {OrderId}: {Basket}", orderId, DescribeBasket(itemCount));
        }
    }

    /// <summary>Which levels would actually be written for THIS category under the current configuration.</summary>
    public IReadOnlyDictionary<string, bool> EnabledLevels() => EnabledLevels(logger);

    public static IReadOnlyDictionary<string, bool> EnabledLevels(ILogger log) =>
        Enum.GetValues<LogLevel>()
            .Where(l => l != LogLevel.None)
            .ToDictionary(l => l.ToString(), log.IsEnabled);

    /// <summary>The RIGHT way to log an exception: pass it as the FIRST argument, keep the template static.</summary>
    public void FailPaymentCorrectly(int orderId)
    {
        try { ChargeCard(orderId); }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Payment failed for order {OrderId}", orderId);   // exception object + stack trace + type kept as a separate field
        }
    }

    /// <summary>The WRONG way: the exception is flattened into the message text; providers never see an Exception.</summary>
    public void FailPaymentWrongly(int orderId)
    {
        try { ChargeCard(orderId); }
        catch (InvalidOperationException ex)
        {
#pragma warning disable CA2254 // deliberate anti-pattern: non-constant template
            logger.LogError($"Payment failed for order {orderId}: {ex}");
#pragma warning restore CA2254
        }
    }

    private static void ChargeCard(int orderId) =>
        throw new InvalidOperationException($"Gateway timeout while charging order {orderId}");

    private static string DescribeBasket(int itemCount) =>
        string.Join(", ", Enumerable.Range(1, itemCount).Select(i => $"item-{i}")); // imagine a heavy serialization here
}
