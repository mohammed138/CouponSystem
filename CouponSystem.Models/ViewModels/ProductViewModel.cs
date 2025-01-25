using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? InStock { get; set; }
        public string? SKU { get; set; }
        public string? HashCode { get; set; }
        public decimal Weight { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
