using Microsoft.EntityFrameworkCore;
using POSTest.Data;
using POSTest.Models;
using POSTest.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Repositories.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserId(string userName = "isl")
        {
            var orders = await _context.Orders.Where(o=>o.UserName == "isl")
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
            
            return orders;
        }


        public async Task StoreOrderAsync(
            List<ShoppingCartItem> items,
            string userName,
            string shippingAddress
        )
        {
            var order = new Order()
            {
                UserName = "isl",
                ShippingAddress = "Cairo"
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };

                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
