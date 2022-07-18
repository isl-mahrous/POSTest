using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSTest.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = "isl";
        public virtual List<OrderItem> OrderItems { get; set; }
        public string ShippingAddress { get; set; } = "Cairo";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}