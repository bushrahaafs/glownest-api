using Microsoft.AspNetCore.Mvc;
using GlowNestBackend.Data;
using GlowNestBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GlowNestBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create-full-order")]
        public async Task<IActionResult> CreateFullOrder([FromBody] CreateOrderRequest request)
        {
            if (request.CartItems == null || request.CartItems.Count == 0)
                return BadRequest("السلة فارغة");

            int? firstOrderId = null;

            foreach (var item in request.CartItems)
            {
                var order = new Order
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderDate = DateTime.Now,
                    UserId = request.UserId // ✅ حفظ الـ UserId مع الطلب
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                if (firstOrderId == null)
                    firstOrderId = order.Id;
            }

            return Ok(new { orderId = firstOrderId });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Ok(orders);
        }
    }
}













