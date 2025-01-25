using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.ViewModels
{
    public class CouponProductViewModel
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? InStock { get; set; }
        public string? SKU { get; set; }
    }
}
