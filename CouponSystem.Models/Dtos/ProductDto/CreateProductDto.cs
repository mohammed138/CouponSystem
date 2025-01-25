using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.ProductDto
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = " this Field is required.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = " this Field is required.")]
        public decimal? Price { get; set; }


        [Required(ErrorMessage = " this Field is required.")]
        public int? Quantity { get; set; }

        public bool? InStock { get; set; } 
        public string? SKU { get; set; }
        public string? HashCode { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public decimal? Width { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public decimal? Length { get; set; }

    }
}
