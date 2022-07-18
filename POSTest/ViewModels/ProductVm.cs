using Microsoft.AspNetCore.Http;
using POSTest.Models;
using System.Collections.Generic;

namespace POSTest.ViewModels
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<Size> Sizes { get; set; } = new HashSet<Size>();
    }
}
