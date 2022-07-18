using Microsoft.AspNetCore.Http;
using POSTest.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSTest.Payloads
{
    public class ProductPayload
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
    }
}
