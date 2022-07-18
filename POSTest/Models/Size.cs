using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSTest.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(5)]
        public string Name { get; set; } = "";

        [DataType(DataType.Currency)]
        public double Price { get; set; } = 0.0;

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

}
