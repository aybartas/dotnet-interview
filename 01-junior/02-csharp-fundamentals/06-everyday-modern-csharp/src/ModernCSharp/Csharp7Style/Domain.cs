using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

// ═══════════════════════════════════════════════════════════════════════════════════════
// "C# 7 TARZI" sipariş modeli. Bu dosya BİLEREK eski alışkanlıklarla yazılmıştır
// (derleyici hâlâ C# 14 olsa da stil C# 7 dönemidir): block-scoped namespace, açık using'ler, elle Equals/GetHashCode/ToString,
// elle immutability (readonly alan + ctor + WithXxx metotları), switch STATEMENT, if + throw null kontrolleri, string.Format/StringBuilder.
// `#nullable disable`: C# 7 döneminde nullable reference types YOKTU; bu yüzden bu dosyada null analizi kapalıdır (oblivious).
// Aynı model: ../Csharp14Style/Domain.cs. Satır sayısı ve okunabilirliği karşılaştırın (Section 10 / compare-loc.sh).
// ═══════════════════════════════════════════════════════════════════════════════════════
#nullable disable
namespace Junior.CSharpFundamentals.ModernCSharp.Csharp7Style
{
    public enum CustomerTier { Standard, Gold, Platinum }

    public enum OrderStatus { Pending, Paid, Shipped, Delivered, Cancelled }

    public sealed class Address : IEquatable<Address>
    {
        public Address(string city, string country)
        {
            City = city;
            Country = country;
        }

        public string City { get; }
        public string Country { get; }

        public Address WithCity(string city)
        {
            return new Address(city, Country);
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(City, other.City) && string.Equals(Country, other.Country);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 31 + (City != null ? City.GetHashCode() : 0);
                hash = hash * 31 + (Country != null ? Country.GetHashCode() : 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("Address {{ City = {0}, Country = {1} }}", City, Country);
        }
    }

    public sealed class Customer
    {
        private string _email;
        private string _name;

        public Customer(string name, CustomerTier tier)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            _name = name;
            Tier = tier;
            _email = string.Empty;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _email = value.Trim().ToLowerInvariant();
            }
        }

        public CustomerTier Tier { get; }

        public Address Address { get; set; }
    }

    public sealed class OrderLine : IEquatable<OrderLine>
    {
        public OrderLine(string product, int quantity, decimal unitPrice)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string Product { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal Total { get { return Quantity * UnitPrice; } }

        public bool Equals(OrderLine other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Product == other.Product && Quantity == other.Quantity && UnitPrice == other.UnitPrice;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderLine);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 31 + (Product != null ? Product.GetHashCode() : 0);
                hash = hash * 31 + Quantity.GetHashCode();
                hash = hash * 31 + UnitPrice.GetHashCode();
                return hash;
            }
        }
    }

    public sealed class Order : IEquatable<Order>
    {
        private readonly ReadOnlyCollection<OrderLine> _lines;

        public Order(string id, Customer customer, IEnumerable<OrderLine> lines, OrderStatus status)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            Id = id;
            Customer = customer;
            _lines = new ReadOnlyCollection<OrderLine>(new List<OrderLine>(lines));
            Status = status;
        }

        public string Id { get; }
        public Customer Customer { get; }
        public IReadOnlyList<OrderLine> Lines { get { return _lines; } }
        public OrderStatus Status { get; }

        public decimal Subtotal { get { return _lines.Sum(l => l.Total); } }

        public decimal DiscountRate
        {
            get
            {
                var bulk = Subtotal > 1000m;
                decimal rate;
                switch (Customer.Tier)
                {
                    case CustomerTier.Platinum:
                        rate = bulk ? 0.20m : 0.15m;
                        break;
                    case CustomerTier.Gold:
                        rate = bulk ? 0.15m : 0.10m;
                        break;
                    case CustomerTier.Standard:
                        rate = bulk ? 0.05m : 0m;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return rate;
            }
        }

        public decimal Total { get { return Subtotal * (1 - DiscountRate); } }

        public Order WithStatus(OrderStatus status)
        {
            return new Order(Id, Customer, _lines, status);
        }

        public Order Advance(string action)
        {
            OrderStatus next;
            switch (Status)
            {
                case OrderStatus.Pending when action == "pay":
                    next = OrderStatus.Paid;
                    break;
                case OrderStatus.Paid when action == "ship":
                    next = OrderStatus.Shipped;
                    break;
                case OrderStatus.Shipped when action == "deliver":
                    next = OrderStatus.Delivered;
                    break;
                case OrderStatus.Pending when action == "cancel":
                case OrderStatus.Paid when action == "cancel":
                    next = OrderStatus.Cancelled;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("'{0}' durumundaki sipariş için '{1}' işlemi geçersiz", Status, action));
            }
            return WithStatus(next);
        }

        public string ShippingLabel()
        {
            string city = null;
            if (Customer != null && Customer.Address != null)
            {
                city = Customer.Address.City;
            }
            return city ?? "adres yok";
        }

        public bool Equals(Order other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Id == other.Id && Status == other.Status && _lines.SequenceEqual(other._lines);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 31 + (Id != null ? Id.GetHashCode() : 0);
                hash = hash * 31 + Status.GetHashCode();
                foreach (var line in _lines) hash = hash * 31 + line.GetHashCode();
                return hash;
            }
        }

        public string Receipt()
        {
            var inv = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            sb.AppendLine(string.Format(inv, "SİPARİŞ {0} — {1} <{2}>", Id, Customer.Name, Customer.Email));
            sb.AppendLine(string.Format(inv, "{0} müşteri · {1}", Customer.Tier, ShippingLabel()));
            foreach (var line in _lines)
            {
                sb.AppendLine(string.Format(inv, "  {0} × {1} = {2}", line.Quantity, line.Product, line.Total.ToString("F2", inv)));
            }
            sb.Append(string.Format(inv, "Ara toplam: {0} · İndirim %{1} → {2}",
                Subtotal.ToString("F2", inv), (DiscountRate * 100).ToString("0", inv), Total.ToString("F2", inv)));
            return sb.ToString();
        }
    }
}
