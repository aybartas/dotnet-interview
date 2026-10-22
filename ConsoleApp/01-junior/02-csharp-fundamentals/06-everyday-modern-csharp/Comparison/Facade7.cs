using System;
using System.Collections.Generic;
using System.Linq;
using Junior.CSharpFundamentals.ModernCSharp.Csharp7Style;

#nullable disable
namespace Junior.CSharpFundamentals.ModernCSharp.Comparison
{
    public sealed class Facade7 : IOrderFacade
    {
        public string StyleName { get { return "C# 7 style"; } }

        private static Order Build(string tier, string name, string email, string city, IReadOnlyList<(string Product, int Qty, decimal Price)> lines, OrderStatus status)
        {
            var customer = new Customer(name, (CustomerTier)Enum.Parse(typeof(CustomerTier), tier));
            customer.Email = email;
            if (city != null) customer.Address = new Address(city, null);
            return new Order("ORD-1", customer, lines.Select(l => new OrderLine(l.Product, l.Qty, l.Price)), status);
        }

        public decimal DiscountRate(string tier, decimal subtotal)
        {
            return Build(tier, "x", "x", null, new[] { ("x", 1, subtotal) }, OrderStatus.Pending).DiscountRate;
        }

        public decimal Total(string tier, IReadOnlyList<(string Product, int Qty, decimal Price)> lines)
        {
            return Build(tier, "x", "x", null, lines, OrderStatus.Pending).Total;
        }

        public string Receipt(string tier, string name, string rawEmail, string city, IReadOnlyList<(string Product, int Qty, decimal Price)> lines)
        {
            return Build(tier, name, rawEmail, city, lines, OrderStatus.Pending).Receipt().Replace("\r\n", "\n");
        }

        public string Advance(string status, string action)
        {
            var order = Build("Standard", "x", "x", null, new[] { ("x", 1, 1m) }, (OrderStatus)Enum.Parse(typeof(OrderStatus), status));
            return order.Advance(action).Status.ToString();
        }

        public string ShippingLabel(string city)
        {
            return Build("Standard", "x", "x", city, new[] { ("x", 1, 1m) }, OrderStatus.Pending).ShippingLabel();
        }

        public string NormalizeEmail(string raw)
        {
            var c = new Customer("x", CustomerTier.Standard);
            c.Email = raw;
            return c.Email;
        }

        public bool IdenticalOrdersAreEqual()
        {
            var lines = new[] { ("Pen", 2, 12.5m) };
            return Build("Gold", "Alice", "a@x.com", "London", lines, OrderStatus.Paid)
                .Equals(Build("Gold", "Alice", "a@x.com", "London", lines, OrderStatus.Paid));
        }

        public string RenameIfPresent(bool customerExists, string newName)
        {
            Customer customer = customerExists ? new Customer("old", CustomerTier.Standard) : null;
            if (customer != null)
            {
                customer.Name = newName;
            }
            return customer != null ? customer.Name : "(no customer)";
        }
    }
}
