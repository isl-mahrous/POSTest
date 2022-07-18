using POSTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSTest.Repositories.Interfaces
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(
            List<ShoppingCartItem> items,
            string userName = "isl",
            string ShippingAddress ="Cairo"
        );
        Task<List<Order>> GetOrdersByUserId(string userId = "isl");
    }
}
