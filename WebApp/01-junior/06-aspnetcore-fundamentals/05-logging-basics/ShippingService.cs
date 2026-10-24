namespace Junior.AspNetCoreFundamentals.LoggingBasics;

/// <summary>
/// Source-generated logging: the [LoggerMessage] generator writes the method body at compile time.
/// The template is parsed once (not on every call), arguments are not boxed, the level check is built in.
/// The class must be <c>partial</c> and the logger comes from the <c>ILogger</c> field.
/// </summary>
public sealed partial class ShippingService
{
    private readonly ILogger<ShippingService> _logger;

    public ShippingService(ILogger<ShippingService> logger) => _logger = logger;

    public void Ship(int orderId, string city)
    {
        var started = Environment.TickCount64;
        OrderShipped(orderId, city, Environment.TickCount64 - started);
        try { throw new TimeoutException("Carrier API did not answer"); }
        catch (TimeoutException ex) { ShippingFailed(ex, orderId); }
    }

    [LoggerMessage(EventId = 2001, Level = LogLevel.Information, Message = "Order {OrderId} shipped to {City} in {ElapsedMs} ms")]
    private partial void OrderShipped(int orderId, string city, long elapsedMs);

    // An Exception parameter is recognised automatically and is NOT part of the template.
    [LoggerMessage(EventId = 2002, Level = LogLevel.Error, Message = "Shipping failed for order {OrderId}")]
    private partial void ShippingFailed(Exception ex, int orderId);
}
