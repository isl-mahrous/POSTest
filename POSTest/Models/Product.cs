using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSTest.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(5)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public virtual ICollection<Size> Sizes { get; set; } = new HashSet<Size>();
    }
}
