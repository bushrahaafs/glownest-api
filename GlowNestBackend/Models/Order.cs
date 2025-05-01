namespace GlowNestBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; } // ✅ جديد
    }
}


