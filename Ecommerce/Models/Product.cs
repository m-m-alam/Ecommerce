using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int SpecialTagId { get; set; }
        public SpecialTag SpecialTag { get; set; }
    }
}
