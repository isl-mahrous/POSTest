using System.ComponentModel.DataAnnotations;

namespace POSTest.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public virtual Product Product { get; set; }
    }
}