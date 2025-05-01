namespace GlowNestBackend.Models
{
    public class CreateOrderRequest
    {
        public List<CartItem> CartItems { get; set; } = new();
        public DeliveryInfo DeliveryInfo { get; set; } = new();
        public PaymentInfo PaymentInfo { get; set; } = new();
        public int? UserId { get; set; } // ✅ جديد
    }

    public class CartItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class DeliveryInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class PaymentInfo
    {
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
    }
}


